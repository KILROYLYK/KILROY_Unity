using KILROY.Constant;
using KILROY.Model;
using KILROY.Tool;
using KILROY.Controller;

namespace KILROY.Project.View
{
    public class GameLevel_1_Behaviour : ProjectBehaviour
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

            FN.SendNotification(Notification.InitSceneGameLevel_1, new NotificationData() { Data = this });
        }

        // public void Start() { }

        // public void Update() { }

        public void OnDestroy() { FN.SendNotification(Notification.CleanSceneGameLevel_1); }

        #endregion
    }
}