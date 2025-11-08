using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class NPC_Dialogue1 : MonoBehaviour
{
    

    private bool playerNearby = false;
    private PlayerControls controls;
    private bool interactPressed;

    private InputAction interactAction;

    void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Interact.performed += ctx => interactPressed = true;
        interactAction = controls.Player.Interact;
    }

    void OnEnable()
    {
        controls.Player.Enable();
        if (DialogueManager.Instance != null)
            DialogueManager.Instance.Initialize(interactAction);
    }

    void OnDisable() => controls.Player.Disable();

    void Update()
    {
        if (playerNearby && interactPressed)
        {
           
            DialogueManager.Instance.StartDialogue(gameObject.name, new string[] { "Today is a really nice day to go fishing!" });
                    
            }
            
        }

    

    

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}
