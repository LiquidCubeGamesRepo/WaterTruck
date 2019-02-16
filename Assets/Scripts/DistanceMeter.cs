using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DistanceMeter : MonoBehaviour {

    [Serializable]
    public class DistanceCountChange : UnityEvent<int> { }
    public DistanceCountChange distanceCountChange;

    public float ProgressDistance { get { return Mathf.Clamp(distance / moduleFinishDistance,0,1); } }

    public float distance = 0;
    public float moduleFinishDistance;

    void Start(){
        GameController.Instance.startGameEvent.AddListener(StartGame);
    }

    void StartGame(){
        StartCoroutine(CountDistance());
    }

    private IEnumerator CountDistance()
    {
        while (true)
        {
            var pos = new Vector2(transform.position.x, 0f);
            distance = Vector2.Distance(pos, Vector2.zero);
            distanceCountChange.Invoke(Mathf.RoundToInt(distance));
            yield return new WaitForSeconds(0.25f);
        }
    }

    public void Stop(){
        StopAllCoroutines();
    }
}
