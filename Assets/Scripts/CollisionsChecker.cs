using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RubiksCubeEgg.Game
{
    public class CollisionsChecker : MonoBehaviour
    {
        [SerializeField]
        private List<BallContainerBase> ballContainers;

        private Coroutine checkCollisionsCrtn;


        private void Awake()
        {
            foreach (var item in ballContainers)
                item.OnRotationFinished += Run;
        }

        private void Start()
        {
            Run();
        }

        private void OnDestroy()
        {
            StopAllCoroutines();

            foreach (var item in ballContainers)
                item.OnRotationFinished -= Run;
        }

        public void Run()
        {
            if (checkCollisionsCrtn != null)
                StopCoroutine(checkCollisionsCrtn);

            foreach (var item in ballContainers)
                item.Clear();

            gameObject.SetActive(true);

            if (isActiveAndEnabled)
                checkCollisionsCrtn = StartCoroutine(CheckCollisionsCrtn());
        }

        private IEnumerator CheckCollisionsCrtn()
        {
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();

            gameObject.SetActive(false);
        }
    }
}
