using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuUIController : MonoBehaviour {

    [SerializeField] GameObject MenuCanvas;
    [SerializeField] GameObject GameCanvas;
    [SerializeField] StorePanel storePanel;
    [SerializeField] AchievementPanel achievementPanel;


    [SerializeField] Sprite soundOnSprite;
    [SerializeField] Sprite soundOffSprite;
    [SerializeField] Button soundButton;
    [SerializeField] Button storeButton;
    [SerializeField] Button achivButton;
    [SerializeField] Button startGameButton;

    GameController GC;
    public void Start(){
        GC = FindObjectOfType<GameController>();

        MenuCanvas.SetActive(true);
        GameCanvas.SetActive(false);
        UpdateBestDistanceText();

        soundButton.onClick.AddListener(ToggleSound);
        storeButton.onClick.AddListener(storePanel.Open);
        achivButton.onClick.AddListener(achievementPanel.Open);
        startGameButton.onClick.AddListener(StartGameClick);
        if (GameController.Instance.gameData.audioOn) soundButton.GetComponent<Image>().sprite = soundOnSprite;
        else soundButton.GetComponent<Image>().sprite = soundOffSprite;

        if (GameController.Instance.loadMode == Mode.StartLevel)
        {
            MenuCanvas.SetActive(false);
            GameCanvas.SetActive(true);
        }
    }

    private void StartGameClick()
    {
        StartGame();
    }

    private void ToggleSound()
    {
        var on = GameController.Instance.ToggleSound();
        if (on) soundButton.GetComponent<Image>().sprite = soundOnSprite;
        else soundButton.GetComponent<Image>().sprite = soundOffSprite;
    }

    public void Update()
    {
        if (!GC.IsGamePlaying) {
//#if UNITY_EDITOR
            if (Input.GetButton("Accel"))
            StartGame();
//#elif UNITY_ANDROID
//        if(Input.touchCount > 0)
//        {
//            Touch touch = Input.GetTouch(0);
//            if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
//                StartGame();
//        }
//#endif
        }
    }

    private void StartGame(){
        if (GC.canStart) { 
            MenuCanvas.SetActive(false);
            GameCanvas.SetActive(true);
            GC.StartGame();
        }
    }

    public void UpdateBestDistanceText(){
        //bestDistanceText.text = "Best Distance: " + GameController.Instance.playerSettings.bestDistance;
    }

}
