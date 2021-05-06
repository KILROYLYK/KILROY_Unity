using PureMVC.Interfaces;
using KILROY.Base;
using KILROY.Constant.Name;
using KILROY.Tool;
using KILROY.Project.Controller;

namespace KILROY.Project.Demo
{
    public class CleanSceneDemo_1_Command : BaseSimpleCommand
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
        private void RemoveMediator() { FN.RemoveMVC(MediatorName.Demo_1); }

        #endregion

        /// <summary>
        /// 初始化容器
        /// </summary>
        private void Clean()
        {
            FN.Log(FN.GetName(SceneName.Demo_1) + "-清理");

            Demo_1_Data.Camera.Clear();
            Demo_1_Data.Container = new Demo_1_Container();
        }
    }
}