using UnityEngine;
using KILROY.Base;
using KILROY.Controller;
using KILROY.Project.Model;
using KILROY.Tool;

namespace KILROY.Project.View
{
    public class RoleFirstMove : BaseBehaviour
    {
        #region Parameter

        #endregion

        #region Cycle

        // public void Awake() { }

        // public void Start() { }

        public void Update() { UpdateMove(); }

        #endregion

        /// <summary>
        /// 更新移动
        /// </summary>
        private void UpdateMove()
        {
            RoleState state = RoleData.State;
            RoleSpeed speed = RoleData.Speed;
            float s = !state.IsRun ? speed.RoleWalk : speed.RoleRun;
            float x = InputController.Keyboard.AxisX;
            float y = InputController.Keyboard.AxisY;
            int angle = UIFN.GetAxiaDirection(x, y);

            if (!state.IsWalk) return; // 未移动

            if (state.IsSquat) s += speed.RoleSquat; // 蹲
            if (state.IsDown) s += speed.RoleDown; // 趴

            if (state.IsFly) s += speed.RoleFly; // 飞
            if (state.IsClimb) s += speed.RoleClimb; // 爬
            if (state.IsSwim) s += speed.RoleSwim; // 游

            RoleData.Controller.Move(transform.rotation * (Quaternion.AngleAxis(angle, Vector3.up) * new Vector3(0, 0, s)));
            RoleData.Camera.transform.position = transform.position + new Vector3(0, 1.5f, 0);
        }
    }
}