using System.Collections.Generic;
using UnityEngine;

public class  DataLoader : MonoBehaviour
{
    public PlayerData PlayerData { get; private set; } = new();

    public void Load()
    {
        var data = string.Empty;
#if UNITY_WEBGL
        data = YG2.saves.saves;
#else
        data = PlayerPrefs.GetString("data", string.Empty);
#endif
        if (string.IsNullOrEmpty(data))
        {
                Save(0,0,2,true, new List<int>());
                return;
        }
        PlayerData = JsonUtility.FromJson<PlayerData>(data);
    }

    public void Save(int currentLevel, int bg, int egg, bool music, List<int> skipped)
    {
        PlayerData.id++;
        PlayerData = new PlayerData
        {
            id = PlayerData.id,
            level = currentLevel,
            bg = bg,
            egg = egg,
            music = music,
            skipped = new List<int>(skipped)
        };

        var data = JsonUtility.ToJson(PlayerData);

#if UNITY_WEBGL
        YG2.saves.saves = data;
        YG2.SaveProgress();
#else
        PlayerPrefs.SetString("data", data);
        PlayerPrefs.Save();
#endif
        Debug.Log(data);
    }
}

[System.Serializable]
public class PlayerData
{
    public int id;
    public int level;
    public int bg;
    public int egg;
    public bool music;
    public List<int> skipped;
}
