using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleAnimation : MonoBehaviour {

    [SerializeField] float _MIN;
    [SerializeField] float _MAX;
    [SerializeField] float _speedScaler = 1;
    [SerializeField] bool _unscaled;

    float sign = -1;
	void Update () {
        if (transform.localScale.x >= _MAX) sign = -1;
        else if (transform.localScale.x <= _MIN) sign = 1;

        var scale = _unscaled ? sign * Time.unscaledDeltaTime * _speedScaler : sign * Time.deltaTime * _speedScaler;

        transform.localScale += new Vector3(scale, scale);
    }
}
