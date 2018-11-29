using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField] Transform target;
    [SerializeField] float maxSpeed = 200f;

    Camera[] cams;
    bool follow = false;
    float speed;
    private void Start(){
        GameController.Instance.startGameEvent.AddListener(StartGame);
        cams = GetComponentsInChildren<Camera>();
    }

    private void StartGame(){
        follow = true;
        StartCoroutine(LerpCamera());
    }

    private IEnumerator LerpCamera()
    {
        while(speed <= maxSpeed)
        {
            speed += Time.deltaTime * 10;
            yield return new WaitForFixedUpdate();
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (target)
        {
            if (follow) { 
                var pos = target.position;
                pos.z = transform.position.z;
                pos.y = Mathf.Clamp(pos.y, 0.25f, float.PositiveInfinity);
                transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
                foreach (var cam in cams){
                    var size = Mathf.Clamp(target.position.y + 7f, 8, float.PositiveInfinity);
                    cam.orthographicSize = size;
                }
            }
        }
	}
}
