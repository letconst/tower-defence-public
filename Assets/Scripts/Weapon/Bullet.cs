using UnityEngine;

[RequireComponent(typeof(FORGE3D.F3DProjectile))]
public class Bullet : MonoBehaviour
{
    [SerializeField, Header("与えるダメージ量")]
    private int damageAmount;

    private FORGE3D.F3DProjectile _projectile;

    private void Start()
    {
        _projectile = GetComponent<FORGE3D.F3DProjectile>();
    }

    private void Update()
    {
        // 弾が何かに当たったとき
        if (_projectile.IsHit)
        {
            // ダメージを与える対象ならダメージ付与
            _projectile.HitPoint.collider.GetComponent<IDamageable>()?.OnDamage(damageAmount);
        }
    }
}
