using System.Collections.Generic;
using Project.Scripts.Fractures;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public GameState GameState { get; set; }

    public List<ChunkGraphManager> ChunkInstances  { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        GameState      = GameState.InGame;
        ChunkInstances = new List<ChunkGraphManager>(16);
    }
}

public enum GameState
{
    InGame,
    InTransition,
    Result,
}
