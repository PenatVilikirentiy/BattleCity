using UnityEngine;

public class HelmetBonus : MonoBehaviour
{
    public float invulnerableTime = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var playerHealth = collision.GetComponent<PlayerHealth>();
        if(playerHealth)
        {
            playerHealth.MakeInvulnerable(invulnerableTime);
            Destroy(gameObject);
        }
    }
}
