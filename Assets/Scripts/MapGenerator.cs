using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
public class MapGenerator : MonoBehaviour {

    [SerializeField] Transform player;
    [SerializeField] List<GameObject> ModulesPrefabs;
    [SerializeField] float offsetX;
    [SerializeField] float offsetY;
    [SerializeField] float distanceToSpawnModule;
    [SerializeField] float timeToDestroyModule = 1f;

    [SerializeField] GameObject collectablePrefab;
    [Range(0,1)]
    [SerializeField] float collectableChance = 0.3f;

    List<GameObject> modules = new List<GameObject>();

    private Transform modulesParent;
    private int modulesCounter;

    public void Start()
    {
        modulesParent = new GameObject("Map").transform;
        for (int i = 0; i < 4; i++)
        {
            var module = Instantiate(ModulesPrefabs[0], modulesParent);
            module.transform.position = new Vector2(i * offsetX - offsetX, offsetY);
            modules.Add(module);
            modulesCounter++;
        }
        modulesCounter--;

        GameController.Instance.startGameEvent.AddListener(StartGame);
        player = FindObjectOfType<CarController>().transform;
    }

    private void StartGame(){
        StartCoroutine(DestroyModules());
    }

    private IEnumerator DestroyModules()
    {
        while (true)
        {
            if (modules.Count > 0)
            { 
                if (modules[0].transform.childCount > 0){
                StartCoroutine(DestroyModulePart(modules[0].transform.GetChild(0)));
                }
                else{
                    Destroy(modules[0]);
                    modules.RemoveAt(0);
                }
            }
            yield return new WaitForSeconds(timeToDestroyModule);
        }
    }

    private IEnumerator DestroyModulePart(Transform part)
    {
        var time = 0f;
        while(time < timeToDestroyModule)
        {
            if (part != null){
                part.position -= Vector3.up * 0.5f;
            }
            yield return new WaitForEndOfFrame();
            time += Time.deltaTime;
        }

        if (part != null)
            Destroy(part.gameObject);
    }


    private void Update()
    {
        if (player.position.y < -8f) return;

        if(modules.Count > 0)
        if (modules[modules.Count - 1]) { 
            var dist = Vector2.Distance(player.position, modules[modules.Count-1].transform.position);
            if (dist < distanceToSpawnModule)
            {
                var module = Instantiate(ModulesPrefabs[Random.Range(0, ModulesPrefabs.Count)], modulesParent);
                module.transform.position = new Vector2(modulesCounter * offsetX, offsetY);
                modules.Add(module);
                modulesCounter++;

                    foreach (Transform child in module.transform)
                    {
                        if (Random.Range(0, 100) < collectableChance * 100)
                        {
                            var sr = child.GetComponent<SpriteRenderer>();
                            var points = child.GetComponent<PolygonCollider2D>().points;
                            var posY = points.Max(x => x.y);

                            Instantiate(collectablePrefab, new Vector2(child.position.x, posY-1.5f), Quaternion.identity);
                        }
                    }

            }
        }
    }
}