using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour {

    public float m_cameraSpeed = 0.5f;
    private Transform m_target;

    public Transform m_topRightLimit;
    public Transform m_bottomLeftLimit;

    void Start ()
    {
        m_target = GameObject.FindGameObjectWithTag("Player").transform;
    }

	void Update ()
    {
        Vector3 newPos = Vector2.Lerp(this.transform.position, m_target.position, m_cameraSpeed * Time.deltaTime);
        newPos.z = -10;
        this.transform.position = newPos;
	}
}
