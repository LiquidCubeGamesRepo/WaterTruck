using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LiquidCounter : MonoBehaviour {

    [SerializeField] Transform box;
    [SerializeField] float countPrecisionTime = 0.25f;

    [Serializable]
    public class WaterCountChange : UnityEvent<int> { }
    public WaterCountChange waterCountChange;

    void Start () {
        GameController.Instance.startGameEvent.AddListener(StartGame);
	}

    void StartGame(){
        StartCoroutine(CountLiquid());
    }

    Collider2D []cols = new Collider2D[50];
    private IEnumerator CountLiquid()
    {
        while (true)
        {
            var waterCount = Physics2D.OverlapBoxNonAlloc(box.position, box.lossyScale, 0, cols, (1 << 9));
            waterCountChange.Invoke(waterCount);

            yield return new WaitForSeconds(countPrecisionTime);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(box.position, box.lossyScale);
    }

}
