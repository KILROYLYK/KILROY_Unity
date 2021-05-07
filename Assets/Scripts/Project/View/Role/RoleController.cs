using System;
using System.Collections.Generic;
using UnityEngine;
using KILROY.Base;
using KILROY.Tool;
using KILROY.Controller;
using KILROY.Project.Model;

namespace KILROY.Project.View
{
    public class RoleController : BaseBehaviour
    {
        #region Parameter

        [Header("【控制器】")] [SerializeField] private RolePerson Person; // 人称

        #endregion

        #region Cycle

        public void Awake()
        {
            SwitchList.Add("IsSquat", false);
            SwitchList.Add("IsDown", false);

            RoleData.Camera = GameObject.Find("BoxCamera/Camera").GetComponent<Camera>();
            RoleData.Controller = GetComponent<CharacterController>();
            // RoleData.Animator = GetComponent<Animator>();
            RoleData.Substitute.Camera = transform.Find("Substitute/Camera");

            AddComponent();
            AddRaycast();
        }

        // public void Start() { }

        public void Update()
        {
            UpdateRaycast();
            UpdateState();
        }

        #endregion

        /// <summary>
        /// 添加组件
        /// </summary>
        private void AddComponent()
        {
            if (Person == RolePerson.First)
            {
                gameObject.AddComponent<Role_1_Direction>();
                gameObject.AddComponent<Role_1_Move>();
                gameObject.AddComponent<Role_1_Jump>();
            }
            else if (Person == RolePerson.Third)
            {
            }
        }

        /// <summary>
        /// 添加射线
        /// </summary>
        private void AddRaycast()
        {
            RoleData.Raycast.RayList.Add(RoleRay.Ray_0, false);
            RoleData.Raycast.HitList.Add(RoleRay.Ray_0, new RaycastHit());

            RoleData.Raycast.RayList.Add(RoleRay.Ray_1, false);
            RoleData.Raycast.HitList.Add(RoleRay.Ray_1, new RaycastHit());

            RoleData.Raycast.RayList.Add(RoleRay.Ray_2, false);
            RoleData.Raycast.HitList.Add(RoleRay.Ray_2, new RaycastHit());
        }

        #region Check

        /// <summary>
        /// 检测是否接触地面
        /// </summary>
        /// <returns>是否接触地面</returns>
        private bool CheckIsGrounded()
        {
            return RoleData.Controller.isGrounded ||
                   (RoleData.Raycast.RayList[RoleRay.Ray_0] &&
                    Math.Abs(RoleData.Raycast.HitList[RoleRay.Ray_0].point.y - transform.position.y) < 0.1f);
        }

        /// <summary>
        /// 检测是否站立
        /// </summary>
        /// <returns>是否站立</returns>
        private bool CheckIsStand()
        {
            RoleState state = RoleData.State;

            return state.IsGrounded && !(SwitchList["IsSquat"] || SwitchList["IsDown"]) &&
                   !(state.IsJumpUp || state.IsJumpDown ||
                     state.IsFly || state.IsClimb || state.IsSwim ||
                     state.IsAttack || state.IsInjured);
        }

        /// <summary>
        /// 检测是否蹲
        /// </summary>
        /// <returns>是否蹲</returns>
        private bool CheckIsSquat()
        {
            RoleState state = RoleData.State;

            if (state.IsDisableSquat) return false; // 禁用

            return state.IsGrounded && SwitchList["IsSquat"] &&
                   !(state.IsJumpUp || state.IsJumpDown ||
                     state.IsFly || state.IsClimb || state.IsSwim ||
                     state.IsAttack || state.IsInjured);
        }

        /// <summary>
        /// 检测是否趴
        /// </summary>
        /// <returns>是否趴</returns>
        private bool CheckIsDown()
        {
            RoleState state = RoleData.State;

            if (state.IsDisableDown) return false; // 禁用

            return state.IsGrounded && SwitchList["IsDown"] &&
                   !(state.IsJumpUp || state.IsJumpDown ||
                     state.IsFly || state.IsClimb || state.IsSwim ||
                     state.IsAttack || state.IsInjured);
        }

        /// <summary>
        /// 检测是否闲置
        /// </summary>
        /// <returns>是否闲置</returns>
        private bool CheckIsIdle()
        {
            RoleState state = RoleData.State;

            return state.IsGrounded && !InputController.Keyboard.Move &&
                   !(state.IsJumpUp || state.IsJumpDown ||
                     state.IsFly || state.IsClimb || state.IsSwim ||
                     state.IsAttack || state.IsInjured);
        }

