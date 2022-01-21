using Cysharp.Threading.Tasks;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Vector3 posOffset;

    [SerializeField]
    private Vector3 rotOffset;

    [SerializeField, Header("マウス移動によるカメラ回転の感度"), Range(.1f, 5)]
    private float rotateSensitivity;

    private Transform        _playerTrf;
    private CursorUtil.POINT _fixedMousePos;

    private void Start()
    {
        _playerTrf = GameObject.FindWithTag("Player").transform;

        SetDefaultTransform();
    }

    private async void Update()
    {
        // 右クリック押下で現在のカーソル位置取得
        if (Input.GetMouseButtonDown(1))
        {
            // カーソル位置記憶
            CursorUtil.GetCursorPos(out _fixedMousePos);
            Cursor.lockState = CursorLockMode.Locked;
        }

        // 右クリックドラッグでカメラを回転
        if (Input.GetMouseButton(1))
        {
            UpdateCameraRotation();
        }

        if (Input.GetMouseButtonUp(1))
        {
            Cursor.lockState = CursorLockMode.Confined;

            // 同フレームでは↑でカーソル位置が上書きされるため、SetCursorPosは1フレーム待つ
            await UniTask.DelayFrame(1);

            // カーソルをドラッグ前の位置に戻す
            CursorUtil.SetCursorPos(_fixedMousePos);
        }
    }

    /// <summary>
    /// カメラの位置・回転を定位置に設定する
    /// </summary>
    private void SetDefaultTransform()
    {
        transform.position = _playerTrf.position - posOffset;
        transform.LookAt(_playerTrf);
        transform.rotation *= Quaternion.Euler(rotOffset.x, rotOffset.y, rotOffset.z);
    }

    /// <summary>
    /// マウスのX移動によりカメラをプレイヤー中心に回転させる
    /// </summary>
    private void UpdateCameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");

        if (mouseX == 0) return;

        transform.RotateAround(_playerTrf.position, Vector3.up, -mouseX * rotateSensitivity);
    }
}
