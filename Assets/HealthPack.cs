using UnityEngine;

public class HealthPack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Health player = other.GetComponent<Health>();
        if (player != null)
        {
            player.Heal(0);
            Destroy(gameObject);
        }
    }
}
