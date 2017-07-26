using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EAxisState
{
    GreaterThan,
    LessThan
}

[System.Serializable]
public struct InputMapping
{
    [Tooltip("Name of the button when referred to in the script. The action.")]
    public string name;
    [Tooltip("Default key for the action. Use the Unity KeyCode name. (If KeyCode.W enter W)\nIf an Axis, the positive key. (For keyboard control)")]
    public string positiveKeyName;
    [Tooltip("If an Axis, the negative key. (For keyboard control)")]
    public string negativeKeyName;
    [Tooltip("Default joypad button for the action. Use b.buttonName (same name as entered in Unity's input manager) to use a joystick button. Use a.axisName (same name as entered in Unity's input manager) to use a joystick axis.")]
    public string buttonName;
    [Tooltip("Is positive state greater or less than 0? (For joypad control)\nCan be used with GetButton with an axis Down = LeftStickVertical(LessThan)")]
    public EAxisState axisPositiveState;
    [Tooltip("Does this action use an axis.")]
    public bool isAxis;

    private bool m_isPressed;
    public bool IsPressed { get { return m_isPressed; } }
    public void Press()
    {
        m_isPressed = true;
    }
    public void UnPress()
    {
        m_isPressed = false;
    }
}

public class InputManager : Singleton<InputManager> {
    [Header("Input Mappings")]
    [SerializeField, Tooltip("The inputs that are needed for the game.")]
    private InputMapping[] m_inputMappings;
    public InputMapping[] mappings { get; private set; }
    private Dictionary<string, InputMapping> m_inputs = new Dictionary<string, InputMapping>();

    [Header("Controller Input Names")]
    [SerializeField, Tooltip("All the button names from Unity's Input Manager.")]
    private string[] m_controllerButtonNames;
    [SerializeField, Tooltip("All the axis names from Unity's Input Manager.")]
    private string[] m_controllerAxisNames;
    private bool m_reassignInput = false;
    private bool m_reassignAxis = false;
    private bool m_reassigningPositive = true;
    private string m_inputToReassign = "";
    private bool m_reassigningKeys = false;
    [SerializeField, Tooltip("Time to waitbetween selecting to reassign and actually reassigning.")]
    private float m_reasignDelay = 1.0f;
    private float m_reasignWaitTime = 0;

	void Start ()
    {
        for(int i = 0; i < m_inputMappings.Length; i++)
        {
            m_inputs.Add(m_inputMappings[i].name, m_inputMappings[i]);  // Add inputs to the dictionary
        }
	}
	
	void Update ()
    {
	    if(m_reassignInput || m_reassignAxis) // If an input is being reassigned
        {
            if (m_reasignWaitTime > m_reasignDelay)
                DetectNewInput();
            else
                m_reasignWaitTime += Time.deltaTime;
        }
	}

    #region Input Reassigning
    public void UI_ReassignInput(string inputNameAndDevice)
    {
        string[] newInput = inputNameAndDevice.Split(' ');

        m_inputToReassign = newInput[0];

        switch (newInput[1])
        {
            case "Keyboard":
                m_reassigningKeys = true;
                break;
            case "Controller":
                m_reassigningKeys = false;
                break;
            default:
                Debug.LogError("Unknown Device. [UI_ReassignInput], InputManager");
                break;
        }

        m_reassignInput = true;
        m_reasignWaitTime = 0;
    }

    public void UI_ReasignAxis(string inputNameAndStateAndDevice)
    {
        string[] newInput = inputNameAndStateAndDevice.Split(' ');

        m_inputToReassign = newInput[0];
        
        switch(newInput[1])
        {
            case "False":
                m_reassigningPositive = false;
                break;
            case "True":
                m_reassigningPositive = true;
                break;
            default:
                Debug.LogError("Error Reading 'State'. [UI_ReasignAxis], InputManager");
                break;
        }

        switch(newInput[2])
        {
            case "Keyboard":
                m_reassigningKeys = true;
                break;
            case "Controller":
                m_reassigningKeys = false;
                break;
            default:
                Debug.LogError("Unknown Device. [UI_ReasignAxis], InputManager");
                break;
        }

        m_reassignAxis = true;
        m_reasignWaitTime = 0;
    }

