using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace KILROY.Project.Model
{
    #region Data

    public static class RoleData
    {
        public static Camera Camera = null; // 相机
        public static CharacterController Controller = null; // 角色控制器
        public static Animator Animator = null; // 动画器

        public static RoleState State = new RoleState(); // 状态
        public static RoleSpeed Speed = new RoleSpeed(); // 速度
        public static RoleAnimation Animation = new RoleAnimation(); // 动画
        public static RoleRaycast Raycast = new RoleRaycast(); // 射线
        public static RoleSubstitute Substitute = new RoleSubstitute(); // 替身节点
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
    /// 角色速度
    /// </summary>
    public class RoleSpeed
    {
        public float CameraX = 2; // 相机垂直
        public float CameraY = 2; // 相机水平
        public float RoleWalk = 0.05f; // 角色走
        public float RoleRun = 0.08f; // 角色跑
        public float RoleSquat = -0.02f; // 角色蹲的减速
        public float RoleDown = -0.03f; // 角色趴的减速
        public float RoleFly = -0.02f; // 角色飞的减速
        public float RoleClimb = -0.02f; // 角色爬的减速
        public float RoleSwim = -0.02f; // 角色游的减速
        public float RoleJump = 8; // 角色跳跃速度
        public float RoleJumpA = 0.3f; // 角色跳跃加速度
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
    /// 角色动画参数
    /// </summary>
    public enum RoleAnimPara
    {
        // #region Life
        //
        // Born, // 出生
        // Death, // 死亡
        //
        // #endregion
        //
        // #region Orientation
        //
        // Turn, // 转向
        // AngleY, // 旋转Y轴角度
        //
        // #endregion
        //
        // #region Idle
        //
        // Idle, // 是否闲置
        // IdleTimeout, // 闲置超时
        // IdleRandom, // 随机闲置动画
        //
        // #endregion
        //
        // #region Move
        //
        // Move, // 是否移动
        // Accelerate, // 是否加速
        // SpeedXZ, // Z轴速度
        // HillUp, // 是否上坡
        // HillDown, // 是否下坡
        //
        // #endregion
        //
        // #region Jump
        //
        // Jump, // 是否跳跃
        // SpeedY, // Y轴速度
        //
        // #endregion
        //
        // #region Climb
        //
        // Climb, // 是否攀爬
        // ClimbHeight, // 攀爬高度
        //
        // #endregion
        //
        // #region Attack
        //
        // Attack, // 是否攻击
        // Combo, // 连击
        // ComboNumber, // 连击数
        //
        // #endregion
        //
        // #region Hurt
        //
        // Hurt, // 受伤
        // HurtAngleY, // 受伤Y轴角度
        //
        // #endregion
    }

    /// <summary>
    /// 角色动画状态
    /// </summary>
    public enum RoleAnimState
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