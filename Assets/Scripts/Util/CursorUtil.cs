using System.Runtime.InteropServices;

public static class CursorUtil
{
    /// <summary>
    /// マウスカーソルの位置を設定する
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    [DllImport("user32.dll")]
    public static extern void SetCursorPos(int x, int y);

    /// <summary>
    /// マウスカーソルの位置を設定する
    /// </summary>
    /// <param name="point"></param>
    public static void SetCursorPos(POINT point)
    {
        SetCursorPos(point.X, point.Y);
    }

    /// <summary>
    /// マウスカーソルの位置を設定する
    /// </summary>
    /// <param name="pos"></param>
    public static void SetCursorPos(UnityEngine.Vector2 pos)
    {
        SetCursorPos((int) pos.x, (int) pos.y);
    }

    /// <summary>
    /// マウスカーソルの位置を取得する
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    public static extern bool GetCursorPos(out POINT point);

    public struct POINT
    {
        public int X;
        public int Y;

        /// <summary>
        /// POINTをVector2に変換する
        /// </summary>
        /// <returns></returns>
        public UnityEngine.Vector2 ToVector2()
        {
            UnityEngine.Vector2 pos;
            (pos.x, pos.y) = (X, Y);

            return pos;
        }
    }
}
