using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainGenerator : MonoBehaviour {

    [SerializeField] Transform canvasParent;
    [SerializeField] GameObject farMountainPrefab;
    [SerializeField] GameObject closeMountainPrefab;
    [SerializeField] Vector2 startPos;
    [SerializeField] float mountainSpeed = 100f;

    BackgroundElement currentFarMountain;
    BackgroundElement currentCloseMountain;

    BackgroundElement prevFarMountain;
    BackgroundElement prevCloseMountain;

    private CarController car;

    private void Start() {
        car = FindObjectOfType<CarController>();

        InstantiateFarMountain(startPos);
        InstantiateCloseMountain(startPos);
    }

    private void InstantiateFarMountain(Vector2 pos){
        currentFarMountain = new BackgroundElement();
        var fMount = Instantiate(farMountainPrefab, canvasParent);
        currentFarMountain.rect = fMount.GetComponent<RectTransform>();
        currentFarMountain.rect.localPosition = pos;
        currentFarMountain.speed = mountainSpeed * 0.5f;
        currentFarMountain.rect.SetAsFirstSibling();
    }

    private void InstantiateCloseMountain(Vector2 pos){
        currentCloseMountain = new BackgroundElement();
        var fMount = Instantiate(closeMountainPrefab, canvasParent);
        currentCloseMountain.rect = fMount.GetComponent<RectTransform>();
        currentCloseMountain.rect.localPosition = pos;
        currentCloseMountain.speed = mountainSpeed;
    }

    private void Update()
    {
        if(currentFarMountain.rect.localPosition.x < -startPos.x && currentFarMountain.rect.localPosition.x > -1350f)
        {
            if(prevFarMountain == null) {
                prevFarMountain = currentFarMountain;
                InstantiateFarMountain(new Vector2(currentFarMountain.rect.rect.width + currentFarMountain.rect.localPosition.x, -347f));
            }
        }
        else if(prevFarMountain != null)
        {
            if(prevFarMountain.rect.localPosition.x < -1388f) { 
                Destroy(prevFarMountain.rect.gameObject);
                prevFarMountain = null;
            }
        }

        if (currentCloseMountain.rect.localPosition.x < -startPos.x && currentCloseMountain.rect.localPosition.x > -1350f)
        {
            if (prevCloseMountain == null)
            {
                prevCloseMountain = currentCloseMountain;
                InstantiateCloseMountain(new Vector2(currentCloseMountain.rect.rect.width + currentCloseMountain.rect.localPosition.x, -347f));
            }
        }
        else if (prevCloseMountain != null)
        {
            if (prevCloseMountain.rect.localPosition.x < -1388f)
            {
                Destroy(prevCloseMountain.rect.gameObject);
                prevCloseMountain = null;
            }
        }

        if (car.isVisible)
        {
            currentFarMountain.rect.localPosition -= (Vector3.right * car.CarSpeed * currentFarMountain.speed * Time.deltaTime);
            if(prevFarMountain != null) prevFarMountain.rect.localPosition -= (Vector3.right * car.CarSpeed * currentFarMountain.speed * Time.deltaTime);

            currentCloseMountain.rect.localPosition -= (Vector3.right * car.CarSpeed * currentCloseMountain.speed * Time.deltaTime);
            if (prevCloseMountain != null) prevCloseMountain.rect.localPosition -= (Vector3.right * car.CarSpeed * prevCloseMountain.speed * Time.deltaTime);
            // currentCloseMountain.rect.localPosition -= (Vector3)(Vector2.right * car.CarSpeed * currentCloseMountain.speed * Time.deltaTime);
        }
    }
}
