using UnityEngine;
using Cinemachine;

public class CinemachinePOVExtension : CinemachineExtension
{
    [SerializeField]
    private Transform player;
    private InputManager inputManager;
    private Vector3 camRotation;

    private float sensitivity = 0.1f;
    private float verticalClampAngle = 89f;

    // Awake is called when the script instance is being loaded
    protected override void Awake()
    {
        inputManager = InputManager.Instance;
        base.Awake();
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state , float deltaTime) {
        if (vcam.Follow) {
            if( stage == CinemachineCore.Stage.Aim) {
                if (camRotation == null) camRotation = transform.localRotation.eulerAngles;
                
                // Get mouse input
                Vector2 mouseInput = inputManager.GetPlayerMouseMovement();
                
                camRotation.x += mouseInput.x * sensitivity;
                camRotation.y += mouseInput.y * sensitivity;

                // Clamp the vertival viewing angle
                camRotation.y = Mathf.Clamp(camRotation.y, -verticalClampAngle, verticalClampAngle);

                // Rotate the camera
                state.RawOrientation = Quaternion.Euler(-camRotation.y, camRotation.x, 0f);

                // Rotate the player with the camera
                player.rotation = Quaternion.Euler(0, camRotation.x, 0);
            }
        }
    }
}
