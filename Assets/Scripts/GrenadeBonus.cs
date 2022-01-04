using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBonus : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerHealth>();
        if(player)
        {
            var enemies = FindObjectsOfType<EnemyHealth>();
            foreach(var enemy in enemies)
            {
                enemy.TakeDamage(10);
                Destroy(gameObject);
            }
        }
    }
}
