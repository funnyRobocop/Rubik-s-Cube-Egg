using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation;
using PathCreation.Examples;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private const int EggUpLayerMask = 10;
    private const int EggForwardLayerMask = 13;

    private float prevDistanceTraveled;
    
    [SerializeField]
    private PathFollower pathFollower;

    [SerializeField]
    private PathCreator path;

    private void Awake()
    {
        prevDistanceTraveled = pathFollower.distanceTravelled;
    }

    private void OnTriggerStay(Collider other)
    {
        /*if (other.gameObject.layer == EggForwardLayerMask)
        {
            if (!Input.GetMouseButton(0))
            {
                var parent = transform.parent;
                parent.SetParent(other.transform);
                pathFollower.pathCreator = path;
                pathFollower.distanceTravelled = prevDistanceTraveled;
                //ballLList.Add(parent);
            }
        }
        else if (other.gameObject.layer == EggUpLayerMask)
        {
            var parent = transform.parent;
            parent.SetParent(other.transform);
            prevDistanceTraveled = pathFollower.distanceTravelled;
            pathFollower.pathCreator = null;
            //ballLList.Add(parent);
        }*/
    }
}
