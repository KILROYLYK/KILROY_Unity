using KILROY.Base;

namespace KILROY.Project.View
{
    public abstract class ProjectMediator : BaseMediator
    {
        #region Constructor

        public ProjectMediator(string name, BaseBehaviour viewComponent = null) : base(name, viewComponent) { }

        #endregion

        #region Event

        /// <summary>
        /// 添加事件
        /// </summary>
        protected virtual void AddEvent() { }

        /// <summary>
        /// 移除事件
        /// </summary>
        protected virtual void RemoveEvent() { }

        #endregion
    }
}