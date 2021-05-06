using KILROY.Base;

namespace KILROY.Project.View
{
    public abstract class ProjectBehaviour : BaseBehaviour
    {
        #region Event

        /// <summary>
        /// 添加事件监听
        /// </summary>
        protected virtual void AddListener() { }

        /// <summary>
        /// 移除事件监听
        /// </summary>
        protected virtual void RemoveListener() { }

        #endregion
    }
}