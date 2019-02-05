using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchivmentCheck : MonoBehaviour {
    [SerializeField] AchievementUnlockedPopup popup;
	
    // Use this for initialization
	public void Start () {
        FindObjectOfType<DistanceMeter>().distanceCountChange.AddListener(CheckDistance);
    }

    private void CheckDistance(int dist)
    {
        //if (dist >= 1000)
        //{
        //    if (GameController.Instance.playerSettings.achievements[3].isUnlocked == false)
        //    {
        //        GameController.Instance.playerSettings.achievements[3].isUnlocked = true;
        //        popup.StartAchiv();
        //    }
        //}
        //else if (dist >= 500)
        //{
        //    if (GameController.Instance.playerSettings.achievements[2].isUnlocked == false)
        //    {
        //        GameController.Instance.playerSettings.achievements[2].isUnlocked = true;
        //        popup.StartAchiv();
        //    }
        //}
        //else if(dist >= 200)
        //{
        //    if (GameController.Instance.playerSettings.achievements[1].isUnlocked == false)
        //    {
        //        GameController.Instance.playerSettings.achievements[1].isUnlocked = true;
        //        popup.StartAchiv();
        //    }
        //}
        //else if(dist >= 100)
        //{
        //    if(GameController.Instance.playerSettings.achievements[0].isUnlocked == false) { 
        //        GameController.Instance.playerSettings.achievements[0].isUnlocked = true;
        //        popup.StartAchiv();
        //    }
        //}
    }
}
