using UnityEngine;
using PureMVC.Interfaces;
using KILROY.Base;
using KILROY.Constant.Name;
using KILROY.Model;
using KILROY.Tool;
using KILROY.Project.Controller;

namespace KILROY.Project.Demo
{
    public class InitSceneDemo_1_Command : BaseSimpleCommand
    {
        #region Parameter

        Demo_1_Behaviour View = null; // 视图层

        #endregion

        public override void Execute(INotification notification)
        {
            NotificationData data = notification.Body as NotificationData;
            View = data.Data as Demo_1_Behaviour;

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
        private void RegisterMediator() { FN.RegisterMVC(new Demo_1_Mediator(FN.GetName(MediatorName.Demo_1), View)); }

        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            FN.Log(FN.GetName(SceneName.Demo_1) + "-初始化");

            foreach (Transform item in View.transform) FN.SetObjectValue(Demo_1_Data.Container, item.name, item, false);
            Demo_1_Data.Camera.Add(Demo_1_Camera.Main, Demo_1_Data.Container.BoxCamera.Find("Camera").GetComponent<Camera>());
            Demo_1_Data.Camera.Add(Demo_1_Camera.Snake, Demo_1_Data.Container.BoxCamera.Find("CameraSnake").GetComponent<Camera>());
        }
    }
}