using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour {

    [SerializeField, Tooltip("Speed that the camera will follow")]
    private float m_cameraSpeed = 0.5f;
    [SerializeField, Tooltip("Object that the camera will follow, leave this null if you want the camera to follow the object tagged 'Player'")]
    private Transform m_target;

    //[SerializeField, Tooltip("The top and right limit of the camera movement")]
    //private Transform m_topRightLimit;
    //[SerializeField, Tooltip("The bottom and left limit of the camera movement")]
    //private Transform m_bottomLeftLimit;

    private bool m_canFollow = true;
    /// <summary>
    /// Enable/disable camera following
    /// </summary>
    public bool CanFollow { get { return m_canFollow; } set { m_canFollow = value; } }

    private float m_followZPos = -10;
    /// <summary>
    /// Z distance away from object being followed
    /// </summary>
    public float FollowZPos { get { return m_followZPos; } set { m_followZPos = value; } }

    void Start ()
    {
        if(m_target == null)
            m_target = GameObject.FindGameObjectWithTag("Player").transform;
    }

	void Update ()
    {
        if (m_canFollow)
        {
            Vector3 tempTarget = m_target.position;
            tempTarget.z = tempTarget.z + m_followZPos;
            Vector3 newPos = Vector3.Lerp(this.transform.position, tempTarget, m_cameraSpeed * Time.deltaTime);

            //Vector3 newPos = Vector2.Lerp(this.transform.position, m_target.position, m_cameraSpeed * Time.deltaTime);
            //newPos.z = m_followZPos;

            this.transform.position = newPos;
        }
	}

}
