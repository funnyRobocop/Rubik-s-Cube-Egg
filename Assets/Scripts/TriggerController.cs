using UnityEngine;


namespace RubiksCubeEgg.Game
{
    public class TriggerController : MonoBehaviour
    {

        [SerializeField]
        private BallContainerBase container;

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
}