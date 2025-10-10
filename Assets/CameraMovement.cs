using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0f, 0f, 0f);


    void LateUpdate()
    {
        transform.position = player.position + offset + new Vector3(-1.6f, -.4f, -1.5f);
    }
}