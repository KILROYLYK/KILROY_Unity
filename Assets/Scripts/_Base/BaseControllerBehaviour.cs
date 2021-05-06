using UnityEngine;
using KILROY.Tool;

namespace KILROY.Base
{
    public abstract class BaseControllerBehaviour<T> : BaseBehaviour where T : BaseControllerBehaviour<T>
    {
        #region Instance

        private static T instance = null;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    string[] name = typeof(T).ToString().Split('.');
                    GameObject controller = new GameObject(name[name.Length - 1]);
                    controller.transform.SetParent(Root.transform);
                    instance = controller.AddComponent<T>();
                }

                return instance;
            }
        }

        #endregion

        #region Parameter

        private static string Name = "Controller"; // 根节点名称

        private static GameObject Root // 根节点
        {
            get
            {
                GameObject controller = GameObject.Find(Name);
                if (controller == null)
                {
                    controller = new GameObject(Name);
                    controller.transform.SetAsFirstSibling();
                }

                return controller;
            }
        }

        #endregion

        #region Cycle

        /// <summary>
        /// 初始化
        /// </summary>
        public virtual void Init()
        {
            string[] name = typeof(T).ToString().Split('.');
            FN.Log("初始化：" + name[name.Length - 1]);
        }

        #endregion
    }
}