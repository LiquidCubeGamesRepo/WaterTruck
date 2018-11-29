using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuUIController : MonoBehaviour {

    [SerializeField] Text bestDistanceText;
    [SerializeField] GameObject MenuCanvas;
    [SerializeField] GameObject GameCanvas;

    GameController GC;
    public void Start(){
        GC = FindObjectOfType<GameController>();

        MenuCanvas.SetActive(true);
        GameCanvas.SetActive(false);
        UpdateBestDistanceText();
    }

    public void Update()
    {
        if (!GC.IsGamePlaying) { 
#if UNITY_EDITOR
        if (Input.GetButton("Accel"))
            StartGame();
#elif UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            StartGame();
        }
#endif
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
        bestDistanceText.text = "Best Distance: " + GameController.Instance.playerSettings.bestDistance;
    }

}
