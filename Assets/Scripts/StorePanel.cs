using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class StorePanel : MonoBehaviour {

    [SerializeField] Text coinsTextCar;
    [SerializeField] Text coinsTextFluid;
    [SerializeField] Button buyCarButton;
    [SerializeField] Button buyFluidButton;
    [SerializeField] Button backButton;

    [SerializeField] RectTransform fluidContent;
    [SerializeField] RectTransform carsContent;

    private void Start()
    {
        buyCarButton.onClick.AddListener(BuyNewCar);
        buyFluidButton.onClick.AddListener(BuyNewFluid);
        backButton.onClick.AddListener(Back);
    }

    private void UpdateContents()
    {
        UpdateContent(GameController.Instance.gameData.storeCars, carsContent);
        UpdateContent(GameController.Instance.gameData.storeFluids, fluidContent);
    }

    private void UpdateContent(List<StoreItem> list, RectTransform content)
    {
        for (int i = 0; i < list.Count; i++)
        {
            var child = content.GetChild(i);
            var item = child.GetComponent<StoreItemButton>();
            item.SetButton(list[child.GetSiblingIndex()].isUnlocked, list[child.GetSiblingIndex()].isSelected);
        }
    }

    public void Open()
    {
        gameObject.SetActive(true);
        UpdateCoins();
        UpdateContents();
        SnapElements();

        CheckForUnclockedContent();
    }

    private void CheckForUnclockedContent()
    {
        if (AllContentUnlocked(GameController.Instance.gameData.storeCars))
        {
            var parent = coinsTextCar.transform.parent;
            for (int i = 0; i < parent.childCount; i++)
                if (i > 0) parent.GetChild(i).gameObject.SetActive(false);

            parent.GetChild(0).GetComponent<Text>().text = "MORE CARS COMMING SOON...";

        }

        if (AllContentUnlocked(GameController.Instance.gameData.storeFluids))
        {
            var parent = coinsTextFluid.transform.parent;
            for (int i = 0; i < parent.childCount; i++)
                if (i > 0) parent.GetChild(i).gameObject.SetActive(false);

            parent.GetChild(0).GetComponent<Text>().text = "MORE FLUID COMMING SOON...";
        }
    }

    private void SnapElements()
    {
        var data = GameController.Instance.gameData;
        for (int i = 0; i < data.storeCars.Count; i++)
            if (data.storeCars[i].isSelected)
            {
                SnapToElement(i, carsContent);
                break;
            }

        for (int i = 0; i < data.storeFluids.Count; i++)
            if (data.storeFluids[i].isSelected)
            {
                SnapToElement(i, fluidContent);
                break;
            }
    }

    private void Back()  {
        gameObject.SetActive(false);
        GameController.Instance.SetCarProp();
        GetComponentInParent<GameUIController>().UpdateCoins();
    }

    public void UpdateCoins()
    {
        var coins = GameController.Instance.gameData.coins;
        coinsTextCar.text = string.Format("{0}/{1}", coins, 300);
        coinsTextFluid.text = string.Format("{0}/{1}", coins, 150);
    }

    public void BuyNewCar()
    {
        var data = GameController.Instance.gameData;
        if (data.coins < 300) return;

        bool allUnlocked = AllContentUnlocked(data.storeCars);
        if (allUnlocked) return;

        var rand = 0;
        while (data.storeCars[rand].isUnlocked == true)
        {
            rand++;
        }

        Reset(StoreItemButton.ItemType.Car);

        data.storeCars[rand].isUnlocked = true;
        data.storeCars[rand].isSelected = true;
        data.coins -= 300;

        UpdateContent(GameController.Instance.gameData.storeCars, carsContent);
        SnapToElement(rand, carsContent);
        UpdateCoins();

        CheckForUnclockedContent();

        Data.SaveSettings();
    }

    private bool AllContentUnlocked(List<StoreItem> list)
    {
        bool allUnlocked = true;
        for (int i = 0; i < list.Count; i++)
        {
            if (!list[i].isUnlocked)
            {
                allUnlocked = false;
                break;
            }
        }

        return allUnlocked;
    }

    public void BuyNewFluid()
    {
        var data = GameController.Instance.gameData;
        if (data.coins < 150) return;

        bool allUnlocked = AllContentUnlocked(data.storeFluids);
        if (allUnlocked) return;

        var rand = 0;
        while (data.storeFluids[rand].isUnlocked == true)
        {
            rand++;
        }

        Reset(StoreItemButton.ItemType.Fluid);

        data.storeFluids[rand].isUnlocked = true;
        data.storeFluids[rand].isSelected = true;
        data.coins -= 150;

        UpdateContent(GameController.Instance.gameData.storeFluids, fluidContent);
        SnapToElement(rand, fluidContent);
        UpdateCoins();

        CheckForUnclockedContent();

        Data.SaveSettings();
    }

    private void SnapToElement(int index, Transform content)
    {
        var rect = content.GetComponentInParent<ScrollRect>();
        float normalizePosition = (float)content.GetChild(index).GetSiblingIndex() /
                                  (float)content.childCount;
        rect.horizontalNormalizedPosition = normalizePosition;
    }

    private void Reset(StoreItemButton.ItemType type)
    {
        var data = GameController.Instance.gameData;
        switch (type)
        {
            case StoreItemButton.ItemType.Car:
                for (int i = 0; i < data.storeCars.Count; i++)
                    data.storeCars[i].isSelected = false;
                break;
            case StoreItemButton.ItemType.Fluid:
                for (int i = 0; i < data.storeFluids.Count; i++)
                    data.storeFluids[i].isSelected = false;
                break;
        }
    }

    public void SelectCar(int index)
    {
        Reset(StoreItemButton.ItemType.Car);

        GameController.Instance.gameData.storeCars[index].isSelected = true;
        UpdateContent(GameController.Instance.gameData.storeCars, carsContent);

        Data.SaveSettings();
    }

    public void SelectFluid(int index)
    {
        Reset(StoreItemButton.ItemType.Fluid);

        GameController.Instance.gameData.storeFluids[index].isSelected = true;
        UpdateContent(GameController.Instance.gameData.storeFluids, fluidContent);

        Data.SaveSettings();
    }
}
