using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

    public static SoundController Instance { get; private set; }
    public void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }

    [SerializeField] AudioClip coinsClip;
    [SerializeField] List<AudioClip> crashClips;

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource engineSource;

    GameController GC;

    private void Start()  {
        GC = GetComponent<GameController>();
    }

    public void PlayCoinsSound()
    {
        if (GC.gameData.audioOn)
            musicSource.PlayOneShot(coinsClip);
    }

    public void PlayCrashSound()
    {
        if(GC.gameData.audioOn)
            musicSource.PlayOneShot(crashClips[Random.Range(0, crashClips.Count)], 0.8f);
    }

    public void Update()
    {
        if(!GC.raceOver && GC.carController && GC.gameData.audioOn)
        {
            if (!engineSource.isPlaying) engineSource.Play();
            engineSource.pitch = Lerp(0.15f, 1.6f, GC.carController.CarSpeed);
        }
        else
        {
            if (engineSource.isPlaying) engineSource.Stop();
        }
    }

    private float Lerp(float from, float to, float value)
    {
        return (1.0f - value) * from + value * to;
    }
}
