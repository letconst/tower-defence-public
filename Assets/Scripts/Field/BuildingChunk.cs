using System;
using UniRx;
using UnityEngine;

public class BuildingChunk : MonoBehaviour
{
    private Vector3 _originPos;
    private bool    _isAwayed;

    private IDisposable _disposable;

    private void Start()
    {
        _originPos = transform.position;

        ScoreManager.Instance.maxChunks++;

        _disposable = this.ObserveEveryValueChanged(x => x.transform.position)
                          .Subscribe(CheckAway)
                          .AddTo(this);
    }

    private void CheckAway(Vector3 movedPos)
    {
        // 一定距離離れていたら、この住宅パーツは破壊されたとみなす
        // TODO: magic number
        if (Vector3.Distance(movedPos, _originPos) > 20)
        {
            _isAwayed = true;

            _disposable.Dispose();
            ScoreManager.Instance.destroyedChunks++;
        }
    }
}
