using UnityEngine;


namespace RubiksCubeEgg
{
    public class Main : MonoBehaviour
    {      

        public int CurrentLevel = 0;
        
        [SerializeField]
        private Game.BallSpawner ballSpawner;
        [SerializeField]
        private Game.CollisionsChecker collisionsChecker;

        
        void Start()
        {
            CurrentLevel = 1;
            ballSpawner.Init(CurrentLevel);
            collisionsChecker.Init();
        }
    }
}
