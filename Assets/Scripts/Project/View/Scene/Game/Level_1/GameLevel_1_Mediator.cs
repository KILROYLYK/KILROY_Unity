using PureMVC.Interfaces;

namespace KILROY.Project.View
{
    public class GameLevel_1_Mediator : ProjectMediator
    {
        #region Constructor

        public GameLevel_1_Mediator(string name, ProjectBehaviour viewComponent = null) : base(name, viewComponent) { }

        #endregion

        #region Notification

        protected override void ExpansionNotification() { }

        public override void HandleNotification(INotification notification)
        {
            GameLevel_1_Behaviour view = ViewComponent as GameLevel_1_Behaviour;

            // if (notification.Name == FN.GetNotification(Notification.))
            // {
            //     SendNotification(FN.GetNotification(Notification.));
            // }
        }

        #endregion

        #region Cycle

        protected override void Init() { }

        #endregion
    }
}