using UnityEngine;
using YG;

public class  DataLoader : MonoBehaviour
{
    public PlayerData PlayerData = new();

    public void Load()
    {
        var data = string.Empty;
#if UNITY_WEBGL
        data = YandexGame.savesData.data;
#else
        data = PlayerPrefs.GetString("data", string.Empty);
#endif
        if (string.IsNullOrEmpty(data))
        {
            PlayerData.music = true;
            return;
        }
        PlayerData = JsonUtility.FromJson<PlayerData>(data);
    }

    public void Save(int currentLevel, int bg, int egg, bool music)
    {
        PlayerData.id++;
        PlayerData = new PlayerData
        {
            id = PlayerData.id,
            level = currentLevel,
            bg = bg,
            egg = egg,
            music = music
        };

        var data = JsonUtility.ToJson(PlayerData);

#if UNITY_WEBGL
        YandexGame.savesData.data = data;
        YandexGame.SaveProgress();
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
}
