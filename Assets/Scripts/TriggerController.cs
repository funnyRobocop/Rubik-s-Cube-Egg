using UnityEngine;

public class TriggerController : MonoBehaviour
{
    private const int BallLayer = 6;

    [SerializeField]
    private SideBallContainer container;

    private void OnTriggerEnter(Collider other)
    {
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
