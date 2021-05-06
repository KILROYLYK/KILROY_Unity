using System.Collections.Generic;
using UnityEngine;

namespace KILROY.Project.Demo
{
    #region Data

    /// <summary>
    /// 数据
    /// </summary>
    public static class Demo_1_Data
    {
        public static Dictionary<Demo_1_Camera, Camera> Camera = new Dictionary<Demo_1_Camera, Camera>(); // 相机列表

        public static Demo_1_Container Container = new Demo_1_Container(); // 容器
    }

    #endregion

    #region Structure

    /// <summary>
    /// 容器
    /// </summary>
    public class Demo_1_Container
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
    public enum Demo_1_Camera
    {
        Main, // 主相机
        Snake, // 蛇视角相机
    }

    #endregion
}