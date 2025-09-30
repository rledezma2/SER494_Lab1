using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(15.1f, -3.083f, -1.256f);


    void LateUpdate()
    {
        transform.position = player.position + offset;
    }
}