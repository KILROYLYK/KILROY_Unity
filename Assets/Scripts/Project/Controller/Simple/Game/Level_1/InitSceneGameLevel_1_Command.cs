using UnityEngine;
using PureMVC.Interfaces;
using KILROY.Base;
using KILROY.Constant.Name;
using KILROY.Model;
using KILROY.Tool;
using KILROY.Project.View;
using KILROY.Project.Model;

namespace KILROY.Project.Controller
{
    public class InitSceneGameLevel_1_Command : BaseSimpleCommand
    {
        #region Parameter

        GameLevel_1_Behaviour View = null; // 视图层

        #endregion

        public override void Execute(INotification notification)
        {
            NotificationData data = notification.Body as NotificationData;
            View = data.Data as GameLevel_1_Behaviour;

            Init();
            RegisterCommand();
            RegisterProxy();
            RegisterMediator();
        }

        #region MVC

        /// <summary>
        /// 注册Command
        /// </summary>
        private void RegisterCommand() { }

        /// <summary>
        /// 注册Proxy
        /// </summary>
        private void RegisterProxy() { }

        /// <summary>
        /// 注册Mediator
        /// </summary>
        private void RegisterMediator() { FN.RegisterMVC(new GameLevel_1_Mediator(FN.GetName(MediatorName.GameLevel_1), View)); }

        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            FN.Log(FN.GetName(SceneName.GameLevel_1) + "-初始化");

            foreach (Transform item in View.transform) FN.SetObjectValue(GameLevel_1_Data.Container, item.name, item, false);
            GameLevel_1_Data.Camera.Add(GameLevel_1_Camera.Main, GameLevel_1_Data.Container.BoxCamera.Find("Camera").GetComponent<Camera>());
        }
    }
}