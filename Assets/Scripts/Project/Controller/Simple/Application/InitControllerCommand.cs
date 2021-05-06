using PureMVC.Interfaces;
using KILROY.Base;
using KILROY.Controller;

namespace KILROY.Project.Controller
{
    public class InitControllerCommand : BaseSimpleCommand
    {
        public override void Execute(INotification notification)
        {
            LuaController.Instance.Init();
            DebugController.Instance.Init();
            AsyncController.Instance.Init();
            NetController.Instance.Init();
            FileController.Instance.Init();
            InputController.Instance.Init();
            SceneController.Instance.Init();
            LightController.Instance.Init();
            // AudioController.Instance.Init();
            // VideoController.Instance.Init();
            // Live2DController.Instance.Init();
        }
    }
}