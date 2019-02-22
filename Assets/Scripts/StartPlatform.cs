using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlatform : MonoBehaviour {

    [SerializeField] GameObject tutorial;

	// Use this for initialization
	void Start () {
        GameController.Instance.startGameEvent.AddListener(GameStart);
        tutorial.SetActive(false);
	}

    private void GameStart()
    {
        tutorial.SetActive(true);
    }
}
