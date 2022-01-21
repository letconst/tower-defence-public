using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    private void Start()
    {
        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0))
            .Take(1)
            .Subscribe(_ => SystemSceneManager.LoadNextScene("MainGameV2", SceneTransition.Fade))
            .AddTo(this);
    }
}
