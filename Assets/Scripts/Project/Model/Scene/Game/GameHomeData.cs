using System.Collections.Generic;
using UnityEngine;

namespace KILROY.Project.Model
{
    #region Data

    /// <summary>
    /// 数据
    /// </summary>
    public static class GameHomeData
    {
        public static Dictionary<GameHomeCamera, Camera> Camera = new Dictionary<GameHomeCamera, Camera>(); // 相机列表
        public static GameHomeContainer Container = new GameHomeContainer(); // 容器
    }

    #endregion

    #region Structure

    /// <summary>
    /// 容器
    /// </summary>
    public class GameHomeContainer
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
    public enum GameHomeCamera
    {
        Main // 主相机
    }

    #endregion
}