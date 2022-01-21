using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public static class GameInitializer
{
    [RuntimeInitializeOnLoadMethod]
    private static async void Init()
    {
        Application.targetFrameRate = 60;

        GameObject gameSysObj = await Addressables.InstantiateAsync("GameSystem");
        Object.DontDestroyOnLoad(gameSysObj);
    }
}
