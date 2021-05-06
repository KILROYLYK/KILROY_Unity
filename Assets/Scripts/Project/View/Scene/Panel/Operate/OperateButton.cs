using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
using KILROY.Base;
using KILROY.Controller;

namespace KILROY.Project.View
{
    public class OperateButton : BaseBehaviour
    {
        #region Parameter

        private Button ButtonJump = null; // 跳跃按钮
        private Button ButtonAttack = null; // 攻击按钮
        private const string TweenId_1 = "ButtonJump"; // 动效标识-面板显隐
        private const string TweenId_2 = "ButtonAttack"; // 动效标识-面板显隐

        #endregion

        #region Cycle

        public void Awake()
        {
            FloatList.Add("ButtonTime", 0.1f); // 按钮持续时间

            ButtonJump = transform.Find("Operate/BoxButton/ButtonJump").GetComponent<Button>();
            ButtonJump.onClick.AddListener(ClickJump);

            ButtonAttack = transform.Find("Operate/BoxButton/ButtonAttack").GetComponent<Button>();
            ButtonAttack.onClick.AddListener(ClickAttack);
        }

        // public void Start() { }

        public void Update() { }

        #endregion

        #region Click

        /// <summary>
        /// 点击跳跃
        /// </summary>
        private void ClickJump()
        {
            InputController.Keyboard.Space = true;

            KillTween(TweenId_1);
            Sequence tween = DOTween.Sequence();
            tween.AppendInterval(FloatList["ButtonTime"]);
            tween.AppendCallback(() => { InputController.Keyboard.Space = false; });
            AddTween(TweenId_1, tween);
        }

        /// <summary>
        /// 点击攻击
        /// </summary>
        private void ClickAttack()
        {
            InputController.Mouse.Left = true;

            KillTween(TweenId_2);
            Sequence tween = DOTween.Sequence();
            tween.AppendInterval(FloatList["ButtonTime"]);
            tween.AppendCallback(() => { InputController.Mouse.Left = false; });
            AddTween(TweenId_2, tween);
        }

        #endregion
    }
}