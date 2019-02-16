using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LiquidCounter : MonoBehaviour {

    [SerializeField] Transform box;
    [SerializeField] float countPrecisionTime = 0.25f;
    [SerializeField] GameObject waterMetaball;
    [SerializeField] Transform waterMetaballParent;
    [SerializeField] int waterMaxBalls = 50;
    [SerializeField] public int waterGameoverCount = 5;

    [Serializable]
    public class WaterCountChange : UnityEvent<int> { }
    public WaterCountChange waterCountChange;

    List<WaterParticle> waterList = new List<WaterParticle>();

    void Start () {
        GameController.Instance.startGameEvent.AddListener(StartGame);
        WaterParticle.onWaterDestroy.AddListener(WaterParticleDestroyed);

        for (int i = 0; i < waterMaxBalls; i++){
            waterList.Add(Instantiate(waterMetaball, waterMetaballParent).GetComponent<WaterParticle>());
        }
        waterCountChange.Invoke(waterList.Count);
    }

    private void WaterParticleDestroyed(WaterParticle water)
    {
        if (GameController.Instance.raceOver) return;

        waterList.Remove(water);
        waterCountChange.Invoke(waterList.Count);
    }

    void StartGame(){

        var balls = Physics2D.OverlapBoxAll(waterMetaballParent.transform.position, new Vector2(1.7f, 1.7f), 0, (1 << 9));
        //Check for water balls at start
        for (int i = 0; i < waterMaxBalls - balls.Length; i++){
            waterList.Add(Instantiate(waterMetaball, waterMetaballParent).GetComponent<WaterParticle>());
        }
        waterCountChange.Invoke(waterList.Count);
    }
}
