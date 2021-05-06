using System.Collections.Generic;
using UnityEngine;

namespace KILROY.Project.Demo
{
    #region Data

    /// <summary>
    /// 数据
    /// </summary>
    public static class DemoData
    {
        public static Dictionary<DemoCamera, Camera> Camera = new Dictionary<DemoCamera, Camera>(); // 相机列表

        public static DemoContainer Container = new DemoContainer(); // 容器
    }

    #endregion

    #region Structure

    /// <summary>
    /// 容器
    /// </summary>
    public class DemoContainer
    {
        public Transform BoxCamera = null; //  盒子-相机
        public Transform BoxCanvas = null; //  盒子-画布
        public Transform BoxLight = null; //  盒子-灯光
        public Transform BoxMap = null; //  盒子-地图
        public Transform BoxProp = null; //  盒子-道具
        public Transform BoxRole = null; //  盒子-角色
    }

    #endregion

    #region Enum

    /// <summary>
    /// 相机
    /// </summary>
    public enum DemoCamera
    {
        Main, // 主相机
    }

    #endregion
}