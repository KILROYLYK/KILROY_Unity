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

        public void Awake()
        {
            FloatList.Add("SpeedWalk", 0.05f); // 走
            FloatList.Add("SpeedRun", 0.08f); // 跑
            FloatList.Add("SpeedSquat", -0.02f); // 蹲的减速
            FloatList.Add("SpeedDown", -0.03f); // 趴的减速
            FloatList.Add("SpeedFly", -0.02f); // 飞的减速
            FloatList.Add("SpeedClimb", -0.02f); // 爬的减速
            FloatList.Add("SpeedSwim", -0.02f); // 游的减速
        }

        // public void Start() { }

        public void Update() { UpdateMove(); }

        #endregion

        /// <summary>
        /// 更新移动
        /// </summary>
        private void UpdateMove()
        {
            RoleState state = RoleData.State;
            float s = !state.IsRun ? FloatList["SpeedWalk"] : FloatList["SpeedRun"];
            float x = InputController.Keyboard.AxisX;
            float y = InputController.Keyboard.AxisY;
            int angle = UIFN.GetAxiaDirection(x, y);

            if (!state.IsWalk) return; // 未移动

            if (state.IsSquat) s += FloatList["SpeedSquat"]; // 蹲
            if (state.IsDown) s += FloatList["SpeedDown"]; // 趴

            if (state.IsFly) s += FloatList["SpeedFly"]; // 飞
            if (state.IsClimb) s += FloatList["SpeedClimb"]; // 爬
            if (state.IsSwim) s += FloatList["SpeedSwim"]; // 游

            RoleData.Controller.Move(transform.rotation * (Quaternion.AngleAxis(angle, Vector3.up) * new Vector3(0, 0, s)));
            RoleData.Camera.transform.position = transform.position + new Vector3(0, 1.5f, 0);
        }
    }
}