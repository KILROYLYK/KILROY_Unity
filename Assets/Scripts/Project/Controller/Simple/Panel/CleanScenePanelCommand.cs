using PureMVC.Interfaces;
using KILROY.Base;
using KILROY.Constant.Name;
using KILROY.Tool;
using KILROY.Project.Model;

namespace KILROY.Project.Controller
{
    public class CleanScenePanelCommand : BaseSimpleCommand
    {
        public override void Execute(INotification notification)
        {
            RemoveCommand();
            RemoveProxy();
            RemoveMediator();
            Clean();
        }

        #region MVC

        /// <summary>
        /// 注册Command
        /// </summary>
        private void RemoveCommand() { }

        /// <summary>
        /// 注册Proxy
        /// </summary>
        private void RemoveProxy() { }

        /// <summary>
        /// 注册Mediator
        /// </summary>
        private void RemoveMediator() { FN.RemoveMVC(MediatorName.Panel); }

        #endregion

        /// <summary>
        /// 初始化容器
        /// </summary>
        private void Clean()
        {
            FN.Log(FN.GetName(SceneName.Panel) + "-清理");

            PanelData.Camera.Clear();
            PanelData.Container = new PanelContainer();
        }
    }
}