using UnityEngine;
using KILROY.Base;
using KILROY.Controller;
using KILROY.Project.Model;
using KILROY.Tool;

namespace KILROY.Project.View
{
    public class RoleThirdMove : BaseBehaviour
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
            Transform cameraT = RoleData.Camera.transform;

            // AnimatorStateInfo currentState = RoleData.Animation.Current;
            // AnimatorStateInfo nextState = RoleData.Animation.Next;
            // bool isMoveTree = currentState.IsName(RoleAnimState.MoveTree.ToString());
            // float angle = Math.Abs(FloatList["DifferenceAngleY"]);
            //
            // if (!Keyboard.Move)
            // {
            //     Animator.ResetTrigger(RoleAnimPara.Turn.ToString());
            //     return;
            // }
            //
            // if (state.IsMove && isMoveTree && ((angle >= 75 && angle <= 105) || angle >= 165))
            // {
            //     Animator.SetTrigger(RoleAnimPara.Turn.ToString());
            //     return;
            // }
            //
            // if (state.IsMove
            //     && ((isMoveTree && !nextState.IsName(string.Empty))
            //         || (!isMoveTree
            //             && !currentState.IsName(RoleAnimState.SlopeUpTree.ToString())
            //             && !currentState.IsName(RoleAnimState.SlopeDownTree.ToString())))) return;
            //
            // Animator.ResetTrigger(RoleAnimPara.Turn.ToString());
            // UIFN.RotateTween(transform, new Vector3(0, FloatList["TargetAngleY"], 0), 10);
        }
    }
}