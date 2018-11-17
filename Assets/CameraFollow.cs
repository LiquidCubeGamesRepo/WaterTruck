using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField] Transform target;
    [SerializeField] float speed;

    Camera[] cams;
    bool follow = false;
    private void Start(){
        GameController.Instance.startGameEvent.AddListener(StartGame);
        cams = GetComponentsInChildren<Camera>();
    }

    private void StartGame(){
        follow = true;
    }

    // Update is called once per frame
    void Update () {
        if (target)
        {
            if (follow) { 
                var pos = target.position;
                pos.z = transform.position.z;
                transform.position = pos;
                foreach (var cam in cams){
                    cam.orthographicSize = target.position.y + 8f;
                }
            }
        }
	}
}
