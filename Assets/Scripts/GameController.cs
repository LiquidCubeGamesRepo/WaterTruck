using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static GameController Instance { get; private set; }
    public void Awake(){
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);

        Data.LoadSettings();
    }

    public GameData gameData;

    public bool IsGamePlaying { get; set; }
    public UnityEvent startGameEvent;

    public bool canStart = false;
    [SerializeField] Transform target;
    [SerializeField] Material liquidMaterial;
    [SerializeField] public Sprite[] carSprites;
    [SerializeField] Color[] carColors;
    [SerializeField] AudioSource audioSource;
    CarController carController;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Init();
    }

    void OnDisable()  {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void Init(){
        IsGamePlaying = false;
        carController = FindObjectOfType<CarController>();
        target = FindObjectOfType<CenterTarget>().transform;

        StartCoroutine(MoveToTarget());
        SetCarProp();

        if (gameData.audioOn) { 
            if(!audioSource.isPlaying)
                audioSource.Play();
        }
        else
            audioSource.Stop();
    }

    public void SetCarProp()
    {
        //carController.ChangeCar();
        //liquidMaterial.SetColor("_Color", carColors[gameData.currentLiquid]);
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
        IsGamePlaying = true;
        startGameEvent.Invoke();
    }

    public void RestartGame(){
        SceneManager.LoadScene("Map");
        Init();
    }

    public bool ToggleSound()
    {
        gameData.audioOn = !gameData.audioOn;

        if (gameData.audioOn) audioSource.Play();
        else audioSource.Stop();

        return gameData.audioOn;
    }
}
