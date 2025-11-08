using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class Dialogue : MonoBehaviour
{
    [Header("Dialogue Lines")]
    [TextArea(2, 5)] public string[] dialogueWithoutBook;
    [TextArea(2, 5)] public string[] dialogueWithBook;

    private bool playerNearby = false;
    private Inventory playerInventory;
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
            interactPressed = false;
            if (playerInventory == null) return;

            bool hasBook = playerInventory.GetItems().Exists(i => i.itemType == ItemType.Book);

            if (hasBook && dialogueWithBook.Length > 0)
            {
                DialogueManager.Instance.StartDialogue(gameObject.name, dialogueWithBook);
                RemoveBookFromInventory();

                DialogueManager.Instance.SetChoiceActions(
                    yes: () => {
                        DialogueManager.Instance.StartDialogue(gameObject.name, new string[] { "Thank you for brining be the book!" });
                    },
                    no: () => {
                        DialogueManager.Instance.StartDialogue(gameObject.name, new string[] { "You lie! I will take the book for myself!!!" });
                    }
                );
            }
            else if (!hasBook && dialogueWithoutBook.Length > 0)
            {
                DialogueManager.Instance.StartDialogue(gameObject.name, dialogueWithoutBook);

                DialogueManager.Instance.SetChoiceActions(
                    yes: () => {
                        DialogueManager.Instance.StartDialogue(gameObject.name, new string[] { "Lies!!! You do not have the 'Book of Robots'!!! Please find it!" });
                    },
                    no: () => {
                        DialogueManager.Instance.StartDialogue(gameObject.name, new string[] { "Please find the 'Book of Robots' and bring it to me." });
                    }
                );
            }
        }
    }

    private void RemoveBookFromInventory()
    {
        var book = playerInventory.GetItems().Find(i => i.itemType == ItemType.Book);
        if (book != null)
            playerInventory.RemoveItem(book);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            playerInventory = other.GetComponent<Inventory>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            playerInventory = null;
        }
    }
}
