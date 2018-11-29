using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpawner : MonoBehaviour {

    [SerializeField] Transform point;
    [SerializeField] GameObject waterPrefab;
    [SerializeField] float waterSpawnTime;

	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnWater());
	}

    private IEnumerator SpawnWater()
    {
        while (true)
        {
            var pos = point.position;
            pos.x += Random.Range(-0.1f, 0.1f);
            pos.y += Random.Range(-0.1f, 0.1f);
            Instantiate(waterPrefab, pos, Quaternion.identity);
            yield return new WaitForSeconds(waterSpawnTime);
        }

    }
}
