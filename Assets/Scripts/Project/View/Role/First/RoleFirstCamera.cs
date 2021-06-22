using UnityEngine;
using KILROY.Base;
using KILROY.Controller;
using KILROY.Project.Model;
using KILROY.Tool;

namespace KILROY.Project.View
{
    public class RoleFirstCamera : BaseBehaviour
    {
        #region Parameter

        #endregion

        #region Cycle

        public void Awake()
        {
            SwitchList.Add("ReverseX", true); // 反转X轴
            SwitchList.Add("ReverseY", false); // 反转Y轴

            FloatList.Add("SpeedHorizontal", 20000); // 横向速度
            FloatList.Add("SpeedVertical", 20000); // 纵向
            FloatList.Add("SpeedFollow", 50); // 跟随速度
        }

        // public void Start() { }

        public void Update()
        {
            UpdateDirection();
            UpdatePosition();
        }

        #endregion

        /// <summary>
        /// 更新方向
        /// </summary>
        private void UpdateDirection()
        {
            Transform cameraT = RoleData.Camera.transform;
            float x = (SwitchList["ReverseX"] ? -1 : 1) * InputController.Mouse.AxisY;
            float y = (SwitchList["ReverseY"] ? -1 : 1) * InputController.Mouse.AxisX;
            Vector3 rotationCamera = cameraT.rotation.eulerAngles + new Vector3(x, y, 0);
            Vector3 rotationRole = transform.rotation.eulerAngles + new Vector3(0, y, 0);

            if (RoleData.State.IsDisableOrientation) return; // 禁用

            if (x == 0 && y == 0) return;
            if (rotationCamera.x >= 85 && rotationCamera.x <= 90) rotationCamera.x = 85;
            if (rotationCamera.x >= 270 && rotationCamera.x <= 285) rotationCamera.x = 285;

            cameraT.rotation = Quaternion.Slerp(cameraT.rotation, Quaternion.Euler(rotationCamera), FloatList["SpeedHorizontal"] * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(rotationRole), FloatList["SpeedVertical"] * Time.deltaTime);
        }

        /// <summary>
        /// 更新位置
        /// </summary>
        private void UpdatePosition() { UIFN.MoveTween(RoleData.Camera.transform, RoleData.Substitute.Camera.position, FloatList["SpeedFollow"]); }
    }
}