using UnityEngine;
using KILROY.Base;
using KILROY.Model;
using KILROY.Tool;
using KILROY.Controller;
using KILROY.Project.Model;

namespace KILROY.Project.View
{
    public class RoleThirdCamera : BaseBehaviour
    {
        #region Parameter

        #endregion

        #region Cycle

        // public void Awake() { }

        // public void Start() { }

        public void Update() { UpdateDirection(); }

        #endregion

        /// <summary>
        /// 更新方向
        /// </summary>
        private void UpdateDirection()
        {
            RoleState state = RoleData.State;
            KeyboardData keyboard = InputController.Keyboard;
            Transform cameraT = RoleData.Camera.transform;

            if (state.IsIdle) return;

            RoleData.AngleY = Mathf.DeltaAngle(
                transform.localEulerAngles.y,
                cameraT.localEulerAngles.y + UIFN.GetAxiaDirection(keyboard.AxisX, keyboard.AxisY)
            );
        }
    }
}