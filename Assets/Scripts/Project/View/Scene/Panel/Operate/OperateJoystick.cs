using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using KILROY.Base;
using KILROY.Tool;
using KILROY.Controller;

namespace KILROY.Project.View
{
    public class OperateJoystick : BaseBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        #region Parameter

        private Camera Camera = null; // 相机
        private Canvas Canvas = null; // 画布
        private RectTransform Panel = null; // 面板
        private RectTransform Pole = null; // 摇杆
        private Vector2 InitPosition = new Vector2(-130, -200); // 初始位置
        private Vector2 InputPosition = Vector2.zero; // 输入位置
        private const string TweenId = "PanelFade"; // 动效标识-面板显隐

        #endregion

        #region Cycle

        public void Awake()
        {
            FloatList.Add("PanelFade", 0.3f); // 面板显隐透明度
            FloatList.Add("PanelFadeTime", 0.2f); // 面板显隐时间

            Camera = GameObject.Find("BoxCamera/Camera/").GetComponent<Camera>();
            Canvas = GameObject.Find("BoxCanvas/CanvasOperate").GetComponent<Canvas>();
            Panel = transform.Find("Panel").GetComponent<RectTransform>();
            Pole = Panel.Find("Pole").GetComponent<RectTransform>();

            FadePanel(false);
        }

        // public void Start() { }

        public void Update() { InputController.InputAxis = InputPosition; }

        public void OnDestroy() { KillTween(); }

        #endregion

        #region Drag

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            SetPanel(eventData);
            FadePanel();

            OnDrag(eventData);
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            SetPanel();
            FadePanel(false);
            ScalePanel();

            Pole.anchoredPosition = Vector3.zero;
            InputPosition = Vector2.zero;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 position = RectTransformUtility.WorldToScreenPoint(Camera, Panel.position);
            Vector2 radius = Panel.sizeDelta / 2;

            InputPosition = (eventData.position - position) / (radius * Canvas.scaleFactor);

            HandleInput();

            Pole.anchoredPosition = InputPosition * radius;
        }

        #endregion

        /// <summary>
        /// 设置面板位置
        /// </summary>
        /// <param name="eventData">事件数据</param>
        private void SetPanel(PointerEventData eventData = null)
        {
            if (eventData == null) // 重置位置
            {
                UIFN.SetRectPosition(Panel, InitPosition, true);
                return;
            }

            Vector3 position = Camera.ScreenToWorldPoint(eventData.position);
            position.z = Panel.position.z;
            Panel.position = position;
        }

        /// <summary>
        /// 显隐面板
        /// </summary>
        /// <param name="isShow">是否显示</param>
        private void FadePanel(bool isShow = true)
        {
            Image panelImage = Panel.GetComponent<Image>();
            Image poleImage = Pole.GetComponent<Image>();

            KillTween(TweenId);
            Sequence tween = DOTween.Sequence();
            tween.Append(panelImage.DOFade(isShow ? 1 : FloatList["PanelFade"], FloatList["PanelFadeTime"])).SetEase(Ease.InOutSine);
            tween.Join(poleImage.DOFade(isShow ? 1 : FloatList["PanelFade"], FloatList["PanelFadeTime"])).SetEase(Ease.InOutSine);
            AddTween(TweenId, tween);
        }

        /// <summary>
        /// 缩放面板
        /// </summary>
        /// <param name="isScale">是否缩放</param>
        private void ScalePanel(bool isScale = false) { Panel.localScale = isScale ? new Vector3(1.5f, 1.5f, 1.5f) : Vector3.one; }

        /// <summary>
        /// 处理输入
        /// </summary>
        private void HandleInput()
        {
            float magnitude = InputPosition.magnitude;

            ScalePanel();

            if (magnitude > 0)
            {
                if (magnitude > 0.5) ScalePanel(true);
                if (magnitude > 1) InputPosition = InputPosition.normalized;
            }
            else
            {
                InputPosition = Vector2.zero;
            }
        }
    }
}