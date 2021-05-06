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
    public class InitScenePanelCommand : BaseSimpleCommand
    {
        #region Parameter

        PanelBehaviour View = null; // 视图层

        #endregion

        public override void Execute(INotification notification)
        {
            NotificationData data = notification.Body as NotificationData;
            View = data.Data as PanelBehaviour;

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
        private void RegisterMediator() { FN.RegisterMVC(new PanelMediator(FN.GetName(MediatorName.Panel), View)); }

        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            FN.Log(FN.GetName(SceneName.Panel) + "-初始化");

            foreach (Transform item in View.transform) FN.SetObjectValue(PanelData.Container, item.name, item, false);
            PanelData.Camera.Add(PanelCamera.Main, PanelData.Container.BoxCamera.Find("Camera").GetComponent<Camera>());
        }
    }
}