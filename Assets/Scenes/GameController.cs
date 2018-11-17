using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour {

    public static GameController Instance { get; private set; }
    public void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    public UnityEvent startGameEvent;

    public void StartGame()
    {
        startGameEvent.Invoke();
    }

}
