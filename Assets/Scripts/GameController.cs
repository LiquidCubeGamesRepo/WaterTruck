using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum Mode
{
    Menu,
    StartLevel
}

public class GameController : MonoBehaviour {

    public static GameController Instance { get; private set; }
    public void Awake(){
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);

        Data.LoadSettings();
    }

    public GameData gameData;
    public Mode loadMode;
    public bool IsGamePlaying { get; set; }
    public UnityEvent startGameEvent;

    public bool canStart = false;
    public bool raceOver = false;
    [SerializeField] Transform target;
    [SerializeField] Material liquidMaterial;
    [SerializeField] public Sprite[] carSprites;
    [SerializeField] Color[] carColors;
    [SerializeField] AudioSource audioSource;
    CarController carController;

    public Sprite SelectedCar
    {
        get { return carSprites[gameData.SelectedCar]; }
    }
    public Color SelectedFluid
    {
        get { return carColors[gameData.SelectedFluid]; }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(loadMode == Mode.StartLevel)
        {
            StartGame();
            IsGamePlaying = false;
        }
        else
        {
            IsGamePlaying = true;
        }

        Init();
    }

    void OnDisable()  {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void Init(){
        
        raceOver = false;
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
        carController.ChangeCar();
        liquidMaterial.SetColor("_Color", SelectedFluid);
    }

    private IEnumerator MoveToTarget()
    {
        canStart = false;
        //start game noise 
        yield return new WaitForSeconds(0.5f);

        var dist = Vector2.Distance(carController.transform.position, target.position);

        while (dist > 0.25f)
        {
            dist = Vector2.Distance(carController.transform.position, target.position);
            carController.transform.position = Vector2.Lerp(carController.transform.position, target.position, Time.deltaTime * 5f);
            yield return new WaitForEndOfFrame();
        }

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

    public void FinishRace()
    {
        gameData.currentLevel++;
        carController.CanMove = false;
        raceOver = true;
        Data.SaveSettings();
    }
}
