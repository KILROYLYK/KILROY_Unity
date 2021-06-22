using System;

namespace KILROY.Model
{
    #region Static

    /// <summary>
    /// 应用数据
    /// </summary>
    public static class ApplicationData
    {
        public static readonly string Name = "KILROY"; // 名称
        public static readonly string Version = "1.0.0"; // 版本
        public static AppMode Mode = AppMode.DevelopSelf; // 模式
        public static long Timestamp = 0; // 时间戳
    }

    /// <summary>
    /// 配置数据
    /// </summary>
    public static class ConfigData
    {
        // 音频
        public static float Audio = 1; // 主音量
        public static float AudioMusic = 1; // 音乐
        public static float AudioSound = 1; // 音效
        public static float AudioDialog = 1; // 对白
    }

    /// <summary>
    /// 用户数据
    /// </summary>
    public static class UserData
    {
        public static int Id = 0; // 用户ID
        public static string NickName = string.Empty; // 昵称
    }

    #endregion

    #region Dynamic

    /// <summary>
    /// 通知数据
    /// </summary>
    public class NotificationData
    {
        public object Data = null; // 数据
        public Action Callback = null; // 回调
    }

    /// <summary>
    /// 鼠标数据
    /// </summary>
    public class MouseData
    {
        public float AxisX = 0; // 水平向量
        public float AxisY = 0; // 垂直向量
        public bool Left = false; // 左键
        public bool Right = false; // 右键
        public bool Center = false; // 中键
    }

    /// <summary>
    /// 键盘数据
    /// </summary>
    public class KeyboardData
    {
        public float AxisX = 0; // 水平向量
        public float AxisY = 0; // 垂直向量

        public bool Move // 移动
        {
            get { return AxisX != 0 || AxisY != 0; }
        }

        public bool Escape = false; // 退出
        public bool EscapePress = false; // 退出-长按

        public bool Tab = false; // 标签
        public bool TabPress = false; // 标签-长按

        public bool CapsLock = false; // 大写锁定
        public bool CapsLockPress = false; // 大写锁定-长按

        public bool ShiftLeft = false; // Shift-左
        public bool ShiftLeftPress = false; // Shift-左-长按

        public bool ControlLeft = false; // Control-左
        public bool ControlLeftPress = false; // Control-左-长按

        public bool AltLeft = false; // Alt-左
        public bool AltLeftPress = false; // Alt-左-长按

        public bool Space = false; // 空格
        public bool SpacePress = false; // 空格-长按
    }

    #endregion

    #region Enum

    /// <summary>
    /// 应用模式
    /// </summary>
    public enum AppMode
    {
        DevelopSelf, // 开发-独立
        DevelopIntegration, // 开发-整合
        Official // 正式
    }

    /// <summary>
    /// 图层
    /// </summary>
    public enum Layers
    {
        Application = 8,
        Menu = 9,
        Load = 10,
        Game = 11
    }

    #endregion
}