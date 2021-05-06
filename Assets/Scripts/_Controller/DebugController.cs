using System.Collections.Generic;
using UnityEngine;
using KILROY.Base;

namespace KILROY.Controller
{
    public class DebugController : BaseControllerBehaviour<DebugController>
    {
        #region Parameter

        private Rect ScreenRect = Rect.zero; // 场景信息
        private Vector2 ScrollPosition = Vector2.zero; // 滚动位置
        private List<string> LogList = new List<string>(); // 日志列表
        private bool IsOpen = false; // 是否打开

        #endregion

        #region Cycle

        public void Awake()
        {
            ScreenRect = new Rect(0, 0, Screen.width, Screen.height);
            UnityEngine.Application.logMessageReceived += Listener;
        }

        // public void Start() { }

        // public void Update() { }

        public void OnGUI()
        {
            if (IsOpen) GUILayout.Window(0, ScreenRect, ShowLogWindow, "Debug");
        }

        #endregion

        /// <summary>
        /// 监听日志
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="stack">堆栈</param>
        /// <param name="type">日志类型</param>
        private void Listener(string content, string stack, LogType type)
        {
            if (type == LogType.Log 
                || type == LogType.Error 
                || type == LogType.Warning 
                || type == LogType.Assert 
                || type == LogType.Exception) LogList.Add(string.Format("{0}\n{1}", content, stack));
        }

        /// <summary>
        /// 绘制日志窗口
        /// </summary>
        /// <param name="windowID">窗口ID</param>
        private void ShowLogWindow(int windowID)
        {
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("清理日志", GUILayout.MaxWidth(Screen.width / 2), GUILayout.MaxHeight(50))) LogList.Clear();
            if (GUILayout.Button("关闭调试", GUILayout.MaxWidth(Screen.width / 2), GUILayout.MaxHeight(50))) IsOpen = false;

            ScrollPosition = GUILayout.BeginScrollView(ScrollPosition);
            foreach (var entry in LogList) GUILayout.TextArea(entry);
            GUILayout.EndScrollView();
        }

        /// <summary>
        /// 打开
        /// </summary>
        public void Open() { IsOpen = true; }
    }
}