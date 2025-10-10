using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class HUD : MonoBehaviour
{
    [Header("Position Display")]
    [SerializeField] private TextMeshProUGUI positionText;

    [Header("Rotation Display")]
    [SerializeField] private TextMeshProUGUI rotationText;

    [Header("Death Screen")]
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private TextMeshProUGUI deathText;
    [SerializeField] private Button restartButton;

    private Transform playerTransform;
    private Health playerHealth;
    private PlayerMovement playerMovement;
    private Rigidbody playerRigidbody;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
            playerHealth = player.GetComponent<Health>();
            playerMovement = player.GetComponent<PlayerMovement>();
            playerRigidbody = player.GetComponent<Rigidbody>();
        }

        if (deathPanel != null)
        {
            deathPanel.SetActive(false);
        }

        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            UpdatePositionDisplay();
            UpdateRotationDisplay();
        }

        if (playerHealth != null && playerHealth.GetCurrentHealth() <= 0 && deathPanel != null && !deathPanel.activeSelf)
        {
            ShowDeathScreen();
        }
    }

    void UpdatePositionDisplay()
    {
        if (positionText != null)
        {
            float x = playerTransform.position.x;
            float z = playerTransform.position.z;
            positionText.text = $"Position\nX: {x:F1}  Z: {z:F1}";
        }
    }

    void UpdateRotationDisplay()
    {
        if (rotationText != null)
        {
            float yRotation = playerTransform.eulerAngles.y;
            rotationText.text = $"Rotation\n{yRotation:F0}°";
        }
    }

    void ShowDeathScreen()
    {
        if (deathPanel != null)
        {
            deathPanel.SetActive(true);
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}