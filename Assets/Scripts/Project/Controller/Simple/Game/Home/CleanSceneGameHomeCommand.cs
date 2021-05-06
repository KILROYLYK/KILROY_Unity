using PureMVC.Interfaces;
using KILROY.Base;
using KILROY.Constant.Name;
using KILROY.Tool;
using KILROY.Project.Model;

namespace KILROY.Project.Controller
{
    public class CleanSceneGameHomeCommand : BaseSimpleCommand
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
        private void RemoveMediator() { FN.RemoveMVC(MediatorName.GameHome); }

        #endregion

        /// <summary>
        /// 初始化容器
        /// </summary>
        private void Clean()
        {
            FN.Log(FN.GetName(SceneName.GameHome) + "-清理");

            GameHomeData.Camera.Clear();
            GameHomeData.Container = new GameHomeContainer();
        }
    }
}