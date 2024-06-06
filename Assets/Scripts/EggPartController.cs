using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation.Examples;
using UnityEngine;

public class EggPartController : MonoBehaviour
{
    private const int BallLayerMask = 6;
    
    [SerializeField]
    private Transform ballParent;

    //private List<Transform> ballLList = new List<Transform>();
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == BallLayerMask)
        {
            var parent = other.transform.parent;
            parent.SetParent(ballParent);
            parent.GetComponent<PathFollower>().pathCreator = null;
            //ballLList.Add(parent);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            /*foreach (var item in ballLList)
            {
                item.parent = null;
                if (ballLList.Contains(item))
                    ballLList.Remove(item);
                break;
            }*/
        }
    }
}
