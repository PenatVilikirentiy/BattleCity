using UnityEngine;
using System.Linq;

public enum Direction { Up, Right, None }

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private GameObject bulletExplosion;

    [SerializeField]
    private AudioSource bulletExplosionSound;

    private Player player;

    public Vector2 vec;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        bulletExplosionSound = FindObjectsOfType<AudioSource>().Where(s => s.name == "BulletExplosion").First();
    }

    private void Start()
    {
        rb.velocity = transform.up * player.bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Die();
        
        var health = collision.gameObject.GetComponent<Health>();
        if(health)
        {
            health.TakeDamage(player.damageAmount);
        }

        Vector3 hitPosition = Vector3.zero;
        hitPosition.x = collision.contacts[0].point.x - 0.01f * collision.contacts[0].normal.x;
        hitPosition.y = collision.contacts[0].point.y - 0.01f * collision.contacts[0].normal.y;
        TilemapDestroyer.Instance.DestroyTiles(hitPosition, player.area, DetectSide(), player.canDestroySteel);
    }

    private void OnDestroy()
    {
        Die();
    }

    public void Die()
    {
        var explosionFX = Instantiate(bulletExplosion, transform.position, transform.rotation);
        bulletExplosionSound.Play();
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
