using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Linq;

public class Data {

    public static void SaveSettings()
    {
        Debug.LogWarning("Saving Data...");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/data.dat");

        bf.Serialize(file, GameController.Instance.gameData);
        file.Close();
    }

    public static void LoadSettings()
    {
        Debug.LogWarning("Loading Data...");
        if (File.Exists(Application.persistentDataPath + "/data.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/data.dat", FileMode.Open);
            GameData data = (GameData)bf.Deserialize(file);

            GameController.Instance.gameData = data;
            file.Close();
        }
        else
        {
            GameData data = new GameData();

            data.audioOn = false;
            data.coins = 300;

            data.storeCars = new List<StoreItem>();
            for (int i = 0; i < 3; i++)
            {
                var item = new StoreItem();
                if(i == 0)
                {
                    item.isSelected = true;
                    item.isUnlocked = true;
                }
                data.storeCars.Add(item);
            }

            data.storeFluids = new List<StoreItem>();
            for (int i = 0; i < 6; i++)
            {
                var item = new StoreItem();
                if (i == 0)
                {
                    item.isSelected = true;
                    item.isUnlocked = true;
                }
                data.storeFluids.Add(item);
            }

            GameController.Instance.gameData = data;

            SaveSettings();
        }
    }
}

[Serializable]
public class GameData
{
    public int coins;
    public bool audioOn;
    public int currentLevel;

    public List<StoreItem> storeCars;
    public List<StoreItem> storeFluids;

    public int SelectedCar
    {
       get { return storeCars.IndexOf(storeCars.SingleOrDefault(x => x.isSelected)); }
    }
    public int SelectedFluid
    {
        get { return storeFluids.IndexOf(storeFluids.SingleOrDefault(x => x.isSelected)); }
    }
}

[Serializable]
public class StoreItem
{
    public bool isUnlocked;
    public bool isSelected;
}