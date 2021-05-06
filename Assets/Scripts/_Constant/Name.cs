namespace KILROY.Constant.Name
{
    /// <summary>
    /// 场景名称
    /// </summary>
    public enum SceneName
    {
        Application, // 应用
        Panel, // 面板

        #region Game

        GameHome, // 游戏-首页
        GameLevel_1, // 游戏-关卡1

        #endregion

        #region Demo

        #if KILROY_DEMO
        Demo,
        Demo_1,
        #endif

        #endregion
    }

    /// <summary>
    /// 外观模式名称
    /// </summary>
    public enum FacadeName
    {
        Application // 应用
    }

    /// <summary>
    /// 代理模式名称
    /// </summary>
    public enum ProxyName
    {
        #region Game

        GameLevel_1, // 游戏-关卡1

        #endregion

        #region Demo

        #if KILROY_DEMO
        Demo,
        Demo_1,
        #endif

        #endregion
    }

    /// <summary>
    /// 中介者模式名称
    /// </summary>
    public enum MediatorName
    {
        Panel, // 面板
        GameHome, // 游戏-首页

        #region Game

        GameLevel_1, // 游戏-关卡1

        #endregion

        #region Demo

        #if KILROY_DEMO
        Demo,
        Demo_1,
        #endif

        #endregion
    }
}