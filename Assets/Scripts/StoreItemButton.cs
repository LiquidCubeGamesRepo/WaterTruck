using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StoreItemButton : MonoBehaviour, IPointerClickHandler {

    public enum ItemType
    {
        Car,
        Fluid
    }

    [SerializeField] ItemType itemType;

    Image img;
    bool interactable = false;

    StorePanel storePanel;
    private void Awake() {
        img = GetComponent<Image>();
        storePanel = GetComponentInParent<StorePanel>();
    }

    public void SetButton(bool unlocked, bool selected)
    {
        if (selected)
        {
            img.color = Color.white;
            interactable = false;
        }
        else if (unlocked)
        {
            img.color = new Color(0.34f, 0.34f, 0.34f);
            interactable = true;
        }
        else
        {
            img.color = Color.black;
            interactable = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!interactable) return;

        switch (itemType)
        {
            case ItemType.Car:
                storePanel.SelectCar(transform.GetSiblingIndex());
                break;
            case ItemType.Fluid:
                storePanel.SelectFluid(transform.GetSiblingIndex());
                break;
        }
    }
}
