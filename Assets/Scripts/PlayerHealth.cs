using UnityEngine;

public class PlayerHealth : Health
{
    private bool invulnerable = false;

    [SerializeField]
    private GameObject invulnerableFX;

    [SerializeField]
    private Player player;

    private GameObject deleteFX;

    public override void TakeDamage(int damageAmount)
    {
        if (invulnerable) return;

        base.TakeDamage(damageAmount);        
    }

    public void AddHealth()
    {
        health += 1;
    }

    public void MakeInvulnerable(float time)
    {
        invulnerable = true;
        deleteFX = Instantiate(invulnerableFX, transform.position, Quaternion.identity, transform);
        Invoke(nameof(MakeVulnerable), time);
    }

    private void MakeVulnerable()
    {
        invulnerable = false;
        Destroy(deleteFX);
    }

    protected override void Die()
    {
        player.currentTankLevel = TankLevel.level1;
        player.ChangeTankLevel();
        GameManager.Instance.lives--;
        GameManager.Instance.ChangeState(State.Spawn);
        TilemapUIManager.Instance.RemovePlayerLivesUI();
        base.Die();        
    }
}
