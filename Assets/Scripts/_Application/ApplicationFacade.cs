using PureMVC.Patterns.Facade;
using KILROY.Constant;
using KILROY.Constant.Name;
using KILROY.Model;
using KILROY.Tool;
using KILROY.Controller;
using KILROY.Project.Controller;

namespace KILROY.Application
{
    public class ApplicationFacade : Facade
    {
        #region Instance

        private static ApplicationFacade instance = null;

        public static ApplicationFacade Instance
        {
            get
            {
                if (instance == null)
                {
                    string name = FN.GetName(FacadeName.Application);
                    instance = new ApplicationFacade(name);
                }

                return instance;
            }
        }

        #endregion

        #region Constructor

        public ApplicationFacade(string name) : base(name) { }

        #endregion

        #region Initialize

        protected override void InitializeFacade()
        {
            base.InitializeFacade(); // 包含初始化Model、Controller、View

            // 注册Facade
        }

        protected override void InitializeController()
        {
            base.InitializeController();

            // 注册Command
            // RegisterCommand();
            RegisterCommand(FN.GetNotification(Notification.InitController), FN.CreateInstance<InitControllerCommand>);
            RegisterCommand(FN.GetNotification(Notification.InitScene), FN.CreateInstance<InitSceneCommand>);
        }

        protected override void InitializeModel()
        {
            base.InitializeModel();

            // 注册Proxy
            // RegisterProxy();
        }

        protected override void InitializeView()
        {
            base.InitializeView();

            // 注册Mediator
            // RegisterMediator();
        }

        #endregion

        /// <summary>
        /// 启动
        /// </summary>
        public void StartUp()
        {
            ApplicationData.Mode = AppMode.DevelopIntegration;

            FN.SendNotification(Notification.InitController);
            FN.SendNotification(Notification.InitScene);

            SceneController.Instance.ShowScene(
                SceneName.Panel,
                () =>
                {
                    FN.SendNotification(
                        Notification.ShowMask,
                        new NotificationData()
                        {
                            Callback = () =>
                            {
                                FN.SendNotification(
                                    Notification.ShowProgress,
                                    new NotificationData()
                                    {
                                        Data = SceneController.Instance.ShowScene(SceneName.GameHome),
                                        Callback = () => { FN.SendNotification(Notification.HideMask); }
                                    });
                            }
                        });
                });
        }

        /// <summary>
        /// 显示游戏1场景
        /// </summary>
        // public void ShowSceneGameLevel_1()
        // {
        //     FN.SendNotification(Notification.ShowMask, new NotificationData()
        //     {
        //         Callback = () =>
        //         {
        //             FN.SendNotification(Notification.ShowProgress, new NotificationData()
        //             {
        //                 Data = SceneController.Instance.ShowScene(SceneName.GameLevel_1),
        //                 Callback = () =>
        //                 {
        //                     FN.SendNotification(Notification.HideMask);
        //                     FN.SendNotification(Notification.ShowOperate);

        //                     InputController.Instance.OpenInput();
        //                 }
        //             });
        //         }
        //     });
        // }
    }
}