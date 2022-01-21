public interface IDamageable
{
    /// <summary>現在の体力値</summary>
    public int CurrentHealth { get; }

    /// <summary>
    /// ダメージを受けた際の処理
    /// </summary>
    /// <param name="amount">ダメージ量</param>
    public void OnDamage(int amount);
}
