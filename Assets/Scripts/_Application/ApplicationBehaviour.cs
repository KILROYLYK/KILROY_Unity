using KILROY.Base;

namespace KILROY.Application
{
    public class ApplicationBehaviour : BaseBehaviour
    {
        #region Instance

        private static ApplicationBehaviour instance = null;

        public static ApplicationBehaviour Instance
        {
            get { return instance; }
        }

        #endregion

        #region Cycle

        public void Awake()
        {
            if (instance == null) // 无实例
            {
                instance = this;
                ApplicationFacade.Instance.StartUp();
            }
            else if (this != instance) // 销毁其他实例
            {
                Destroy(this);
            }
        }

        // public void Start() { }

        // public void Update() { }

        #endregion
    }
}