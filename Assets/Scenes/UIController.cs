using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [SerializeField] Text waterCountText;
    [SerializeField] Text distanceProggresText;

    LiquidCounter lc;
    DistanceMeter dm;
    private void Start()
    {
        lc = FindObjectOfType<LiquidCounter>();
        lc.waterCountChange.AddListener(UpdateWaterCountText);

        dm = FindObjectOfType<DistanceMeter>();
        dm.distanceCountChange.AddListener(UpdateDistanceProggres);
    }

    public void UpdateWaterCountText(int value)
    {
        waterCountText.text = string.Format("{0}/50 L", value);
    }

    public void UpdateDistanceProggres(int value)
    {
        distanceProggresText.text = string.Format("{0} M", value);
    }

}
