using System;
using PureMVC.Interfaces;
using UnityEngine;
using KILROY.Constant;
using KILROY.Model;
using KILROY.Tool;

namespace KILROY.Project.View
{
    public class PanelMediator : ProjectMediator
    {
        #region Parameter

        private Transform CanvasTips = null; // 提示容器
        private Transform CanvasPopup = null; // 弹窗容器
        private Transform CanvasMask = null; // 遮罩容器
        private Transform CanvasLoad = null; // 加载容器
        private Transform CanvasMenu = null; // 菜单容器
        private Transform CanvasOperate = null; // 操作容器

        #endregion

        #region Constructor

        public PanelMediator(string name, ProjectBehaviour viewComponent = null) : base(name, viewComponent)
        {
            PanelBehaviour view = viewComponent as PanelBehaviour;
            CanvasTips = view.transform.Find("BoxCanvas/CanvasTips");
            CanvasPopup = view.transform.Find("BoxCanvas/CanvasPopup");
            CanvasMask = view.transform.Find("BoxCanvas/CanvasMask");
            CanvasLoad = view.transform.Find("BoxCanvas/CanvasLoad");
            CanvasMenu = view.transform.Find("BoxCanvas/CanvasMenu");
            CanvasOperate = view.transform.Find("BoxCanvas/CanvasOperate");
        }

        #endregion

        #region Notification

        protected override void ExpansionNotification()
        {
            AddNotification(Notification.ShowProgress);
            AddNotification(Notification.ShowMask);
            AddNotification(Notification.HideMask);
            AddNotification(Notification.ShowOperate);
            AddNotification(Notification.HideOperate);
        }

        public override void HandleNotification(INotification notification)
        {
            NotificationData data = notification.Body as NotificationData;
            if (notification.Name == FN.GetNotification(Notification.ShowProgress)) CanvasLoad.GetComponent<LoadProgress>().ShowProgress((AsyncOperation)data.Data, data.Callback);
            if (notification.Name == FN.GetNotification(Notification.ShowMask)) CanvasMask.GetComponent<MaskDefault>().ShowMask((Action)data?.Callback);
            if (notification.Name == FN.GetNotification(Notification.HideMask)) CanvasMask.GetComponent<MaskDefault>().HideMask((Action)data?.Callback);
            if (notification.Name == FN.GetNotification(Notification.ShowOperate)) CanvasOperate.Find("Operate").gameObject.SetActive(true);
            if (notification.Name == FN.GetNotification(Notification.HideOperate)) CanvasOperate.Find("Operate").gameObject.SetActive(false);
        }

        #endregion
    }
}