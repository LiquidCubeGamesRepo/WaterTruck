using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementUnlockedPopup : MonoBehaviour {

    public void StartAchiv()
    {
        gameObject.SetActive(true);
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
