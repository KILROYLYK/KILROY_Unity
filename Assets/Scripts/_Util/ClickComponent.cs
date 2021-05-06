using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using KILROY.Controller;

namespace KILROY.Util
{
    public class ClickComponent : Button
    {
        #region Parameter

        private PointerEventData EventData = null; // 按下数据
        private Dictionary<string, float> ClickTime = new Dictionary<string, float>(); // 时间
        private bool IsPress = false; // 是否按下
        private bool IsLongPress = false; // 是否长按
        private bool IsContinuous = false; // 是否连续点击
        private int ClickCount = 0; // 按下次数
        private int Collaboration = 0; // 协同程序

        #endregion

        #region Event
        
        public new class ButtonClickedEvent : UnityEvent<PointerEventData>
        {
        }

        public new ButtonClickedEvent onClick = new ButtonClickedEvent(); // 单击事件
        public ButtonClickedEvent onDoubleClick = new ButtonClickedEvent(); // 双击事件
        public ButtonClickedEvent onLongPress = new ButtonClickedEvent(); // 长按事件

        #endregion

        #region Cycle

        protected override void Awake()
        {
            base.Awake();

            ClickTime.Add("ClickInterval", 0.17f);
            ClickTime.Add("LongPress", 0.4f);
            ClickTime.Add("Press", 0);
        }

        // public void Start() { }

        public void Update() { CheckLongPress(); }

        #endregion

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            StartPress(eventData);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            StopPress();
        }

        public override void OnPointerEnter(PointerEventData eventData) { base.OnPointerEnter(eventData); }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            StopPress();
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (!IsInteractable()) return; // 交互关闭
            if (IsLongPress) return; // 长按则弹出

            if (!IsContinuous)
            {
                onClick.Invoke(EventData);
                EventData = null;
                return;
            }

            if (ClickCount == 2) return; // 2次以上不执行

            ClickCount++;

            if (Collaboration != 0) AsyncController.Instance.StopCollaboration(Collaboration);
            Collaboration = AsyncController.Instance.StartCollaboration
            (
                new WaitForSeconds(ClickTime["ClickInterval"]),
                () =>
                {
                    if (ClickCount == 1) onClick.Invoke(EventData);
                    else if (ClickCount == 2) onDoubleClick.Invoke(EventData);

                    Collaboration = 0;
                    ClickCount = 0;
                    EventData = null;
                }
            );
        }

        /// <summary>
        /// 开启连续点击
        /// </summary>
        /// <param name="isOpen">是否打开连续点击</param>
        public void Continuous(bool isOpen = true) { IsContinuous = isOpen; }

        /// <summary>
        /// 开始按
        /// </summary>
        /// <param name="eventData">事件</param>
        private void StartPress(PointerEventData eventData)
        {
            if (!IsInteractable()) return; // 交互关闭

            IsPress = true;
            IsLongPress = false; // 按下时重置长按状态，防止抬起时执行单击事件
            EventData = eventData;
            ClickTime["Press"] = Time.time;
        }

        /// <summary>
        /// 停止按
        /// </summary>
        private void StopPress()
        {
            if (!IsInteractable()) return; // 交互关闭

            IsPress = false;
        }

        /// <summary>
        /// 检查按的状态
        /// </summary>
        private void CheckLongPress()
        {
            if (!IsInteractable()) return; // 交互关闭
            if (!IsPress || IsLongPress) return; // 没有按下或已经长按
            if (Time.time < ClickTime["Press"] + ClickTime["LongPress"]) return; // 没有达到长按时间

            IsLongPress = true;
            onLongPress?.Invoke(EventData);
        }
    }
}