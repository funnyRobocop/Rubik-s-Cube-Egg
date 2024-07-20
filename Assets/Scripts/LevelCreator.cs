using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    public int levelNumber;

    [Serializable]
    public class LevelDataSerialised
    {
        public int level;
        public string color;

        public LevelDataSerialised(int level, string color)
        {
            this.level = level;
            this.color = color;
        }
    }

    public void Save()
    {
        var destination = Application.persistentDataPath + "/level.txt";
        FileStream file;

        if(File.Exists(destination)) 
            file = File.OpenWrite(destination);
        else    
            file = File.Create(destination);

        var level = 1;

        var color = level + " forward: ";
        foreach (var item in levels[levelNumber].forwardBallList)
            color += item + " ";

        color += "back: ";
        foreach (var item in levels[levelNumber].backBallList)
            color += item + " ";

        color += "left: ";
        foreach (var item in levels[levelNumber].leftBallList)
            color += item + " ";

        color += "right: ";
        foreach (var item in levels[levelNumber].rightBallList)
            color += item + " ";

        var data = new LevelDataSerialised(1, color);
        var formatter = new BinaryFormatter();
        formatter.Serialize(file, data);
        file.Close();
    }
}
