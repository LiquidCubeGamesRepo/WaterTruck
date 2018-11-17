using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleAnimation : MonoBehaviour {

    // Update is called once per frame
    float sign = -1;
	void Update () {
        if (transform.localScale.x >= 1f) sign = -1;
        else if (transform.localScale.x <= 0.5f) sign = 1;
        transform.localScale += new Vector3(sign * Time.deltaTime, sign * Time.deltaTime);
    }
}
