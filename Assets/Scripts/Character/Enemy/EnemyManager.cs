using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : SingletonMonoBehaviour<EnemyManager>
{
    [SerializeField, Header("敵の最大出現数")]
    private int maxEnemies;

    public int MaxEnemies => maxEnemies;

    public int CurrentEnemies => _enemies.Count;

    public bool CanSpawn => _enemies.Count < maxEnemies;

    private List<EnemyBase> _enemies;

    private void Start()
    {
        _enemies = new List<EnemyBase>();
    }

    /// <summary>
    /// 敵を管理下に追加する
    /// </summary>
    /// <param name="enemyObject">追加する敵オブジェクト</param>
    public void AddEnemy(GameObject enemyObject)
    {
        var enemy = enemyObject.GetComponent<EnemyBase>();

        AddEnemy(enemy);
    }

    /// <summary>
    /// 敵を管理下に追加する
    /// </summary>
    /// <param name="enemy">削除する敵</param>
    public void AddEnemy(EnemyBase enemy)
    {
        if (!enemy) return;

        _enemies.Add(enemy);
    }

    /// <summary>
    /// 敵を管理下から削除する
    /// </summary>
    /// <param name="enemy">削除する敵</param>
    public void RemoveEnemy(EnemyBase enemy)
    {
        Debug.Log(enemy);
        if (!enemy) return;

        _enemies.Remove(enemy);
    }
}
