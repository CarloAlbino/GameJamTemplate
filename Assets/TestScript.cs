using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {

    public bool m_cut = true;
    public CameraController m_camControl = null;

	void Update () {
        if (m_camControl != null)
        {
            if (InputManager.Instance.GetButtonDown("Up"))
            {
                if (m_cut)
                    m_camControl.CutToCamera(0);
                else
                    m_camControl.MoveCamera(0, 2);
            }
            else if (InputManager.Instance.GetButtonDown("Down"))
            {
                if (m_cut)
                    m_camControl.CutToCamera(1);
                else
                    m_camControl.MoveCamera(1, 4);
            }
            else if (InputManager.Instance.GetButtonDown("Left"))
            {
                if (m_cut)
                    m_camControl.CutToCamera(2);
                else
                    m_camControl.MoveCamera(2, 1);
            }
            else if (InputManager.Instance.GetButtonDown("Right"))
            {
                if (m_cut)
                    m_camControl.CutToCamera(3);
                else
                    m_camControl.MoveCamera(3, 5);
            }
            else if (InputManager.Instance.GetButtonDown("A"))
            {
                m_cut = !m_cut;
            }
        }
    }
}
