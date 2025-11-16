using System.Numerics;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform; // The location of the player.
    public UnityEngine.Vector3 offset; // How far back to move the camera.
    public float smoothSpeed; // How quickly to move the camera while smoothing it. A range of 0-1.

    // LateUpdate updates after everything else.
    private void LateUpdate()
    {
        // If we just set the camera to the player position, the camera would be inside the player.
        UnityEngine.Vector3 desiredPosition = playerTransform.position + offset;
        // Uses linear interpolation (lerp) to find a better inbetween from the current position of the camera and where we want it to be.
        UnityEngine.Vector3 smoothedPosition = UnityEngine.Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        // Starts moving the camera
        transform.position = smoothedPosition;
    }
}
