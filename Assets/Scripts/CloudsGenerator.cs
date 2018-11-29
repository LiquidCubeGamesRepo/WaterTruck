using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsGenerator : MonoBehaviour {

    [SerializeField] GameObject cloudTemplate;
    [SerializeField] List<Sprite> cloudSprites;
    [SerializeField] Transform canvasParent;
    [SerializeField] float startX;
    [SerializeField] float maxY;
    [SerializeField] float minY;
    [SerializeField] float timeBetweenSpawns;

    private List<BackgroundElement> clouds = new List<BackgroundElement>();
    private CarController car;

    // Use this for initialization
    void Start () {
        car = FindObjectOfType<CarController>();
        GameController.Instance.startGameEvent.AddListener(StartGame);
    }

    private void StartGame()
    {
        StartCoroutine(InstantiateClouds());
    }

    private IEnumerator InstantiateClouds()
    {
        while (true)
        {
            var pos = new Vector2(startX, Random.Range(minY, maxY));
            var cloud = Instantiate(cloudTemplate, canvasParent);
            cloud.GetComponent<UnityEngine.UI.Image>().sprite = cloudSprites[Random.Range(0, cloudSprites.Count)];
            var cloundRect = cloud.GetComponent<RectTransform>();
            cloundRect.localPosition = pos;

            BackgroundElement newCloud = new BackgroundElement();
            newCloud.rect = cloundRect;
            newCloud.speed = Random.Range(100f, 200f);
            clouds.Add(newCloud);

            yield return new WaitForSeconds(timeBetweenSpawns *  Mathf.Clamp(1-car.CarSpeed, 0.1f, 1f));
        }
    }

    // Update is called once per frame
    void Update () {

        for (int i = 0; i < clouds.Count; i++)
        {
            if (clouds[i].rect.localPosition.x < -startX) { 
                Destroy(clouds[i].rect.gameObject);
                clouds.RemoveAt(i);
                break;
            }

            if(car.isVisible)
                clouds[i].rect.localPosition -= (Vector3.right * car.CarSpeed * clouds[i].speed * Time.deltaTime);
        }
	}
}

public class BackgroundElement
{
    public RectTransform rect;
    public float speed;
}
