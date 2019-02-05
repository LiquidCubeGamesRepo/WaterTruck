using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementElement : MonoBehaviour {

    [SerializeField] Button claimButton;
    [SerializeField] GameObject locked;
    [SerializeField] GameObject claimed;
    [SerializeField] int reward = 10;

    void Start () {
        CheckStatus();
        claimButton.onClick.AddListener(ClaimReward);
	}

    private void ClaimReward()
    {
        //GameController.Instance.playerSettings.achievements[transform.GetSiblingIndex()].isClaimed = true;
        //GameController.Instance.playerSettings.coins += reward;
        CheckStatus();
    }

    public void CheckStatus()
    {
        //var achiv = GameController.Instance.playerSettings.achievements;

        //if (achiv[transform.GetSiblingIndex()].isClaimed)
        //{
        //    claimButton.gameObject.SetActive(false);
        //    locked.SetActive(false);
        //    claimed.SetActive(true);
        //}
        //else if (achiv[transform.GetSiblingIndex()].isUnlocked)
        //{
        //    claimButton.gameObject.SetActive(true);
        //    locked.SetActive(false);
        //    claimed.SetActive(false);
        //}
        //else
        //{
        //    claimButton.gameObject.SetActive(false);
        //    locked.SetActive(true);
        //    claimed.SetActive(false);
        //}
    }
}
