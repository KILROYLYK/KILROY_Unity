using System;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
using KILROY.Base;

namespace KILROY.Project.View
{
    public class MaskDefault : BaseBehaviour
    {
        #region Parameter

        private Image DefaultBG = null; // 默认背景

        #endregion

        #region Cycle

        public void Awake()
        {
            FloatList.Add("Fade", 1); // 显隐时间

            DefaultBG = transform.Find("DefaultBG").GetComponent<Image>();
        }

        // public void Start() { }

        // public void Update() { }

        public void OnDestroy() { KillTween(); }

        #endregion

        /// <summary>
        /// 显示遮罩
        /// </summary>
        /// <param name="callback">显示回调</param>
        public void ShowMask(Action callback = null)
        {
            Sequence tween = DOTween.Sequence();
            tween.AppendCallback(() => { DefaultBG.gameObject.SetActive(true); });
            tween.Append(DefaultBG.DOFade(1, FloatList["Fade"]).SetEase(Ease.Linear));
            tween.AppendCallback(() => { callback?.Invoke(); });
            AddTween(tween);
        }

        /// <summary>
        /// 隐藏遮罩
        /// </summary>
        /// <param name="callback">隐藏回调</param>
        public void HideMask(Action callback = null)
        {
            Sequence tween = DOTween.Sequence();
            tween.AppendCallback(() => { callback?.Invoke(); });
            tween.Append(DefaultBG.DOFade(0, FloatList["Fade"]).SetEase(Ease.Linear));
            tween.AppendCallback(() => { DefaultBG.gameObject.SetActive(false); });
            AddTween(tween);
        }
    }
}