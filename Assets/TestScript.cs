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
                    m_camControl.CutToCamera(0, 80.0f);
                else
                    m_camControl.MoveCamera(0, 2, 80.0f);
            }
            else if (InputManager.Instance.GetButtonDown("Down"))
            {
                if (m_cut)
                    m_camControl.CutToCamera(1, 10, true);
                else
                    m_camControl.MoveCamera(1, 4, 10, true);
            }
            else if (InputManager.Instance.GetButtonDown("Left"))
            {
                if (m_cut)
                    m_camControl.CutToCamera(2, 30, false);
                else
                    m_camControl.MoveCamera(2, 1, 30, false);
            }
            else if (InputManager.Instance.GetButtonDown("Right"))
            {
                if (m_cut)
                    m_camControl.CutToCamera(3, 3, true);
                else
                    m_camControl.MoveCamera(3, 5, 3, true);
            }
            else if (InputManager.Instance.GetButtonDown("A"))
            {
                m_cut = !m_cut;
            }
        }
    }
}
