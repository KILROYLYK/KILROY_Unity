using UnityEngine;
using PureMVC.Interfaces;
using KILROY.Base;
using KILROY.Constant.Name;
using KILROY.Model;
using KILROY.Tool;

namespace KILROY.Project.Demo
{
    public class InitSceneDemoCommand : BaseSimpleCommand
    {
        #region Parameter

        DemoBehaviour View = null; // 视图层

        #endregion

        public override void Execute(INotification notification)
        {
            NotificationData data = notification.Body as NotificationData;
            View = data.Data as DemoBehaviour;

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
        private void RegisterMediator() { FN.RegisterMVC(new DemoMediator(FN.GetName(MediatorName.Demo), View)); }

        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            FN.Log(FN.GetName(SceneName.Demo) + "-初始化");

            foreach (Transform item in View.transform) FN.SetObjectValue(DemoData.Container, item.name, item, false);
            DemoData.Camera.Add(DemoCamera.Main, DemoData.Container.BoxCamera.Find("Camera").GetComponent<Camera>());
        }
    }
}