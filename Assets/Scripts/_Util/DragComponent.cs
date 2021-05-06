using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using XLua;

namespace KILROY.Util
{
    public class DragComponent : Selectable, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        #region Parameter

        private bool IsDrag = false; // 是否拖拽

        #endregion

        #region Event
        
        public class ClickedEvent : UnityEvent<PointerEventData>
        {
        }

        public ClickedEvent onBeginDrag = new ClickedEvent(); // 开始拖动
        public ClickedEvent onDrag = new ClickedEvent(); // 拖动
        public ClickedEvent onEndDrag = new ClickedEvent(); // 结束拖动

        #endregion

        #region Cycle

        // public void Awake() { }

        // public void Start() { }

        // public void Update() { }

        #endregion

        /// <summary>
        /// 开始拖动
        /// </summary>
        /// <param name="eventData">事件</param>
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!IsInteractable()) return; // 交互关闭
            if (IsDrag) return; // 正在拖拽

            IsDrag = true;
            onBeginDrag?.Invoke(eventData);
        }

        /// <summary>
        /// 拖动中
        /// </summary>
        /// <param name="eventData">事件</param>
        public void OnDrag(PointerEventData eventData)
        {
            if (!IsInteractable()) return; // 交互关闭
            if (!IsDrag) return; // 未拖拽
            if (!IsContainsPoint(eventData)) return; // 在屏幕内拖拽

            onDrag?.Invoke(eventData);
        }

        /// <summary>
        /// 拖动结束
        /// </summary>
        /// <param name="eventData">事件</param>
        public void OnEndDrag(PointerEventData eventData)
        {
            if (!IsInteractable()) return; // 交互关闭
            if (!IsDrag) return; // 未拖拽

            IsDrag = false;
            onEndDrag?.Invoke(eventData);
        }

        /// <summary>
        /// 判断拖动点是否在物体内
        /// </summary>
        /// <param name="eventData">事件</param>
        /// <returns>是否在屏幕内</returns>
        private bool IsContainsPoint(PointerEventData eventData)
        {
            Camera camera = eventData.pressEventCamera;
            RectTransform rect = gameObject.GetComponent<RectTransform>();
            return RectTransformUtility.RectangleContainsScreenPoint(rect, eventData.position, camera);
        }
    }
}