using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestrictScrollView : MonoBehaviour {

    [SerializeField] float clampSpeed;

    RectTransform rect;
    float maxX;

    private void Start() {
        rect = GetComponent<RectTransform>();
        var layout = GetComponent<HorizontalLayoutGroup>();
        maxX = layout.spacing * (transform.childCount-1) + 100 * transform.childCount - layout.padding.left;
        Debug.Log(maxX);
    }

    void Update () {

        if (rect.localPosition.x > 0)
            rect.localPosition = new Vector2(Mathf.Lerp(rect.localPosition.x, 0, Time.deltaTime * clampSpeed), 0);
        else if (rect.localPosition.x < -maxX)
            rect.localPosition = new Vector2(Mathf.Lerp(rect.localPosition.x, -maxX, Time.deltaTime * clampSpeed), 0);
    }
}
