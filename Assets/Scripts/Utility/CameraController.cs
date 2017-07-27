using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField]
    private Camera m_camera;
    [SerializeField]
    private Transform[] m_cameraPoint;

    private int m_nextCamera = -1;
    private float m_speed = 1.0f;
    private bool m_moving = false;

	void Update ()
    {
        if (m_moving)
        {
            m_camera.transform.position = Vector3.Lerp(m_camera.transform.position, m_cameraPoint[m_nextCamera].transform.position, m_speed * Time.deltaTime);
            m_camera.transform.rotation = Quaternion.Lerp(m_camera.transform.rotation, m_cameraPoint[m_nextCamera].transform.rotation, m_speed * Time.deltaTime);
        }
	}

    public void CutToCamera(int num)
    {
        m_moving = false;
        m_camera.transform.position = m_cameraPoint[num].position;
        m_camera.transform.rotation = m_cameraPoint[num].rotation;
    }

    public void MoveCamera(int num, float speed)
    {
        m_nextCamera = num;
        m_speed = speed;
        m_moving = true;
    }
}
