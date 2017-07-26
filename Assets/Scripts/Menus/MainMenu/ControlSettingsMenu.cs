using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlSettingsMenu : MonoBehaviour {

    [Header ("Action Names")]
    [SerializeField]
    private string[] actionNames;
    [SerializeField]
    private string[] axisNames;

    [Header("Button Label Names")]
    [SerializeField]
    private Text[] buttonActionLabels;
    [SerializeField]
    private Text[] keyActionLabels;
    [SerializeField]
    private Text[] buttonAxisLabels;
    [SerializeField]
    private Text[] keyPosAxisLabels;
    [SerializeField]
    private Text[] keyNegAxisLabels;

    void Update ()
    {
        for (int i = 0; i < actionNames.Length; i++)
            buttonActionLabels[i].text = InputManager.Instance.UI_GetInputName(actionNames[i] + " Controller False");
        for (int i = 0; i < actionNames.Length; i++)
            keyActionLabels[i].text = InputManager.Instance.UI_GetInputName(actionNames[i] + " Keyboard False");

        for (int i = 0; i < axisNames.Length; i++)
            buttonAxisLabels[i].text = InputManager.Instance.UI_GetInputName(axisNames[i] + " Controller False");

        for (int i = 0; i < axisNames.Length; i++)
            keyPosAxisLabels[i].text = InputManager.Instance.UI_GetInputName(axisNames[i] + " Keyboard False");
        for (int i = 0; i < axisNames.Length; i++)
            keyNegAxisLabels[i].text = InputManager.Instance.UI_GetInputName(axisNames[i] + " Keyboard True");
    }
}
