using PureMVC.Interfaces;
using KILROY.Base;
using KILROY.Constant;
using KILROY.Tool;
using KILROY.Project.Demo;

namespace KILROY.Project.Controller
{
    public class InitSceneCommand : BaseSimpleCommand
    {
        public override void Execute(INotification notification)
        {
            FN.RegisterMVC(Notification.InitScenePanel, FN.CreateInstance<InitScenePanelCommand>);
            FN.RegisterMVC(Notification.CleanScenePanel, FN.CreateInstance<CleanScenePanelCommand>);

            #region Game

            FN.RegisterMVC(Notification.InitSceneGameHome, FN.CreateInstance<InitSceneGameHomeCommand>);
            FN.RegisterMVC(Notification.CleanSceneGameHome, FN.CreateInstance<CleanSceneGameHomeCommand>);

            FN.RegisterMVC(Notification.InitSceneGameLevel_1, FN.CreateInstance<InitSceneGameLevel_1_Command>);
            FN.RegisterMVC(Notification.CleanSceneGameLevel_1, FN.CreateInstance<CleanSceneGameLevel_1_Command>);

            #endregion

            #region Demo

            #if KILROY_DEMO
            FN.RegisterMVC(Notification.InitSceneDemo, FN.CreateInstance<InitSceneDemoCommand>);
            FN.RegisterMVC(Notification.CleanSceneDemo, FN.CreateInstance<CleanSceneDemoCommand>);
            FN.RegisterMVC(Notification.InitSceneDemo_1, FN.CreateInstance<InitSceneDemo_1_Command>);
            FN.RegisterMVC(Notification.CleanSceneDemo_1, FN.CreateInstance<CleanSceneDemo_1_Command>);
            #endif

            #endregion
        }
    }
}