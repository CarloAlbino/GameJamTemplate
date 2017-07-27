using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField, Tooltip("Camera to control.")]
    private Camera m_camera;
    [SerializeField, Tooltip("The positions that the camera will move to")]
    private Transform[] m_cameraPoint;

    private int m_nextCamera = -1;
    private float m_speed = 1.0f;
    private float m_nextFOV = 60.0f;
    private bool m_moving = false;

	void Update ()
    {
        if (m_moving)
        {
            m_camera.transform.position = Vector3.Lerp(m_camera.transform.position, m_cameraPoint[m_nextCamera].transform.position, m_speed * Time.deltaTime);
            m_camera.transform.rotation = Quaternion.Lerp(m_camera.transform.rotation, m_cameraPoint[m_nextCamera].transform.rotation, m_speed * Time.deltaTime);
            if (m_camera.orthographic)
                m_camera.orthographicSize = Mathf.Lerp(m_camera.orthographicSize, m_nextFOV, m_speed * Time.deltaTime);
            else
                m_camera.fieldOfView = Mathf.Lerp(m_camera.fieldOfView, m_nextFOV, m_speed * Time.deltaTime);
        }
	}

    /// <summary>
    /// Instant cut to the camera indicated.
    /// </summary>
    /// <param name="num">Camera number (set in the inspector)</param>
    /// <param name="fov">Field of view, if the camera is orthografic this is used as the size</param>
    /// <param name="isOrtho">Is the camera orthographic?  Set to false for perspective</param>
    public void CutToCamera(int num, float fov, bool isOrtho = false)
    {
        m_moving = false;
        m_camera.transform.position = m_cameraPoint[num].position;
        m_camera.transform.rotation = m_cameraPoint[num].rotation;
        m_camera.orthographic = isOrtho;
        if (isOrtho)
            m_camera.orthographicSize = fov;
        else
            m_camera.fieldOfView = fov;

    }

    /// <summary>
    /// Makes the camera pan, dolly and rotate to the next position.
    /// </summary>
    /// <param name="num">The number of the final resting position of the camera (set in the inspector)</param>
    /// <param name="speed">The speed the camera will move</param>
    /// <param name="fov">Field of view, if the camera is orthografic this is used as the size</param>
    /// <param name="isOrtho">Is the camera orthographic?  Set to false for perspective</param>
    public void MoveCamera(int num, float speed, float fov, bool isOrtho = false)
    {
        m_nextCamera = num;
        m_speed = speed;
        m_nextFOV = fov;
        m_camera.orthographic = isOrtho;

        m_moving = true;
    }
}
