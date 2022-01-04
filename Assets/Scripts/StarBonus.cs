using UnityEngine;

public class StarBonus : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if(player)
        {
            player.currentTankLevel++;
            player.ChangeTankLevel();
            Destroy(gameObject);
        }
    }
}
