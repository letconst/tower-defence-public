using UnityEngine;

public class MainGameController : MonoBehaviour
{
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        if (GameManager.Instance && GameManager.Instance.GameState == GameState.Result)
        {
            if (!SystemSceneManager.IsLoading && Input.GetMouseButtonDown(0))
            {
                SystemSceneManager.LoadNextScene("Title", SceneTransition.Fade);
            }
        }
    }
}
