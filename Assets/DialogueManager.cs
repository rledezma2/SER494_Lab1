using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI Elements")]
    public GameObject dialoguePanel;
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    [Header("Choice Buttons")]
    public GameObject choicePanel;
    public Button yesButton;
    public Button noButton;

    private InputAction interactAction;
    private string[] currentLines;
    private int currentLineIndex;
    private Coroutine typingCoroutine;
    private bool isTyping = false;

    private System.Action yesAction;
    private System.Action noAction;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        dialoguePanel.SetActive(false);
        choicePanel.SetActive(false);

        yesButton.onClick.AddListener(OnYesPressed);
        noButton.onClick.AddListener(OnNoPressed);
    }

    public void Initialize(InputAction interact)
    {
        interactAction = interact;
    }

    public void StartDialogue(string npcName, string[] lines)
    {
        StopAllCoroutines();
        dialoguePanel.SetActive(true);
        choicePanel.SetActive(false);
        nameText.text = npcName;

        currentLines = lines;
        currentLineIndex = 0;

        ShowNextLine();
    }

    private void ShowNextLine()
    {
        if (currentLineIndex >= currentLines.Length)
        {
            EndDialogue();
            return;
        }

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        string line = currentLines[currentLineIndex];

        if (line.StartsWith("[Choice]"))
        {
            choicePanel.SetActive(true);
            dialogueText.text = "";
            currentLineIndex++; 
        }
        else
        {
            typingCoroutine = StartCoroutine(TypeLine(line));
            currentLineIndex++;
        }
    }

    private IEnumerator TypeLine(string line)
    {
        dialogueText.text = "";
        isTyping = true;

        foreach (char letter in line)
        {
            dialogueText.text += letter;
            yield return null;
        }

        isTyping = false;
    }

    private void Update()
    {
        if (dialoguePanel.activeSelf && interactAction != null && interactAction.triggered)
        {
            ContinueDialogue();
        }
    }

    public void ContinueDialogue()
    {
        if (!dialoguePanel.activeSelf || choicePanel.activeSelf) return;

        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = currentLines[currentLineIndex - 1];
            isTyping = false;
        }
        else
        {
            ShowNextLine();
        }
    }

    public void SetChoiceActions(System.Action yes, System.Action no)
    {
        yesAction = yes;
        noAction = no;
    }

    private void OnYesPressed()
    {
        choicePanel.SetActive(false);
        yesAction?.Invoke();
    }

    private void OnNoPressed()
    {
        choicePanel.SetActive(false);
        noAction?.Invoke();
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        choicePanel.SetActive(false);
    }
}
