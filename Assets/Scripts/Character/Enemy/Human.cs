using UnityEngine;

public class Human : EnemyBase
{
    [SerializeField]
    private GameObject deathEffect;

    public override void OnDamage(int amount)
    {
        DecreaseHealth(amount);

        // 死亡してたら破棄
        if (IsDead)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
