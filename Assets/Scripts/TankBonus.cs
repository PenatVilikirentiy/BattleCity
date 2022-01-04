using UnityEngine;

public class TankBonus : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var playerHealth = collision.GetComponent<PlayerHealth>();
        if(playerHealth)
        {
            playerHealth.AddHealth();
            GameManager.Instance.lives++;
            TilemapUIManager.Instance.AddPlayerLivesUI();
            Destroy(gameObject);
        }
    }
}
