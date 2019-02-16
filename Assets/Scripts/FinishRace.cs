using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishRace : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var car = collision.GetComponentInParent<CarController>();
        if (car && !GameController.Instance.raceOver)
        {
            GameController.Instance.FinishRace();
            FindObjectOfType<GameUIController>().FinishRace();
        }
    }
}
