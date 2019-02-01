using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    public Transform target;

    private Vector3 originPos;
    private Vector3 curVel;

	// Use this for initialization
	void Start () {
        originPos = transform.position;
        originPos.y = target.position.y;
        transform.position = originPos;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 desiredPos = 
            new Vector3(originPos.x, target.position.y, originPos.z);
        transform.position = 
            Vector3.SmoothDamp(transform.position, desiredPos, ref curVel, 1f);
	}
}
