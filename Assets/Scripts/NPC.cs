using UnityEngine;
using UnityEngine.InputSystem;

public class NPC : MonoBehaviour
{
    private PlayerControls controls;
    private bool interactPressed;

    void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Interact.performed += ctx => interactPressed = true;
    }

    void OnEnable() => controls.Player.Enable();
    void OnDisable() => controls.Player.Disable();

    void Update()
    {
        if (interactPressed)
        {
            Debug.Log("Interacting with NPC!");
            interactPressed = false;
        }
    }
}
