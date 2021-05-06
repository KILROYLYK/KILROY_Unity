using KILROY.Constant;
using KILROY.Model;
using KILROY.Tool;
using KILROY.Project.Model;

namespace KILROY.Project.View
{
    public class GameHomeBehaviour : ProjectBehaviour
    {
        #region Cycle

        public void Awake()
        {
            if (ApplicationData.Mode == AppMode.DevelopSelf)
            {
                FN.SendNotification(Notification.InitController);
                FN.SendNotification(Notification.InitScene);
            }

            FN.SendNotification(Notification.InitSceneGameHome, new NotificationData() { Data = this });
        }

        // public void Start() { }

        // public void Update() { }

        public void OnDestroy() { FN.SendNotification(Notification.CleanSceneGameHome); }

        #endregion
    }
}