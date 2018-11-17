using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

    [SerializeField] float maxCarSpeed;
    [SerializeField] float accelerationSpeed;
    [SerializeField] WheelJoint2D[] wheels;

    float carSpeed;

    // Update is called once per frame
    void Update () {

#if UNITY_EDITOR
        if (Input.GetButton("Accel"))
            Acceleration();
        else if (Input.GetButton("Break"))
            Break();
        else
            NoInput();
#elif UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            text.text = touch.position.ToString();
            if (touch.position.x > Screen.width * 0.5f)
                Acceleration();
            else if(touch.position.x < Screen.width * 0.5f)
                Break();
        }
        else
            NoInput();
#endif
        carSpeed = Mathf.Clamp(carSpeed, 0, maxCarSpeed);

    }

    private void NoInput()
    {
        carSpeed -= Time.deltaTime * accelerationSpeed;
        foreach (var wheel in wheels){
            var motor = wheel.motor;
            motor.motorSpeed = carSpeed;
            wheel.motor = motor;
            wheel.useMotor = false;
        }
    }

    private void Break()
    {
        foreach (var wheel in wheels){
            var motor = wheel.motor;
            motor.motorSpeed = 0;
            wheel.motor = motor;
            wheel.useMotor = true;
        }
    }

    private void Acceleration()
    {
        carSpeed += Time.deltaTime * accelerationSpeed;
        foreach (var wheel in wheels)
        {
            var motor = wheel.motor;
            motor.motorSpeed = -carSpeed;
            wheel.motor = motor;
            wheel.useMotor = true;
        }
    }
}
