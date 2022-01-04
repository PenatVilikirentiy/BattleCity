using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField]
    private EnemyHealth enemyHealth;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private Color color;

    public void ChangeColor()
    {        
        switch (enemyHealth.HealthValue)
        {
            case 1: color = Color.red; break;
            case 2: color = Color.yellow; break;
            case 3: color = Color.green; break;
        }
        spriteRenderer.color = color;
    }
}
