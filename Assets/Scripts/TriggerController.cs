using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    private const int BallLayer = 6;

    [SerializeField]
    private BallsContainer container;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.layer +" "+ BallLayer);
        if (other.gameObject.layer == BallLayer)
        {
            container.Add(other.GetComponentInParent<Ball>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == BallLayer)
        {
            container.Remove(other.GetComponentInParent<Ball>());
        }
    }
}
