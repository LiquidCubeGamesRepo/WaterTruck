using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour {

    [SerializeField] GameObject pausePanel;

    [SerializeField] Button restartButton;
    [SerializeField] Button pauseButton;

    [SerializeField] GameObject newRecordText;
    [SerializeField] Text distaneReachedText;
    [SerializeField] Text bestDistanceText;

    [SerializeField] Text waterCountText;
    [SerializeField] Text distanceProggresText;

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
        
        restartButton.onClick.AddListener(RestartGame);
        pauseButton.onClick.AddListener(PauseGame);

        pausePanel.SetActive(false);
        newRecordText.SetActive(false);
    }

    private void PauseGame()
    {
        pausePanel.SetActive(true);
        cc.DestroyCar(0);
        dm.Stop();

        bestDistanceText.text = "Best Distance: " + GameController.Instance.playerSettings.bestDistance;
        distaneReachedText.text = "Distance: " + Mathf.RoundToInt(dm.distance);

        if (GameController.Instance.playerSettings.bestDistance < Mathf.RoundToInt(dm.distance))
        {
            newRecordText.SetActive(true);
            GameController.Instance.playerSettings.bestDistance = Mathf.RoundToInt(dm.distance);
        }
    }

    private void RestartGame()
    {
        GameController.Instance.RestartGame();
    }

    public void UpdateWaterCountText(int value)
    {
        if (value <= lc.waterGameoverCount){
            PauseGame();
        }

        //update score
        waterCountText.text = string.Format("{0}/50 L", value);
    }

    public void UpdateDistanceProggres(int value)
    {
        distanceProggresText.text = string.Format("{0} M", value);
    }

}
