using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
{
    [SerializeField, Header("ゲームオーバーとする住宅破壊数の割合"), Range(1, 100)]
    private int gameOverLimit;

    [SerializeField]
    private GameObject gameOverText;

    [HideInInspector]
    public int maxChunks;

    [HideInInspector]
    public int destroyedChunks;

    private int _destroyedChunksLimit;

    public  int CurrentScore { get; private set; }

    private async void Start()
    {
        // maxChunksが最大数になるまで待機
        await UniTask.DelayFrame(1);

        _destroyedChunksLimit = Mathf.CeilToInt(maxChunks * (gameOverLimit / 100f));

        this.ObserveEveryValueChanged(x => x.destroyedChunks).Subscribe(OnChunkDestroyed).AddTo(this);
    }

    private void OnChunkDestroyed(int totalDestroyed)
    {
        if (totalDestroyed >= _destroyedChunksLimit)
        {
            gameOverText.SetActive(true);

            if (GameManager.Instance)
            {
                GameManager.Instance.GameState = GameState.Result;
            }
        }
    }
}
