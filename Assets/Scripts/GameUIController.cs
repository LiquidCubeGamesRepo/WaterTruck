using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour {

    [SerializeField] GameObject endGamePanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject victoryPanel;
    [SerializeField] GameObject popup;

    [SerializeField] Button restartButton;
    [SerializeField] Button restartButton1;

    [SerializeField] Button backToMenuButton;
    [SerializeField] Button backToMenuButton1;
    [SerializeField] Button backToMenuButton2;
    [SerializeField] Button nextLevelButton;
    [SerializeField] Button pauseButton;
    [SerializeField] Button continueButton;

    [SerializeField] Text currentLevelText;

    [SerializeField] Image waterCountImage;
    [SerializeField] Image distanceProggresImage;
    [SerializeField] Text coinsCounter;

    LiquidCounter lc;
    DistanceMeter dm;
    CarController cc;

    private void Start()
    {
        lc = FindObjectOfType<LiquidCounter>();
        lc.waterCountChange.AddListener(UpdateWaterCountText);

        dm = FindObjectOfType<DistanceMeter>();
        dm.distanceCountChange.AddListener(UpdateDistanceProggres);

        cc = FindObjectOfType<CarController>();

        cc.flyTimeEvent.AddListener(FlyTimePopUp);

        CollectableItem.coinsCollected.AddListener(UpdateCoins);

        restartButton.onClick.AddListener(RestartGame);
        restartButton1.onClick.AddListener(RestartGame);
        pauseButton.onClick.AddListener(PauseGame);
        continueButton.onClick.AddListener(UnPauseGame);
        backToMenuButton.onClick.AddListener(BackToMainScreen);
        backToMenuButton1.onClick.AddListener(BackToMainScreen);
        backToMenuButton2.onClick.AddListener(BackToMainScreen);
        nextLevelButton.onClick.AddListener(RestartGame);

        endGamePanel.SetActive(false);

        currentLevelText.text = string.Format("Level {0}", GameController.Instance.gameData.currentLevel);

        UpdateCoins();
        StartGame();
    }

    private void FlyTimePopUp(float time)
    {
        if(time >= 0.5f && time < 0.8f)
        {
            InstantiatePopUp("GOOD !");
        }
        else if(time >= 0.8f && time < 1.25f)
        {
            InstantiatePopUp("AMAZING !");
        }
        else if(time > 1.25f)
        {
            InstantiatePopUp("PERFECT !");
        }
    }

    private void InstantiatePopUp(string text)
    {
        var obj = Instantiate(popup, endGamePanel.transform.parent);
        obj.GetComponent<Text>().text = text;
        Destroy(obj, 2f);
    }

    public void StartGame()
    {
        waterCountImage.color = GameController.Instance.SelectedFluid;
    }

    private void UnPauseGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    private void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    private void GameOver()
    {
        endGamePanel.SetActive(true);
        cc.DestroyCar(0);
        dm.Stop();

        //if (GameController.Instance.playerSettings.bestDistance < Mathf.RoundToInt(dm.distance))
        //{
        //    newRecordText.SetActive(true);
        //    GameController.Instance.playerSettings.bestDistance = Mathf.RoundToInt(dm.distance);
        //}
    }

    private void RestartGame()
    {
        GameController.Instance.loadMode = Mode.StartLevel;
        GameController.Instance.RestartGame();
        Time.timeScale = 1f;
    }

    private void BackToMainScreen()
    {
        GameController.Instance.loadMode = Mode.Menu;
        GameController.Instance.RestartGame();
        Time.timeScale = 1f;
    }

    public void UpdateWaterCountText(int value)
    {
        if (value <= lc.waterGameoverCount){
            GameOver();
        }

        //update score
        waterCountImage.fillAmount = value / 50f;
    }

    public void UpdateDistanceProggres(int value)
    {
        distanceProggresImage.fillAmount = dm.ProgressDistance;
    }

    public void UpdateCoins()
    {
        coinsCounter.text = GameController.Instance.gameData.coins.ToString();
    }

    public void FinishRace()
    {
        victoryPanel.SetActive(true);
    }
}
