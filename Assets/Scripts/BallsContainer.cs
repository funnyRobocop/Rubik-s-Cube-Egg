using System.Collections;
using System.Collections.Generic;
using PathCreation;
using PathCreation.Examples;
using UnityEngine;

public class BallsContainer : MonoBehaviour
{

    [SerializeField]
    private List<Ball> ballList;

    [SerializeField]
    private PathCreator pathCreator;

    public bool CanMove {get;set;}

        
    public void Move(int delta)
    {
        if (!CanMove)
            return;

        foreach (var item in ballList)
        {
            item.SetGoalPos(delta);
        }

        CanMove = false;
    }
}
