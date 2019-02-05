using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementPanel : MonoBehaviour {

    [SerializeField] Button backButton;
    [SerializeField] Transform content;

    private void Start()
    {
        backButton.onClick.AddListener(Back);
    }

    public void Open()
    {
        gameObject.SetActive(true);
        foreach (Transform child in content) {
            var ae = child.GetComponentInChildren<AchievementElement>();
            ae.CheckStatus();
        }

    }

    private void Back()
    {
        gameObject.SetActive(false);
        GetComponentInParent<GameUIController>().UpdateCoins();
    }
}
