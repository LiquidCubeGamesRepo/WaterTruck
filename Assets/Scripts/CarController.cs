using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

    public bool CanMove;
    public float CarSpeed { get { return Mathf.Clamp(rig.velocity.x/6f, 0, 1); } }
    public bool isVisible;

    [SerializeField] float maxCarSpeed;
    [SerializeField] float accelerationSpeed;
    [SerializeField] WheelJoint2D[] wheels;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] List<PolygonCollider2D> carColliders;
    [SerializeField] GameObject additionWheelObject;

    float carSpeed;
    LiquidCounter lc;
    Rigidbody2D rig;


    private void Start(){
        CanMove = false;
        GameController.Instance.startGameEvent.AddListener(StartGame);
        lc = GetComponent<LiquidCounter>();
        lc.waterCountChange.AddListener(DestroyCar);

        rig = GetComponent<Rigidbody2D>();
    }

    void StartGame() {
        CanMove = true;
    }

    // Update is called once per frame
    void Update () {

        if (transform.position.y < -8f) isVisible = false;
        else isVisible = true;

        if (!CanMove) return;

#if UNITY_EDITOR
        if (Input.GetButton("Accel"))
            Acceleration();
        //else if (Input.GetButton("Break"))
        //    Break();
        else
            NoInput();
#elif UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Acceleration();
            //Touch touch = Input.GetTouch(0);

            //if (touch.position.x > Screen.width * 0.5f)
            //    Acceleration();
            //else if(touch.position.x < Screen.width * 0.5f)
            //    Break();
        }
        else
            NoInput();
#endif
        carSpeed = Mathf.Clamp(carSpeed, 0, maxCarSpeed);

    }

    internal void ChangeCar()
    {
        //spriteRenderer.sprite = GameController.Instance.carSprites[GameController.Instance.playerSettings.currentCar];
        //foreach (var col in carColliders) {
        //    col.enabled = false;
        //}
        ////carColliders[GameController.Instance.playerSettings.currentCar].enabled = true;

        //if(GameController.Instance.playerSettings.currentCar == 1)
        //{
        //    additionWheelObject.SetActive(true);
        //}
        //else
        //    additionWheelObject.SetActive(false);
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

    public void DestroyCar(int waterCount){

        if(waterCount <= lc.waterGameoverCount) {
            CanMove = false;
            NoInput();
        }
    }
}
