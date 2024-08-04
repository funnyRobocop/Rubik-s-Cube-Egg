using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using RubiksCubeEgg.Game;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    public int levelNumber;
    public List<Ball> forwardBallList;
    public List<Ball> backBallList;
    public List<Ball> leftBallList;
    public List<Ball> rightBallList;

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
        foreach (var item in forwardBallList)
            color += item + " ";

        color += "back: ";
        foreach (var item in backBallList)
            color += item + " ";

        color += "left: ";
        foreach (var item in leftBallList)
            color += item + " ";

        color += "right: ";
        foreach (var item in rightBallList)
            color += item + " ";

        var data = new LevelDataSerialised(1, color);
        var formatter = new BinaryFormatter();
        formatter.Serialize(file, data);
        file.Close();
    }
}
