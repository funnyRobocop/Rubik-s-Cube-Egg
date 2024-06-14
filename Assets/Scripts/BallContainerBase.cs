using UnityEngine;

namespace RubiksCubeEgg.Game
{
    public class BallContainerBase : MonoBehaviour
    {
        public virtual void Add(Ball ball) {}
        public virtual void Remove(Ball ball) {}
    }
}
