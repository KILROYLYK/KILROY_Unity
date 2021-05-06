using KILROY.Constant;
using KILROY.Model;
using KILROY.Tool;
using KILROY.Controller;
using KILROY.Project.View;

namespace KILROY.Project.Demo
{
    public class DemoBehaviour : ProjectBehaviour
    {
        #region Cycle

        public void Awake()
        {
            if (ApplicationData.Mode == AppMode.DevelopSelf)
            {
                FN.SendNotification(Notification.InitController);
                FN.SendNotification(Notification.InitScene);

                InputController.Instance.OpenInput();
            }

            FN.SendNotification(Notification.InitSceneDemo, new NotificationData() { Data = this });
        }

        // public void Start() { }

        // public void Update() { }

        public void OnDestroy() { FN.SendNotification(Notification.CleanSceneDemo); }

        #endregion
    }
}