        /// <summary>
        /// 检测是否走
        /// </summary>
        /// <returns>是否走</returns>
        private bool CheckIsWalk()
        {
            RoleState state = RoleData.State;

            if (state.IsDisableWalk) return false; // 禁用

            return state.IsGrounded && InputController.Keyboard.Move &&
                   !(state.IsJumpUp || state.IsJumpDown ||
                     state.IsFly || state.IsClimb || state.IsSwim ||
                     state.IsAttack || state.IsInjured);
        }

        /// <summary>
        /// 检测是否跑
        /// </summary>
        /// <returns>是否跑</returns>
        private bool CheckIsRun()
        {
            RoleState state = RoleData.State;

            if (state.IsDisableRun) return false; // 禁用

            return state.IsGrounded && (InputController.Keyboard.Move && InputController.Keyboard.ShiftLeftPress) &&
                   !(state.IsJumpUp || state.IsJumpDown ||
                     state.IsFly || state.IsClimb || state.IsSwim ||
                     state.IsAttack || state.IsInjured);
        }

        /// <summary>
        /// 检测是否跳起
        /// </summary>
        /// <returns>是否跳起</returns>
        private bool CheckIsJumpUp()
        {
            RoleState state = RoleData.State;
            bool isJumpUp = false;

            if (state.IsDisableJump) return false; // 禁用
            if (state.IsGrounded && !state.IsJumpUp && !state.IsJumpDown && InputController.Keyboard.Space) isJumpUp = true; // 即将跳起
            if (state.IsJumpUp) isJumpUp = true; // 正在跳起

            return isJumpUp && !state.IsJumpDown &&
                   !(state.IsFly || state.IsClimb || state.IsSwim ||
                     state.IsAttack || state.IsInjured);
        }

        /// <summary>
        /// 检测是否落下
        /// </summary>
        /// <returns>是否落下</returns>
        private bool CheckIsJumpDown()
        {
            RoleState state = RoleData.State;
            bool isJumpDown = false;

            if (!state.IsGrounded && !state.IsJumpUp && !state.IsJumpDown) isJumpDown = true; // 即将下落  
            if (state.IsJumpDown) isJumpDown = true; // 正在下落

            return isJumpDown &&
                   !(state.IsFly || state.IsClimb || state.IsSwim ||
                     state.IsAttack || state.IsInjured);
        }

        /// <summary>
        /// 检测是否飞行
        /// </summary>
        /// <returns>是否飞行</returns>
        private bool CheckIsFly()
        {
            RoleState state = RoleData.State;

            bool isFly = false;

            if (state.IsDisableFly) return false; // 禁用
            if (!state.IsFly && (state.IsJumpUp || state.IsJumpDown) &&
                !RoleData.Raycast.RayList[RoleRay.Ray_0] && InputController.Keyboard.Space) isFly = true;
            if (state.IsFly) isFly = true;
            if (state.IsFly && state.IsGrounded) isFly = false;

            return isFly &&
                   !(state.IsClimb || state.IsSwim ||
                     state.IsAttack || state.IsInjured);
        }

        /// <summary>
        /// 检测是否攀爬
        /// </summary>
        /// <returns>是否攀爬</returns>
        private bool CheckIsClimb()
        {
            RoleState state = RoleData.State;
            bool isClimb = false;

            if (state.IsDisableClimb) return false; // 禁用
            if (state.IsClimb) isClimb = true;

            return isClimb &&
                   !(state.IsJumpUp || state.IsJumpDown ||
                     state.IsFly || state.IsSwim ||
                     state.IsAttack || state.IsInjured);
        }

        /// <summary>
        /// 检测是否游泳
        /// </summary>
        /// <returns>是否游泳</returns>
        private bool CheckIsSwim()
        {
            RoleState state = RoleData.State;
            bool isSwim = false;

            if (state.IsDisableSwim) return false; // 禁用
            if (state.IsSwim) isSwim = true;

            return isSwim &&
                   !(state.IsJumpUp || state.IsJumpDown ||
                     state.IsFly || state.IsClimb ||
                     state.IsAttack || state.IsInjured);
        }

        /// <summary>
        /// 检测是否攻击
        /// </summary>
        /// <returns>是否攻击</returns>
        private bool CheckIsAttack()
        {
            RoleState state = RoleData.State;
            bool isAttack = false;

            if (state.IsDisableAttack) return false; // 禁用
            if (state.IsAttack) isAttack = true;

            return isAttack &&
                   !(state.IsClimb || state.IsSwim ||
                     state.IsInjured);
        }

        /// <summary>
        /// 检测是否受伤
        /// </summary>
        /// <returns>是否受伤</returns>
        private bool CheckIsInjured()
        {
            RoleState state = RoleData.State;
            bool isInjured = false;

            if (state.IsDisableInjured) return false; // 禁用
            if (state.IsInjured) isInjured = true;

            return isInjured;
        }

