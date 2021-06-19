using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Vector3 _cameraOffset;

    private void Update()
    {
        ChangeCameraPosition();
    }

    private void ChangeCameraPosition()
    {
        var nextCameraPosition = _player.transform.position + _cameraOffset;
        transform.position = nextCameraPosition;
    }
}
