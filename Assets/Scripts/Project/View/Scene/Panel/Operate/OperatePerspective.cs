using UnityEngine;
using UnityEngine.EventSystems;
using KILROY.Base;

namespace KILROY.Project.View
{
    public class OperatePerspective : BaseBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        #region Parameter

        #endregion

        #region Cycle

        public void Awake()
        {
            SwitchList.Add("IsTouch", false); // 是否触摸

            FloatList.Add("TouchDelay", 10); // 按钮持续时间

            #if KILROY_PHONE
            CinemachineCore.GetInputAxis = AxisInput;
            #endif

            #if KILROY_PC
            gameObject.SetActive(false);
            #endif
        }

        // public void Start() { }

        // public void Update() { }

        #endregion

        #region Drag

        public virtual void OnPointerDown(PointerEventData eventData) { SwitchList["IsTouch"] = true; }

        public virtual void OnPointerUp(PointerEventData eventData) { SwitchList["IsTouch"] = false; }

        #endregion

        /// <summary>
        /// 向量输入
        /// </summary>
        /// <param name="touchName">向量名称</param>
        /// <returns>向量值</returns>
        private float AxisInput(string touchName)
        {
            if (SwitchList["IsTouch"])
            {
                switch (touchName)
                {
                    case "Mouse X":
                        if (Input.touchCount > 0) return Input.touches[0].deltaPosition.x / FloatList["TouchDelay"];
                        return Input.GetAxis(touchName);
                    case "Mouse Y":
                        if (Input.touchCount > 0) return Input.touches[0].deltaPosition.y / FloatList["TouchDelay"];
                        return Input.GetAxis(touchName);
                    default:
                        break;
                }
            }

            return 0;
        }
    }
}