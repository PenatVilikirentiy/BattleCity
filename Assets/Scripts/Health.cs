using System.Linq;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField]
    private AudioSource explosionSound;

    [SerializeField]
    protected int health = 1;

    [SerializeField]
    private GameObject explosionFX;

    public int HealthValue => health;

    private void Awake()
    {
        //explosionSound = FindObjectsOfType<AudioSource>().Where(s => s.name == "Explosion").Single();
        //Debug.Log(gameObject.name + " " + explosionSound);
    }

    public virtual void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if(health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        var explosionGO = Instantiate(explosionFX, transform.position, Quaternion.identity);
        var sound = Instantiate(explosionSound);
        Destroy(sound, 1f);
        Destroy(gameObject);
        Destroy(explosionGO, 0.6f);
    }
}
