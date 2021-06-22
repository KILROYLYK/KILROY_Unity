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
            SwitchList.Add("IsUp", false);
            SwitchList.Add("IsDown", false);

            FloatList.Add("SpeedY", 0);
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
            RoleSpeed speed = RoleData.Speed;
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
                    FloatList["SpeedY"] = speed.RoleJump;
                }

                if (FloatList["SpeedY"] <= 0) // 切换下落
                {
                    state.IsJumpUp = false;
                    state.IsJumpDown = true;
                    SwitchList["IsUp"] = false;
                    SwitchList["IsDown"] = true;
                    FloatList["SpeedY"] = 0;
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
                    FloatList["SpeedY"] = 0;
                    return;
                }
            }

            FloatList["SpeedY"] -= speed.RoleJumpA;

            if (InputController.Keyboard.Move)
            {
                s = speed.RoleWalk;
                if (InputController.Keyboard.ShiftLeftPress) s = speed.RoleRun;
            }

            RoleData.Controller.Move(
                transform.rotation * (Quaternion.AngleAxis(angle, Vector3.up) * new Vector3(0, 0, s)) +
                Quaternion.AngleAxis(angle, Vector3.up) * new Vector3(0, FloatList["SpeedY"], s) * Time.deltaTime
            );
        }
    }
}