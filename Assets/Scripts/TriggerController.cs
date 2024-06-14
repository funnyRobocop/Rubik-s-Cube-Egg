using UnityEngine;

public class TriggerController : MonoBehaviour
{

    [SerializeField]
    private SideBallContainer container;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Consts.BallLayer)
        {
            container.Add(other.GetComponentInParent<Ball>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == Consts.BallLayer)
        {
            container.Remove(other.GetComponentInParent<Ball>());
        }
    }
}
