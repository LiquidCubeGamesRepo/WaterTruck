using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

    public bool CanMove { get; set; }

    [SerializeField] float maxCarSpeed;
    [SerializeField] float accelerationSpeed;
    [SerializeField] WheelJoint2D[] wheels;

    float carSpeed;

    private void Start(){
        CanMove = false;
        GameController.Instance.startGameEvent.AddListener(StartGame);
    }

    void StartGame() {
        CanMove = true;
    }

    // Update is called once per frame
    void Update () {

        if (!CanMove) return;

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

    public void NoInput()
    {
        carSpeed -= Time.deltaTime * accelerationSpeed;
        foreach (var wheel in wheels){
            var motor = wheel.motor;
            motor.motorSpeed = carSpeed;
            wheel.motor = motor;
            wheel.useMotor = false;
        }
    }

    public void Break()
    {
        foreach (var wheel in wheels){
            var motor = wheel.motor;
            motor.motorSpeed = 0;
            wheel.motor = motor;
            wheel.useMotor = true;
        }
        carSpeed = 0;
    }

    public void Acceleration()
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
