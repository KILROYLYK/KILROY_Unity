using UnityEngine;
using KILROY.Base;
using KILROY.Controller;
using KILROY.Project.Model;
using KILROY.Tool;

namespace KILROY.Project.View
{
    public class RoleFirstJump : BaseBehaviour
    {
        #region Parameter

        #endregion

        #region Cycle

        public void Awake()
        {
            SwitchList.Add("IsUp", false); // 是否起跳
            SwitchList.Add("IsDown", false); // 是否下落

            FloatList.Add("SpeedWalk", 0.05f); // 走时跳跃速度
            FloatList.Add("SpeedRun", 0.08f); // 跑时跳跃速度
            FloatList.Add("SpeedJumpI", 8); // 跳跃初始速度
            FloatList.Add("SpeedJumpA", 0.3f); // 跳跃加速度
            FloatList.Add("SpeedJump", 0); // 跳跃速度
        }

        // public void Start() { }

        public void Update() { UpdateJump(); }

        #endregion

        /// <summary>
        /// 更新跳跃
        /// </summary>
        private void UpdateJump()
        {
            RoleState state = RoleData.State;
            float s = 0;
            float x = InputController.Keyboard.AxisX;
            float y = InputController.Keyboard.AxisY;
            float angle = UIFN.GetAxiaDirection(x, y);

            if (!state.IsJumpUp && !state.IsJumpDown) return;

            if (state.IsJumpUp && !state.IsJumpDown) // 跳起
            {
                if (!SwitchList["IsUp"]) // 起跳
                {
                    SwitchList["IsUp"] = true;
                    FloatList["SpeedJump"] = FloatList["SpeedJumpI"];
                }

                if (FloatList["SpeedJump"] <= 0) // 切换下落
                {
                    state.IsJumpUp = false;
                    state.IsJumpDown = true;
                    SwitchList["IsUp"] = false;
                    SwitchList["IsDown"] = true;
                    FloatList["SpeedJump"] = 0;
                    return;
                }
            }

            if (state.IsJumpDown) // 下落
            {
                if (state.IsGrounded) // 落地
                {
                    state.IsJumpUp = false;
                    state.IsJumpDown = false;
                    SwitchList["IsUp"] = false;
                    SwitchList["IsDown"] = false;
                    FloatList["SpeedJump"] = 0;
                    return;
                }
            }

            FloatList["SpeedJump"] -= FloatList["SpeedJumpA"];

            if (InputController.Keyboard.Move) s = !InputController.Keyboard.ShiftLeftPress ? FloatList["SpeedWalk"] : FloatList["SpeedRun"];

            RoleData.Controller.Move(
                transform.rotation * (Quaternion.AngleAxis(angle, Vector3.up) * new Vector3(0, 0, s)) +
                Quaternion.AngleAxis(angle, Vector3.up) * new Vector3(0, FloatList["SpeedJump"], s) * Time.deltaTime
            );
        }
    }
}