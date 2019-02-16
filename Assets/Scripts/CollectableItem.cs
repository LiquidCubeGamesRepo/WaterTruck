using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectableItem : MonoBehaviour {

    public static UnityEvent coinsCollected = new UnityEvent();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameController.Instance.gameData.coins++;
        coinsCollected.Invoke();
        Destroy(this.gameObject);
    }
}
