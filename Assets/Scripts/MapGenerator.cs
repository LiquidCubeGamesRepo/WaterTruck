using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
public class MapGenerator : MonoBehaviour {

    [SerializeField] Transform player;
    [SerializeField] List<GameObject> ModulesPrefabs;
    [SerializeField] GameObject startModule;
    [SerializeField] GameObject finishModule;
    [SerializeField] float offsetX;
    [SerializeField] float offsetY;
    [SerializeField] float distanceToSpawnModule;
    [SerializeField] float timeToDestroyModule = 1f;

    [Range(0,1)]
    [SerializeField] float collectableChance = 0.3f;

    List<GameObject> modules = new List<GameObject>();

    private Transform modulesParent;
    private int modulesCounter;
    private DistanceMeter distanceMeter;

    private bool finishSpawned;

    public void Start()
    {
        modulesParent = new GameObject("Map").transform;
        for (int i = 0; i < 3; i++)
        {
            GameObject mod = i == 1 ? startModule : ModulesPrefabs[0];

            var module = Instantiate(mod, modulesParent);
            module.transform.position = new Vector2(i * offsetX - offsetX, offsetY);
            modules.Add(module);
            modulesCounter++;
        }

        modulesCounter--;

        GameController.Instance.startGameEvent.AddListener(StartGame);
        player = FindObjectOfType<CarController>().transform;
        distanceMeter = player.GetComponent<DistanceMeter>();

        //Calculate max modules in level
        distanceMeter.moduleFinishDistance = 100 + (GameController.Instance.gameData.currentLevel) * 25;
    }

    private void StartGame(){
        StartCoroutine(DestroyModules());
    }

    private IEnumerator DestroyModules()
    {
        //Give time to read tutorial
        yield return new WaitForSeconds(5f);

        while (!GameController.Instance.raceOver)
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
                    GameObject mod = null;

                    if ((distanceMeter.distance > distanceMeter.moduleFinishDistance - distanceToSpawnModule * 3) && !finishSpawned)
                    {
                        mod = finishModule;
                        finishSpawned = true;
                    }
                    else
                        mod = ModulesPrefabs[Random.Range(0, ModulesPrefabs.Count)];

                    var module = Instantiate(mod, modulesParent);
                    module.transform.position = new Vector2(modulesCounter * offsetX, offsetY);
                    modules.Add(module);
                    modulesCounter++;

                    //Check for spawn collectables
                    if (Random.Range(0, 100) < collectableChance * 100)
                    {
                        var child = module.transform.GetChild(3);
                        var rand = Random.Range(0, child.childCount);
                        for (int i = 0; i < child.childCount; i++)
                        {
                            if (i == rand) child.GetChild(i).gameObject.SetActive(true);
                            else child.GetChild(i).gameObject.SetActive(false);
                        }
                    }
                }
            }
    }
}