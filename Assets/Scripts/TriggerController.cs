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
                Debug.LogFormat("Enter {0} {1}", gameObject.name, other.transform.parent.name);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == Consts.BallLayer)
            {
                container.Remove(other.GetComponentInParent<Ball>());
                Debug.LogFormat("Exit {0} {1}", gameObject.name, other.transform.parent.name);
            }
        }
    }
}