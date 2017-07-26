using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputOptionsTest : MonoBehaviour {

    public string[] actions;
    public Text[] buttons;

    void Start()
    {
        for(int i = 0; i < actions.Length; i++)
        {
            buttons[i].text = InputManager.Instance.UI_GetInputName(actions[i]);
        }
    }

    void Update()
    {
        for (int i = 0; i < actions.Length; i++)
        {
            buttons[i].text = InputManager.Instance.UI_GetInputName(actions[i]);
        }
    }

    public void Reassign(int i)
    {
        InputManager.Instance.UI_ReassignInput(actions[i]);
        //buttons[i].text = InputManager.Instance.UI_GetInputName(actions[i]);
    }
}
