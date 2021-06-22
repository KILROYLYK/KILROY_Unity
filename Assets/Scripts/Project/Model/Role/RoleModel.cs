using System.Collections.Generic;
using UnityEngine;

namespace KILROY.Project.Model
{
    #region Static

    /// <summary>
    /// 角色数据
    /// </summary>
    public static class RoleData
    {
        public static Camera Camera = null; // 相机
        public static CharacterController Controller = null; // 角色控制器
        public static Animator Animator = null; // 动画器
        public static float AngleY = 0; // 旋转角度

        public static readonly RoleState State = new RoleState(); // 状态
        public static readonly RoleRaycast Raycast = new RoleRaycast(); // 射线
        public static readonly RoleAnimation Animation = new RoleAnimation(); // 动画
        public static readonly RoleSubstitute Substitute = new RoleSubstitute(); // 替身节点
    }

    #endregion

    #region Structure

    /// <summary>
    /// 角色状态
    /// </summary>
    public class RoleState
    {
        public bool IsBorn = false; // 是否出生
        public bool IsDeath = false; // 是否死亡
        public bool IsPlot = false; // 是否在剧情

        public bool IsGrounded = false; // 是否接触地面
        public bool IsStand = false; // 是否站立
        public bool IsSquat = false; // 是否蹲
        public bool IsDown = false; // 是否趴

        public bool IsIdle = false; // 是否闲置
        public bool IsWalk = false; // 是否走
        public bool IsRun = false; // 是否跑

        public bool IsJumpUp = false; // 是否跳起
        public bool IsJumpDown = false; // 是否下落

        public bool IsFly = false; // 是否飞
        public bool IsClimb = false; // 是否爬
        public bool IsSwim = false; // 是否游

        public bool IsAttack = false; // 是否攻击
        public bool IsInjured = false; // 是否受伤

        public bool IsHillUp = false; // 是否处于上坡
        public bool IsHillDown = false; // 是否处于下坡

        public bool IsDisableOrientation = false; // 是否禁用视角
        public bool IsDisableSquat = false; // 是否禁用蹲
        public bool IsDisableDown = false; // 是否禁用趴
        public bool IsDisableWalk = false; // 是否禁用走
        public bool IsDisableRun = false; // 是否禁用跑
        public bool IsDisableJump = false; // 是否禁用跳
        public bool IsDisableFly = true; // 是否禁用飞
        public bool IsDisableClimb = true; // 是否禁用爬
        public bool IsDisableSwim = true; // 是否禁用游
        public bool IsDisableAttack = true; // 是否禁用攻击
        public bool IsDisableInjured = true; // 是否禁用受伤
    }

    /// <summary>
    /// 角色动画
    /// </summary>
    public class RoleAnimation
    {
        public AnimatorStateInfo Current; // 当前动画
        public AnimatorStateInfo Next; // 下一步动画
    }

    /// <summary>
    /// 角色射线
    /// </summary>
    public class RoleRaycast
    {
        public Dictionary<RoleRay, bool> RayList = new Dictionary<RoleRay, bool>();
        public Dictionary<RoleRay, RaycastHit> HitList = new Dictionary<RoleRay, RaycastHit>();
    }

    /// <summary>
    /// 角色替身节点
    /// </summary>
    public class RoleSubstitute
    {
        public Transform Camera = null; // 相机
    }

    #endregion

    #region Enum

    /// <summary>
    /// 人称
    /// </summary>
    public enum RolePerson
    {
        First, // 第一人称
        Third, // 第三人称
    }

    /// <summary>
    /// 角色射线
    /// </summary>
    public enum RoleRay
    {
        Ray_0, // 角色原地向下射线
        Ray_1, // 角色前方向下射线
        Ray_2, // 角色后方向下射线
    }

    /// <summary>
    /// 角色动画剪辑
    /// </summary>
    public enum RoleAnimClip
    {
        // Born,
        // Death,
        //
        // #region Idle
        //
        // IdleSM,
        // Idle,
        // IdleRandom_1,
        // IdleRandom_2,
        // IdleRandom_3,
        //
        // #endregion
        //
        // #region Move
        //
        // MoveSM,
        // MoveTree,
        // WalkStop,
        // RunStop,
        // ActiveToStand,
        // SlopeUpTree,
        // SlopeDownTree,
        // TurnWalkLeft_90,
        // TurnWalkLeft_180,
        // TurnWalkRight_90,
        // TurnWalkRight_180,
        // TurnRunLeft_90,
        // TurnRunLeft_180,
        // TurnRunRight_90,
        // TurnRunRight_180,
        //
        // #endregion
        //
        // #region Jump
        //
        // JumpSM,
        // JumpTree,
        //
        // #endregion
        //
        // #region Landing
        //
        // LandingSM,
        // LandingTree,
        //
        // #endregion
        //
        // #region Climb
        //
        // ClimbTop,
        // ClimbTopShort,
        //
        // #endregion
        //
        // #region Attack
        //
        // Combo_1,
        // Combo_2,
        // Combo_3,
        // Combo_4,
        //
        // #endregion
        //
        // #region Hurt
        //
        // HurtFront,
        // HurtFrontLeft,
        // HurtFrontRight,
        // HurtBack,
        // HurtBackLeft,
        // HurtBackRight,
        //
        // #endregion
    }

    #endregion
}