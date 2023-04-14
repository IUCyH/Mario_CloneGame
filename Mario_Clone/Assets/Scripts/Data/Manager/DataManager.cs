using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletonMonoBehaviour<DataManager>
{
    const string PlayerJsonData = "PlayerData";
    PlayerData playerData;

    public void IncreaseCoin(uint coin)
    {
        playerData.coin += coin;
    }

    public void DecreaseCoin(uint coin)
    {
        if (playerData.coin < 1) return;
        
        playerData.coin -= coin;
    }

    void Save()
    {
        var jsonData = JsonUtility.ToJson(playerData);
        PlayerPrefs.SetString(PlayerJsonData, jsonData);
        PlayerPrefs.Save();
    }

    void Load()
    {
        var jsonData = PlayerPrefs.GetString(PlayerJsonData, string.Empty);

        if (!string.IsNullOrEmpty(jsonData))
        {
            playerData = JsonUtility.FromJson<PlayerData>(jsonData);
        }
        else
        {
            CreateNewPlayerData();
        }
        
        Save();
    }

    void CreateNewPlayerData()
    {
        playerData = new PlayerData
        {
            topRecord = 0f,
            coin = 0
        };
    }
    
    // Start is called before the first frame update
    protected override void OnStart()
    {
        Load();
    }
}