    public string UI_GetInputName(string inputNameAndDeviceAndNegative)
    {
        InputMapping mapping;
        bool checkingKeys = false;
        bool isNegative = false;
        string[] input = inputNameAndDeviceAndNegative.Split(' ');

        switch (input[1])
        {
            case "Keyboard":
                checkingKeys = true;
                break;
            case "Controller":
                checkingKeys = false;
                break;
            default:
                Debug.LogError("Unknown Device. [UI_GetInputName], InputManager");
                break;
        }

        switch(input[2])
        {
            case "True":
                isNegative = true;
                break;
            case "False":
                isNegative = false;
                break;
            default:
                Debug.LogError("Error Reading 'IsNegative'. [UI_GetInputName], InputManager");
                break;
        }

        if (m_inputs.TryGetValue(input[0], out mapping))
        {
            if (checkingKeys)
            {
                if (isNegative)
                    return mapping.negativeKeyName;
                else
                    return mapping.positiveKeyName;
            }
            else
                return mapping.buttonName;
        }
        else
        {
            Debug.LogError("Input name not valid. [UI_GetInputName, InputManager]");
            return "";
        }
    }

    private void DetectNewInput()
    {
        if (m_reassigningKeys)
        {
            foreach (KeyCode k in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(k))
                {
                    InputMapping oldMapping = m_inputs[m_inputToReassign];
                    m_inputs.Remove(m_inputToReassign);
                    InputMapping newMapping = new InputMapping();
                    newMapping.name = m_inputToReassign;

                    if (m_reassignAxis)
                    {
                        if (m_reassigningPositive)
                        {
                            newMapping.positiveKeyName = k.ToString();
                            newMapping.negativeKeyName = oldMapping.negativeKeyName;
                        }
                        else
                        {
                            newMapping.positiveKeyName = oldMapping.positiveKeyName;
                            newMapping.negativeKeyName = k.ToString();
                        }
                    }
                    else
                    {
                        newMapping.positiveKeyName = k.ToString();
                        newMapping.negativeKeyName = oldMapping.negativeKeyName;
                    }

                    newMapping.buttonName = oldMapping.buttonName;

                    newMapping.axisPositiveState = oldMapping.axisPositiveState;

                    m_inputToReassign = "";
                    m_reassignInput = false;
                    m_reassignAxis = false;

                    m_inputs.Add(newMapping.name, newMapping);  // Add inputs to the dictionary
                    return;
                }
            }
        }
        else
        {
            foreach (string s in m_controllerButtonNames)
            {
                if (Input.GetButtonDown(s))
                {
                    InputMapping oldMapping = m_inputs[m_inputToReassign];
                    m_inputs.Remove(m_inputToReassign);
                    InputMapping newMapping = new InputMapping();
                    newMapping.name = m_inputToReassign;
                    newMapping.positiveKeyName = oldMapping.positiveKeyName;
                    newMapping.negativeKeyName = oldMapping.negativeKeyName;
                    newMapping.buttonName = /*"b." + */s;

                    newMapping.axisPositiveState = oldMapping.axisPositiveState;

                    m_inputToReassign = "";
                    m_reassignInput = false;
                    m_reassignAxis = false;

                    m_inputs.Add(newMapping.name, newMapping);  // Add inputs to the dictionary
                    return;
                }
            }

            foreach (string s in m_controllerAxisNames)
            {
                if (Mathf.Abs(Input.GetAxis(s)) > 0)
                {
                    InputMapping oldMapping = m_inputs[m_inputToReassign];
                    m_inputs.Remove(m_inputToReassign);
                    InputMapping newMapping = new InputMapping();
                    newMapping.name = m_inputToReassign;
                    newMapping.positiveKeyName = oldMapping.positiveKeyName;
                    newMapping.negativeKeyName = oldMapping.negativeKeyName;
                    newMapping.buttonName = /*"a." + */s;

                    newMapping.axisPositiveState = EAxisState.GreaterThan;

                    m_inputToReassign = "";
                    m_reassignInput = false;
                    m_reassignAxis = false;

                    m_inputs.Add(newMapping.name, newMapping);  // Add inputs to the dictionary
                    return;
                }
                else if (Mathf.Abs(Input.GetAxis(s)) < 0)
                {
                    InputMapping oldMapping = m_inputs[m_inputToReassign];
                    m_inputs.Remove(m_inputToReassign);
                    InputMapping newMapping = new InputMapping();
                    newMapping.name = m_inputToReassign;
                    newMapping.positiveKeyName = oldMapping.positiveKeyName;
                    newMapping.negativeKeyName = oldMapping.negativeKeyName;
                    newMapping.buttonName = /*"a." + */s;

                    if (!m_reassignAxis)
                        newMapping.axisPositiveState = EAxisState.LessThan;
                    else
                        newMapping.axisPositiveState = EAxisState.GreaterThan;

                    m_inputToReassign = "";
                    m_reassignInput = false;
                    m_reassignAxis = false;

                    m_inputs.Add(newMapping.name, newMapping);  // Add inputs to the dictionary
                    return;
                }
            }
        }
    }
    #endregion Input Reassigning

    #region Input State
    /// <summary>
    /// Returns on every frame (auto-fire)
    /// </summary>
    /// <param name="inputName">Name of the action</param>
    /// <returns>Bool</returns>
    public bool GetButton(string inputName)
    {
        InputMapping input;
        if (m_inputs.TryGetValue(inputName, out input))
        {
            foreach(InputMapping i in m_inputMappings)
            {
                if(i.name == inputName)
                {
                    if (!input.isAxis)
                    {
                        if ((Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), input.positiveKeyName))) ||
                            (Input.GetAxis(input.buttonName) < 0 && input.axisPositiveState == EAxisState.GreaterThan) ||
                            (Input.GetAxis(input.buttonName) > 0 && input.axisPositiveState == EAxisState.LessThan) ||
                            (Input.GetButton(input.buttonName)))
                        {
                            i.Press();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (Mathf.Abs(GetAxis(input.buttonName)) > 0)
                        {
                            i.Press();
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Returns on the frame the input is pressed
    /// </summary>
    /// <param name="inputName">Name of the action</param>
    /// <returns>Bool</returns>
    public bool GetButtonDown(string inputName)
    {
        InputMapping input;
        if (m_inputs.TryGetValue(inputName, out input))
        {
            foreach (InputMapping i in m_inputMappings)
            {
                if (i.name == inputName)
                {
                    if(!i.IsPressed)
                    {
                        if (!input.isAxis)
                        {
                            if ((Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), input.positiveKeyName))) ||
                                (Input.GetAxis(input.buttonName) < 0 && input.axisPositiveState == EAxisState.GreaterThan) ||
                                (Input.GetAxis(input.buttonName) > 0 && input.axisPositiveState == EAxisState.LessThan) ||
                                (Input.GetButtonDown(input.buttonName)))
                            {
                                i.Press();
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            if (Mathf.Abs(GetAxis(input.buttonName)) > 0)
                            {
                                i.Press();
                                return true;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Returns on the frame the input is released
    /// </summary>
    /// <param name="inputName">Name of the action</param>
    /// <returns>Bool</returns>
    public bool GetButtonUp(string inputName)
    {
        InputMapping input;
        if (m_inputs.TryGetValue(inputName, out input))
        {
            foreach (InputMapping i in m_inputMappings)
            {
                if (i.name == inputName)
                {
                    if (i.IsPressed)
                    {
                        if (!input.isAxis)
                        {
                            if ((Input.GetKeyUp((KeyCode)System.Enum.Parse(typeof(KeyCode), input.positiveKeyName))) ||
                                (Input.GetAxis(input.buttonName) < 0 && input.axisPositiveState == EAxisState.GreaterThan) ||
                                (Input.GetAxis(input.buttonName) > 0 && input.axisPositiveState == EAxisState.LessThan) ||
                                (Input.GetButtonDown(input.buttonName)))
                            {
                                i.UnPress();
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        { 
                            if (Mathf.Abs(GetAxis(input.buttonName)) == 0)
                            {
                                i.UnPress();
                                return true;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Returns the state of the axis between -1 and 1
    /// </summary>
    /// <param name="axisName">Name of the action</param>
    /// <returns>Float</returns>
    public float GetAxis(string axisName)
    {
        InputMapping input;
        if (m_inputs.TryGetValue(axisName, out input))
        {
            if (input.isAxis)
            {
                float output = 0.0f;
                bool keypressed = false;
                if(Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), input.positiveKeyName)))
                {
                    output += 1.0f;
                    keypressed = true;
                }
                if(Input.GetKey((KeyCode)System.Enum.Parse(typeof(KeyCode), input.negativeKeyName)))
                {
                    output += -1.0f;
                    keypressed = true;
                }

                if(keypressed)
                {
                    return output;
                }

                return Input.GetAxis(input.buttonName);
            }
            else
            {
                Debug.LogError(axisName + " is not an axis, you will never get -1. Did you mean to use GetButton()?");
                if (Input.GetButton(axisName))
                {
                    return 1.0f;
                }
                else
                {
                    return 0.0f;
                }
            }
        }
        return 0.0f;
    }
    #endregion Input State
}
