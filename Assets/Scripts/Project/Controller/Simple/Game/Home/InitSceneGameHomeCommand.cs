using UnityEngine;
using PureMVC.Interfaces;
using KILROY.Base;
using KILROY.Constant.Name;
using KILROY.Tool;
using KILROY.Model;
using KILROY.Project.View;
using KILROY.Project.Model;

namespace KILROY.Project.Controller
{
    public class InitSceneGameHomeCommand : BaseSimpleCommand
    {
        #region Parameter

        GameHomeBehaviour View = null; // 视图层

        #endregion

        public override void Execute(INotification notification)
        {
            NotificationData data = notification.Body as NotificationData;
            View = data.Data as GameHomeBehaviour;

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
        private void RegisterMediator() { FN.RegisterMVC(new GameHomeMediator(FN.GetName(MediatorName.GameHome), View)); }

        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            FN.Log(FN.GetName(SceneName.GameHome) + "-初始化");

            foreach (Transform item in View.transform) FN.SetObjectValue(PanelData.Container, item.name, item, false);
            GameHomeData.Camera.Add(GameHomeCamera.Main, GameHomeData.Container.BoxCamera.Find("Camera").GetComponent<Camera>());
        }
    }
}