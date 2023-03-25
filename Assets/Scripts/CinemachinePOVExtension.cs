using UnityEngine;
using Cinemachine;

public class CinemachinePOVExtension : CinemachineExtension
{
    [SerializeField]
    private Transform player;
    private InputManager inputManager;
    private Vector3 camRotation;

    private float sensitivity = 0.5f;
    private float verticalClampAngle = 89f;

    // Awake is called when the script instance is being loaded
    protected override void Awake()
    {
        inputManager = InputManager.Instance;
        base.Awake();
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state , float deltaTime)
    {
        if (vcam.Follow)
        {
            if(stage == CinemachineCore.Stage.Aim)
            {
                // If camera rotation is null, then set initial rotation
                if (camRotation == null)
                {
                    camRotation = transform.localRotation.eulerAngles;
                }

                // Get mouse input
                Vector2 mouseInput = inputManager.GetPlayerMouseMovement();

                // Set sensivity to stored value
                sensitivity = PlayerPrefs.GetFloat("MouseSensitivity");

                // If game is paused, then disable mouse look by setting sensitivity to 0
                if (Time.timeScale == 0)
                {
                    sensitivity = 0;
                }

                // Set horizontal and vertical sensitivity to the sensitivity
                float horizontalSensitivity = sensitivity;
                float verticalSensitivity = sensitivity;

                // If horizontal invert camera is enabled, then invert horizontal sensitivity
                if (PlayerPrefs.GetInt("InvertHorizontalCamera") == 1)
                {
                    horizontalSensitivity = -Mathf.Abs(horizontalSensitivity);
                }
                
                // If vertical invert camera is enabled, then invert vertical sensitivity
                if (PlayerPrefs.GetInt("InvertVerticalCamera") == 1)
                {
                    verticalSensitivity = -Mathf.Abs(verticalSensitivity);
                }

                // Set the camera rotation to mouse input multiplied by the sensitivity
                camRotation.x += mouseInput.x * horizontalSensitivity;
                camRotation.y += mouseInput.y * verticalSensitivity;

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
