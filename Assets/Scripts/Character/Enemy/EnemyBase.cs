using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IDamageable
{
    [SerializeField, Header("初期体力値（最大）")]
    protected int defaultHealth;

    public int CurrentHealth { get; protected set; }

    /// <summary>死亡している（体力が0以下）か</summary>
    public bool IsDead => CurrentHealth <= 0;

    public abstract void OnDamage(int amount);

    private IDisposable _disposable;

    protected virtual void Awake()
    {
        CurrentHealth = defaultHealth;
    }

    protected virtual void Start()
    {
        // 破棄時にマネージャーから管理削除させる
        // AddTo(this)にすると発行前にDisposeされてしまう
        _disposable = this.OnDestroyAsObservable().Subscribe(ReturnOnDestroy);
    }

    /// <summary>
    /// 体力を減らす
    /// </summary>
    /// <param name="decreaseAmount">減らす量</param>
    protected void DecreaseHealth(int decreaseAmount)
    {
        CurrentHealth -= decreaseAmount;
    }

    private void ReturnOnDestroy(Unit _)
    {
        if (!EnemyManager.Instance) return;

        EnemyManager.Instance.RemoveEnemy(this);
        _disposable.Dispose();
    }
}
