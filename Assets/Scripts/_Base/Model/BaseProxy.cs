using PureMVC.Patterns.Proxy;
using XLua;

namespace KILROY.Base
{
    [Hotfix]
    public abstract class BaseProxy : Proxy
    {
        #region Constructor

        public BaseProxy(string name, object data = null) : base(name, data) { }

        #endregion

        #region Cycle

        /// <summary>
        /// 初始化数据
        /// </summary>
        protected virtual void InitData() { }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="newData">新数据</param>
        protected virtual void UpdateData(object newData) { }

        /// <summary>
        /// 清理数据
        /// </summary>
        protected virtual void CleanData() { }

        #endregion
    }
}