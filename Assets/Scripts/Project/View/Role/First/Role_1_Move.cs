using UnityEngine;
using KILROY.Base;
using KILROY.Controller;
using KILROY.Project.Model;
using KILROY.Tool;

namespace KILROY.Project.View
{
    public class Role_1_Move : BaseBehaviour
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
            float speed = !RoleData.State.IsRun ? RoleData.Speed.RoleWalk : RoleData.Speed.RoleRun;
            float x = InputController.Keyboard.AxisX;
            float y = InputController.Keyboard.AxisY;
            int angle = UIFN.GetAxiaDirection(x, y);

            if (!RoleData.State.IsWalk) return; // 未移动

            if (RoleData.State.IsSquat) speed += RoleData.Speed.RoleSquat; // 蹲
            if (RoleData.State.IsDown) speed += RoleData.Speed.RoleDown; // 趴

            if (RoleData.State.IsFly) speed += RoleData.Speed.RoleFly; // 飞
            if (RoleData.State.IsClimb) speed += RoleData.Speed.RoleClimb; // 爬
            if (RoleData.State.IsSwim) speed += RoleData.Speed.RoleSwim; // 游

            RoleData.Controller.Move(transform.rotation * (Quaternion.AngleAxis(angle, Vector3.up) * new Vector3(0, 0, speed)));
            RoleData.Camera.transform.position = transform.position + new Vector3(0, 1.5f, 0);
        }
    }
}