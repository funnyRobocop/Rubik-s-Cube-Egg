using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using RubiksCubeEgg.Game;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    public int levelNumber;
    public SideBallContainer forwardBallList;
    public SideBallContainer backBallList;
    public SideBallContainer leftBallList;
    public SideBallContainer rightBallList;

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
        var destination = "D:/level.txt";
        FileStream file;

        if(File.Exists(destination)) 
            file = File.OpenWrite(destination);
        else    
            file = File.Create(destination);

        var color = levelNumber + " forward: ";
        foreach (var item in forwardBallList.Balls)
            color += item.Color + " ";

        color += "back: ";
        foreach (var item in backBallList.Balls)
            color += item.Color + " ";

        color += "left: ";
        foreach (var item in leftBallList.Balls)
            color += item.Color + " ";

        color += "right: ";
        foreach (var item in rightBallList.Balls)
            color += item.Color + " ";

        var data = new LevelDataSerialised(levelNumber, color);
        var formatter = new BinaryFormatter();
        formatter.Serialize(file, data);
        file.Close();
    }
}
