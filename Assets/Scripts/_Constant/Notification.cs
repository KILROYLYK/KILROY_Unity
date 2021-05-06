namespace KILROY.Constant
{
    /// <summary>
    /// 通知
    /// </summary>
    public enum Notification
    {
        #region Application

        InitController,
        InitScene,

        #endregion

        #region Panel

        InitScenePanel,
        CleanScenePanel,
        ShowProgress, // 显示进度条
        ShowMask, // 显示幕布
        HideMask, // 隐藏幕布
        ShowOperate, // 显示操作台
        HideOperate, // 隐藏操作台

        #endregion

        #region Game

        InitSceneGameHome,
        CleanSceneGameHome,
        InitSceneGameLevel_1,
        CleanSceneGameLevel_1,

        #endregion

        #region Demo

        #if KILROY_DEMO
        InitSceneDemo,
        CleanSceneDemo,
        InitSceneDemo_1,
        CleanSceneDemo_1,
        #endif

        #endregion
    }
}