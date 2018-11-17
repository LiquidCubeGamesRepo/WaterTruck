using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField] Transform target;
    [SerializeField] float speed;

    // Update is called once per frame
	void Update () {
        if (target)
        {
                var pos = target.position;
                pos.z = transform.position.z;
                transform.position = pos;
            
        }
	}
}
