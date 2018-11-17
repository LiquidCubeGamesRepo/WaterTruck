using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour {

    public static GameController Instance { get; private set; }
    public void Awake(){
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    public UnityEvent startGameEvent;

    public bool canStart = false;
    [SerializeField] Transform target;

    CarController carController;

    public void Start(){
        carController = FindObjectOfType<CarController>();
        StartCoroutine(MoveToTarget());
    }


    private IEnumerator MoveToTarget()
    {
        canStart = false;
        //start game noise 
        yield return new WaitForSeconds(0.5f);

        var dist = Vector2.Distance(carController.transform.position, target.position);
        while (dist > 2f)
        {
            dist = Vector2.Distance(carController.transform.position, target.position);
            carController.Acceleration();
            yield return new WaitForEndOfFrame();
        }
        carController.Break();
        canStart = true;
    }

    public void StartGame(){
        startGameEvent.Invoke();
    }

}

[Serializable]
public struct PlayerSettings
{
    public List<float> distances;
    public int coins;
    //buyed items etc
}
