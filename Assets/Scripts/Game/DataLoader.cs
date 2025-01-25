using UnityEngine;

public class  DataLoader : MonoBehaviour
{
    public PlayerData PlayerData = new();


    public void LoadFromPrefs()
    {
        var data = PlayerPrefs.GetString("data", string.Empty);

        if (string.IsNullOrEmpty(data))
            return;

        PlayerData = JsonUtility.FromJson<PlayerData>(data);
    }

    public void SaveToPrefs(int currentLevel, int bg, int egg, bool music)
    {
        PlayerData = new PlayerData
        {
            id = PlayerData.id++,
            level = currentLevel,
            bg = bg,
            egg = egg,
            music = music
        };

        var data = JsonUtility.ToJson(PlayerData);
        PlayerPrefs.SetString("data", data);
        PlayerPrefs.Save();
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
