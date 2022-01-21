using System;
using FORGE3D;
using Project.Scripts.Fractures;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(F3DFXController))]
public class Human : EnemyBase
{
    [SerializeField]
    private GameObject deathEffect;

    [SerializeField]
    private float attackInterval;

    private F3DFXController _fxController;

    private Transform _attackTarget;
    private Animator  _animator;

    private static readonly int AttackStart = Animator.StringToHash("AttackStart");
    private static readonly int AttackEnd   = Animator.StringToHash("AttackEnd");
    private static readonly int Attack      = Animator.StringToHash("Attack");

    protected override void Start()
    {
        base.Start();

        _fxController = GetComponent<F3DFXController>();
        _animator     = GetComponent<Animator>();

        this.ObserveEveryValueChanged(x => x._selfState).Subscribe(OnStateChanged).AddTo(this);
        this.UpdateAsObservable()
            .Where(_ => GameManager.Instance && GameManager.Instance.GameState == GameState.InGame)
            .Where(_ => _attackTarget && _selfState == EnemyState.Attacking)
            .ThrottleFirst(TimeSpan.FromSeconds(attackInterval))
            .Subscribe(OnAttack)
            .AddTo(this);
    }

    private void Update()
    {
        switch (_selfState)
        {
            case EnemyState.Idling:
            {
                break;
            }

            case EnemyState.Moving:
            {
                break;
            }

            case EnemyState.Attacking:
            {
                _animator.SetTrigger(AttackStart);

                break;
            }

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnStateChanged(EnemyState _)
    {
        switch (_selfState)
        {
            case EnemyState.Idling:
            {
                // _animator.SetTrigger(AttackEnd);

                ChunkGraphManager closestChunk = GetClosestChunk();

                if (closestChunk == null) break;

                _attackTarget = closestChunk.transform;
                // TODO: 対象まで遠い場合は一定距離まで近づかせる
                _selfState = EnemyState.Attacking;

                SetRandomRotation();

                break;
            }

            case EnemyState.Moving:
            {
                _animator.SetTrigger(AttackEnd);

                break;
            }

            case EnemyState.Attacking:
            {

                break;
            }
        }
    }

    private void OnAttack(Unit _)
    {
        SetRandomRotation();
        _fxController.Fire();
        _fxController.Stop();
        _animator.SetTrigger(Attack);
    }

    /// <summary>
    /// 自身をランダムに回転させる
    /// </summary>
    private void SetRandomRotation()
    {
        Quaternion rot = transform.rotation;
        rot.y              = Random.rotation.y;
        transform.rotation = rot;
    }

    /// <summary>
    /// 最寄りのChunkGraphManagerを取得する
    /// </summary>
    /// <returns></returns>
    private ChunkGraphManager GetClosestChunk()
    {
        // GameManagerがなければエラーとして終了
        if (!GameManager.Instance)
        {
            Debug.LogError("GameManager is not found");

            return null;
        }

        ChunkGraphManager closestChunk       = null;
        float             closestDistanceSqr = Mathf.Infinity;
        Vector3           currentSelfPos     = transform.position;

        foreach (ChunkGraphManager chunk in GameManager.Instance.ChunkInstances)
        {
            Vector3 dirToTarget  = chunk.transform.position - currentSelfPos;
            float   distToTarget = dirToTarget.sqrMagnitude;

            if (distToTarget < closestDistanceSqr)
            {
                closestChunk       = chunk;
                closestDistanceSqr = distToTarget;
            }
        }

        return closestChunk;
    }

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
