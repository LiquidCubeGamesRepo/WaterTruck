using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [SerializeField] Text waterCountText;

    LiquidCounter lc;
    private void Start()
    {
        lc = FindObjectOfType<LiquidCounter>();
        lc.waterCountChange.AddListener(UpdateWaterCountText);
    }


    public void UpdateWaterCountText(int value)
    {
        waterCountText.text = string.Format("{0}/50 L", value);
    }
}
