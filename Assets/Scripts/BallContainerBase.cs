using System;
using UnityEngine;

namespace RubiksCubeEgg.Game
{
    public abstract class BallContainerBase : MonoBehaviour
    {

        public Action OnRotationFinished;

        public abstract void Add(Ball ball);
        public abstract void Remove(Ball ball);
        public abstract void Clear();
    }
}
