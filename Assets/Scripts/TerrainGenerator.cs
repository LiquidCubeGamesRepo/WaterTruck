using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{

    [SerializeField] Transform canvasParent;
    [SerializeField] GameObject terrainPrefab;
    [SerializeField] Vector2 startPos;
    [SerializeField] float terrainSpeed = 300f;

    BackgroundElement currentTerrain;
    BackgroundElement prevTerrain;

    private CarController car;

    // Use this for initialization
    void Start()
    {
        car = FindObjectOfType<CarController>();

        InstantiateTerrain(startPos);
    }

    private void InstantiateTerrain(Vector2 startPos)
    {
        currentTerrain = new BackgroundElement();
        var fMount = Instantiate(terrainPrefab, canvasParent);
        currentTerrain.rect = fMount.GetComponent<RectTransform>();
        currentTerrain.rect.localPosition = startPos;
        currentTerrain.speed = terrainSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTerrain.rect.localPosition.x < -startPos.x && currentTerrain.rect.localPosition.x > -1375f)
        {
            if (prevTerrain == null)
            {
                prevTerrain = currentTerrain;
                InstantiateTerrain(new Vector2(currentTerrain.rect.rect.width+currentTerrain.rect.localPosition.x, -528f));
            }
        }
        else if (prevTerrain != null)
        {
            if (prevTerrain.rect.localPosition.x < -1375f)
            {
                Destroy(prevTerrain.rect.gameObject);
                prevTerrain = null;
            }
        }

        if (car.isVisible && car.CanMove)
        {
            currentTerrain.rect.localPosition -= (Vector3.right * car.CarSpeed * currentTerrain.speed * Time.deltaTime);
            if (prevTerrain != null) prevTerrain.rect.localPosition -= (Vector3.right * car.CarSpeed * prevTerrain.speed * Time.deltaTime);
        }
    }
}
