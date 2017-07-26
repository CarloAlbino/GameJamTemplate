using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDetection : MonoBehaviour {

	void Update()
    {
        if(InputManager.Instance.GetButtonDown("Action"))
        {
            Debug.Log("Pressing Action");
        }
        if (InputManager.Instance.GetButtonDown("Cancel"))
        {
            Debug.Log("Pressing Cancel");
        }
        if (InputManager.Instance.GetButtonDown("Up"))
        {
            Debug.Log("Pressing Up");
        }
        if (InputManager.Instance.GetButtonDown("Down"))
        {
            Debug.Log("Pressing Down");
        }
        if (InputManager.Instance.GetButtonDown("Left"))
        {
            Debug.Log("Pressing Left");
        }
        if (InputManager.Instance.GetButtonDown("Right"))
        {
            Debug.Log("Pressing Right");
        }
    }
}
