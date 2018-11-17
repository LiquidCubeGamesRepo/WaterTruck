using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterExitView : MonoBehaviour {

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
