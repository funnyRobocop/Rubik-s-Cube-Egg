using UnityEngine;

public class CameraController : MonoBehaviour
{
    
        [SerializeField]
        private Camera mainCamera;
        [SerializeField]
        private Transform cameraRotator;
        [SerializeField]
        private bool lockCameraXZ;

        public Camera Camera => mainCamera;
        
        public void Rotate(Vector3 inputDelta)
        {
            if (lockCameraXZ)
                inputDelta = new Vector3(0f, inputDelta.x, 0f);
            if (inputDelta.magnitude != float.NaN && !float.IsInfinity(inputDelta.magnitude) && inputDelta != Vector3.negativeInfinity && inputDelta != Vector3.positiveInfinity)
                cameraRotator.Rotate(inputDelta);
        }
}
