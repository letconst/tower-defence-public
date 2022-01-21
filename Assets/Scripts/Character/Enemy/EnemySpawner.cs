using System;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField, Header("スポーンする敵")]
    private GameObject enemyPrefab;

    [SerializeField, Header("スポーンを試行する間隔 (秒)"), Range(1, 10)]
    private float spawnInterval;

    [SerializeField, Header("スポーン試行時にスポーンする確率 (%)"), Range(0, 100)]
    private int spawnChance;

    private void Start()
    {
        Observable.Interval(TimeSpan.FromSeconds(spawnInterval))
                  .Where(_ => EnemyManager.Instance && EnemyManager.Instance.CanSpawn)
                  .Subscribe(Spawn)
                  .AddTo(this);
    }

    /// <summary>
    /// スポーンするかを抽選する
    /// </summary>
    /// <returns></returns>
    private bool CanSpawnByChance()
    {
        int rnd = Random.Range(1, 100);

        return rnd <= spawnChance;
    }

    /// <summary>
    /// 敵をスポーンさせる
    /// </summary>
    /// <param name="_"></param>
    private void Spawn(long _)
    {
        // スポーンするか確率計算
        if (!CanSpawnByChance()) return;

        GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

        if (EnemyManager.Instance)
        {
            EnemyManager.Instance.AddEnemy(newEnemy);
        }
    }
}
