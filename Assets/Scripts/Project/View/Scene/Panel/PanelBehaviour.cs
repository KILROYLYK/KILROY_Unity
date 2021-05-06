using KILROY.Constant;
using KILROY.Tool;
using KILROY.Model;

namespace KILROY.Project.View
{
    public class PanelBehaviour : ProjectBehaviour
    {
        #region Cycle

        public void Awake()
        {
            if (ApplicationData.Mode == AppMode.DevelopSelf)
            {
                FN.SendNotification(Notification.InitController);
                FN.SendNotification(Notification.InitScene);
            }

            FN.SendNotification(Notification.InitScenePanel, new NotificationData() { Data = this });
        }

        // public void Start() { }

        // public void Update() { }

        public void OnDestroy() { FN.SendNotification(Notification.CleanScenePanel); }

        #endregion
    }
}