        /// <summary>
        /// 检测是否处于斜坡
        /// </summary>
        /// <returns>处于斜坡</returns>
        private void CheckIsHill()
        {
            RoleState state = RoleData.State;
            Dictionary<RoleRay, RaycastHit> hitList = RoleData.Raycast.HitList;
            Vector3 hit_0 = hitList[RoleRay.Ray_0].point;
            Vector3 hit_1 = hitList[RoleRay.Ray_1].point;
            float sideB = hit_0.y - hit_1.y;
            float angle = sideB != 0 ? UIFN.RightTriangleGetAngleB(0.5f, sideB) : 0;

            if (!state.IsGrounded)
            {
                state.IsHillUp = false;
                state.IsHillDown = false;
                return;
            }

            if ((state.IsWalk || state.IsRun) && angle >= 35 && angle <= 55)
            {
                bool isUp = hit_0.y < hit_1.y;
                state.IsHillUp = isUp;
                state.IsHillDown = !isUp;
            }
        }

        #endregion

        /// <summary>
        /// 更新射线
        /// </summary>
        private void UpdateRaycast()
        {
            Dictionary<RoleRay, bool> rayList = RoleData.Raycast.RayList;
            Dictionary<RoleRay, RaycastHit> hitList = RoleData.Raycast.HitList;
            Vector3 position = transform.position;
            Quaternion rotation = transform.rotation;

            // 射线0
            RaycastHit hit_0 = RoleData.Raycast.HitList[RoleRay.Ray_0];
            rayList[RoleRay.Ray_0] = UIFN.Raycast(position + rotation * new Vector3(0, 0.5f, 0),
                                                  rotation * Vector3.down,
                                                  2f, Color.black, true, out hit_0);
            hitList[RoleRay.Ray_0] = hit_0;

            // 射线1
            RaycastHit hit_1 = RoleData.Raycast.HitList[RoleRay.Ray_1];
            rayList[RoleRay.Ray_1] = UIFN.Raycast(position + rotation * new Vector3(0, 1f, 0.5f),
                                                  rotation * Vector3.down,
                                                  4f, Color.black, true, out hit_1);
            hitList[RoleRay.Ray_1] = hit_1;

            // 射线2
            RaycastHit hit_2 = RoleData.Raycast.HitList[RoleRay.Ray_2];
            rayList[RoleRay.Ray_2] = UIFN.Raycast(position + rotation * new Vector3(0, 1f, -0.5f),
                                                  rotation * Vector3.down,
                                                  4f, Color.black, true, out hit_2);
            hitList[RoleRay.Ray_2] = hit_2;
        }

        private void UpdateState()
        {
            RoleState state = RoleData.State;

            // 切换状态
            if ((state.IsStand || state.IsSquat || state.IsDown) && InputController.Keyboard.ControlLeft)
            {
                if (!SwitchList["IsSquat"] && !SwitchList["IsDown"]) // 蹲
                {
                    SwitchList["IsSquat"] = true;
                    SwitchList["IsDown"] = false;
                }
                else if (SwitchList["IsSquat"] && !SwitchList["IsDown"]) // 趴
                {
                    SwitchList["IsSquat"] = false;
                    SwitchList["IsDown"] = true;
                }
                else // 站
                {
                    SwitchList["IsSquat"] = false;
                    SwitchList["IsDown"] = false;
                }
            }

            state.IsGrounded = CheckIsGrounded();
            state.IsStand = CheckIsStand();
            state.IsSquat = CheckIsSquat();
            state.IsDown = CheckIsDown();
            state.IsIdle = CheckIsIdle();
            state.IsWalk = CheckIsWalk();
            state.IsRun = CheckIsRun();
            state.IsJumpUp = CheckIsJumpUp();
            state.IsJumpDown = CheckIsJumpDown();
            state.IsFly = CheckIsFly();
            state.IsClimb = CheckIsClimb();
            state.IsSwim = CheckIsSwim();
            state.IsAttack = CheckIsAttack();
            state.IsInjured = CheckIsInjured();
            CheckIsHill();

            // 特殊处理
            if (state.IsBorn || state.IsDeath || state.IsPlot)
            {
                state.IsStand = state.IsIdle = true;
                state.IsSquat = state.IsDown = state.IsWalk = state.IsRun
                    = state.IsJumpUp = state.IsJumpDown = state.IsFly = state.IsClimb = state.IsSwim
                        = state.IsAttack = state.IsInjured
                            = false;
            }
        }
    }
}