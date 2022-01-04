using System.Linq;
using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField]
    private GameObject pointsSprite;

    [SerializeField]
    private AudioSource hitSound;

    [SerializeField]
    private Blink blink;

    [SerializeField]
    private GameObject[] bonuses;

    private bool isBonusable = false;

    public int points = 100;

    private void Awake()
    {
        if(Random.Range(1, 8) == 5)
        {
            isBonusable = true;
            blink.enabled = true;
        }
    }

    protected override void Die()
    {
        if(isBonusable)
        {
            var bonus = Instantiate(bonuses[Random.Range(0, bonuses.Length)]);
            bonus.transform.position = new Vector3(Random.Range(-1.5f, 10f), Random.Range(-4.5f, 7.5f), 0f);
        }

        base.Die();
        GameManager.Instance.Player1Score += points;
        GameManager.Instance.DecreaseEnemiesCount();
        TilemapUIManager.Instance.ChangeTanksUIAmount();
        var sprite = Instantiate(pointsSprite, transform.position, Quaternion.identity);
        Destroy(sprite, 1f);
    }

    public override void TakeDamage(int damageAmount)
    {
        base.TakeDamage(damageAmount);

        var colorChanger = GetComponent<ColorChanger>();
        if(colorChanger)
        {
            var sound = Instantiate(hitSound);
            Destroy(sound, 0.3f);
            colorChanger.ChangeColor();
        }        
    }
}
