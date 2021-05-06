using System.Collections.Generic;
using PureMVC.Patterns.Mediator;
using XLua;
using KILROY.Constant;
using KILROY.Tool;

namespace KILROY.Base
{
    [Hotfix]
    public abstract class BaseMediator : Mediator
    {
        #region Parameter

        public List<string> NotificationList = new List<string>(); // 通知列表

        #endregion

        #region Constructor

        public BaseMediator(string name, BaseBehaviour viewComponent = null) : base(name, viewComponent) { }

        #endregion

        #region Notification

        public override string[] ListNotificationInterests()
        {
            ExpansionNotification();
            return NotificationList.ToArray();
        }

        /// <summary>
        /// 添加通知
        /// </summary>
        protected void AddNotification(Notification notification) { NotificationList.Add(FN.GetNotification(notification)); }

        /// <summary>
        /// 扩展通知
        /// </summary>
        protected virtual void ExpansionNotification() { }

        #endregion

        #region Cycle

        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void Init() { }

        /// <summary>
        /// 显示
        /// </summary>
        protected virtual void Show() { }

        /// <summary>
        /// 隐藏
        /// </summary>
        protected virtual void Hide() { }

        /// <summary>
        /// 清理
        /// </summary>
        protected virtual void Clean() { }

        #endregion
    }
}