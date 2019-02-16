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

    void Update () {

        if (!CanMove) return;

        if (transform.position.y < -8f) isVisible = false;
        else isVisible = true;

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
    }

    internal void ChangeCar()
    {
        spriteRenderer.sprite = GameController.Instance.SelectedCar;
        foreach (var col in carColliders) {
            col.enabled = false;
        }
        carColliders[GameController.Instance.gameData.SelectedCar].enabled = true;

        //if (GameController.Instance.playerSettings.currentCar == 1)
        //{
        //    additionWheelObject.SetActive(true);
        //}
        //else
        //    additionWheelObject.SetActive(false);
    }

    public void NoInput()
    {
        var hit = Physics2D.Raycast(transform.position, -transform.up, 0.25f, ~(1 << 8));
        if (!hit)
        {
            rig.AddForceAtPosition((-Vector2.right + Vector2.up) * accelerationSpeed * 0.25f * Time.deltaTime,
                      wheels[1].connectedBody.transform.position);
            Debug.Log(hit.collider);
        }
    }

    public void Acceleration()
    {
        rig.AddForceAtPosition((Vector2.right + Vector2.up) * accelerationSpeed * Time.deltaTime, 
                               wheels[0].connectedBody.transform.position);

        Debug.DrawLine(wheels[0].connectedBody.transform.position, wheels[0].connectedBody.transform.position + (Vector3)(Vector2.right + Vector2.up));

        //foreach (var wheel in wheels)
        //{
        //    var motor = wheel.motor;
        //    motor.motorSpeed = -carSpeed;
        //    wheel.motor = motor;
        //    //wheel.useMotor = true;
        //}
    }

    public void DestroyCar(int waterCount){

        if (waterCount <= lc.waterGameoverCount) {
            CanMove = false;
            NoInput();
        }
    }
}
