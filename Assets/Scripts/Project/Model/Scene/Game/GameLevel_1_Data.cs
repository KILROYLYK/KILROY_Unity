using System.Collections.Generic;
using UnityEngine;

namespace KILROY.Project.Model
{
    #region Data

    /// <summary>
    /// 数据
    /// </summary>
    public static class GameLevel_1_Data
    {
        public static Dictionary<GameLevel_1_Camera, Camera> Camera = new Dictionary<GameLevel_1_Camera, Camera>(); // 相机列表

        public static GameLevel_1_Container Container = new GameLevel_1_Container(); // 容器
    }

    #endregion

    #region Structure

    /// <summary>
    /// 容器
    /// </summary>
    public class GameLevel_1_Container
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
    public enum GameLevel_1_Camera
    {
        Main // 主相机
    }

    #endregion
}