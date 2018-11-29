using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaterParticle : MonoBehaviour {

    public class WaterDestroyEvent : UnityEvent<WaterParticle> { }
    public static WaterDestroyEvent onWaterDestroy = new WaterDestroyEvent();

    private void OnBecameInvisible()
    {
        onWaterDestroy.Invoke(this);
        Destroy(this.gameObject);
    }
}
