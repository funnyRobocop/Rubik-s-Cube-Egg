using UnityEngine;

public class  DataLoader : MonoBehaviour
{
    public int ID;
    public PlayerData PlayerData = new();


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public PlayerData LoadFromPrefs()
    {
        var data = PlayerPrefs.GetString("data", string.Empty);

        if (string.IsNullOrEmpty(data))
            return PlayerData;

        PlayerData = JsonUtility.FromJson<PlayerData>(data);
        return PlayerData;
    }

    public void SaveToPrefs(int currentLevel, int choosedLevel, int bg, int egg)
    {
        ID++;
        PlayerData = new PlayerData
        {
            id = ID,
            level = currentLevel,
            bg = bg,
            egg = egg
        };
        string data = JsonUtility.ToJson(PlayerData);
        PlayerPrefs.SetString("data", data);
        PlayerPrefs.Save();
    }
}

[System.Serializable]
public class PlayerData
{
    public int id;
    public int level;
    public int bg;
    public int egg;
}
