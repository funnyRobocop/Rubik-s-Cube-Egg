using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private Transform cameraRotator;
    [SerializeField]
    private bool lockXZ;
    private Vector3 lastMousePosition = Vector3.negativeInfinity;
    private TouchState touchState = TouchState.None;
    private const int UpLayerMask = 1 << 7;
    private const int MidLayerMask = 1 << 8;
    private const int DownLayerMask = 1 << 9;
    [SerializeField]
    private Transform up;
    [SerializeField]
    private Transform mid;
    [SerializeField]
    private Transform down;
    
    private enum TouchState
    {
        None,
        Camera,
        Top,
        Mid,
        Down,
        Ball
    }

    private void Update()
    {
        if (!Input.GetMouseButton(0))
        {
            touchState = TouchState.None;
            lastMousePosition = Input.mousePosition;
            return;
        }
        
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        var isUpHit = Physics.Raycast(ray, out var upHit, 100, UpLayerMask);
        if (isUpHit)
        {
            var rotationUp = Input.mousePosition - lastMousePosition;
            rotationUp = new Vector3(0f, -rotationUp.x, 0f);
            up.Rotate(rotationUp);
            lastMousePosition = Input.mousePosition;
            return;
        }
        
        var isMidHit = Physics.Raycast(ray, out var midHit, 100, MidLayerMask);
        if (isMidHit)
        {
            var rotationMid = Input.mousePosition - lastMousePosition;
            rotationMid = new Vector3(0f, -rotationMid.x, 0f);
            mid.Rotate(rotationMid);
            lastMousePosition = Input.mousePosition;
            return;
        }
        
        var isDownHit = Physics.Raycast(ray, out var downHit, 100, DownLayerMask);
        if (isDownHit)
        {
            var rotationDown = Input.mousePosition - lastMousePosition;
            rotationDown = new Vector3(0f, -rotationDown.x, 0f);
            down.Rotate(rotationDown);
            lastMousePosition = Input.mousePosition;
            return;
        }

        var rotation = Input.mousePosition - lastMousePosition;
        if (lockXZ)
            rotation = new Vector3(0f, rotation.x, 0f);
        cameraRotator.transform.Rotate(rotation);
        lastMousePosition = Input.mousePosition;
    }
}
