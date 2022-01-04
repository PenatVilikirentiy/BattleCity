using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private GameObject bulletExplosion;

    private void Start()
    {
        rb.velocity = transform.up * 5f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Die();

        var health = collision.gameObject.GetComponent<Health>();
        if (health)
        {
            health.TakeDamage(1);
        }

        Vector3 hitPosition = Vector3.zero;
        hitPosition.x = collision.contacts[0].point.x - 0.01f * collision.contacts[0].normal.x;
        hitPosition.y = collision.contacts[0].point.y - 0.01f * collision.contacts[0].normal.y;
        TilemapDestroyer.Instance.DestroyTiles(hitPosition, 0.8f, DetectSide(), false);
    }

    private void OnDestroy()
    {
        Die();
    }

    public void Die()
    {
        var explosionFX = Instantiate(bulletExplosion, transform.position, transform.rotation);

        Destroy(gameObject);
        Destroy(explosionFX, 0.3f);
    }

    private Direction DetectSide()
    {
        if (Mathf.Round(transform.up.y) == 1f || Mathf.Round(transform.up.y) == -1f)
        {
            return Direction.Up;
        }

        if (Mathf.Round(transform.up.x) == 1f || Mathf.Round(transform.up.x) == -1f)
        {
            return Direction.Right;
        }

        return Direction.None;
    }
}
