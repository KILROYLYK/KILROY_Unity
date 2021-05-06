using UnityEngine;
using KILROY.Base;
using KILROY.Controller;
using KILROY.Project.Model;
using KILROY.Tool;

namespace KILROY.Project.View
{
    public class Role_1_Jump : BaseBehaviour
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
            float speed = 0;
            float x = InputController.Keyboard.AxisX;
            float y = InputController.Keyboard.AxisY;
            float angle = UIFN.GetAxiaDirection(x, y);

            if (!RoleData.State.IsJumpUp && !RoleData.State.IsJumpDown) return;

            if (RoleData.State.IsJumpUp && !RoleData.State.IsJumpDown) // 跳起
            {
                if (!SwitchList["IsUp"]) // 起跳
                {
                    SwitchList["IsUp"] = true;
                    FloatList["SpeedY"] = RoleData.Speed.RoleJump;
                }

                if (FloatList["SpeedY"] <= 0) // 切换下落
                {
                    RoleData.State.IsJumpUp = false;
                    RoleData.State.IsJumpDown = true;
                    SwitchList["IsUp"] = false;
                    SwitchList["IsDown"] = true;
                    FloatList["SpeedY"] = 0;

                    return;
                }
            }

            if (RoleData.State.IsJumpDown) // 下落
            {
                if (RoleData.State.IsGrounded) // 落地
                {
                    RoleData.State.IsJumpUp = false;
                    RoleData.State.IsJumpDown = false;
                    SwitchList["IsUp"] = false;
                    SwitchList["IsDown"] = false;
                    FloatList["SpeedY"] = 0;

                    return;
                }
            }

            FloatList["SpeedY"] -= RoleData.Speed.RoleJumpA;

            if (InputController.Keyboard.Move)
            {
                speed = RoleData.Speed.RoleWalk;
                if (InputController.Keyboard.ShiftLeftPress) speed = RoleData.Speed.RoleRun;
            }

            RoleData.Controller.Move(transform.rotation * (Quaternion.AngleAxis(angle, Vector3.up) * new Vector3(0, 0, speed)) +
                                     Quaternion.AngleAxis(angle, Vector3.up) * new Vector3(0, FloatList["SpeedY"], speed) * Time.deltaTime);
        }
    }
}