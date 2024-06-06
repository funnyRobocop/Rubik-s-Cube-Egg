using System.Collections;
using System.Collections.Generic;
using PathCreation;
using PathCreation.Examples;
using UnityEngine;

public class EggSideController : MonoBehaviour
{
    private const int BallLayerMask = 6;

    [SerializeField]
    private PathCreator path;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == BallLayerMask)
        {
            other.transform.parent.GetComponent<PathFollower>().pathCreator = path;
        }
    }
}
