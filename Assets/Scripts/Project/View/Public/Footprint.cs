using System.Collections.Generic;
using UnityEngine;
using KILROY.Base;
using KILROY.Constant.Resource;
using KILROY.Manager;
using KILROY.Tool;
using KILROY.Project.Model;

namespace KILROY.Project.View
{
    public class Footprint : BaseBehaviour
    {
        // #region Parameter
        // private int direction = 1; // 左右脚方向
        // private Dictionary<string, bool> SwitchList = new Dictionary<string, bool>() // 开关列表
        // {
        //     {"Interval",false} // 脚印间隔
        // };
        // private Dictionary<string, float> FloatList = new Dictionary<string, float>() // 数值列表
        // {
        //     {"Interval",0.5f}, // 脚印间隔
        //     {"Delay",0.4f}, // 脚印间隔
        //     {"DelayFlag",0}, // 脚印间隔
        //     {"Gap",0.1f} // 双脚间距
        // };
        // #endregion

        // #region Cycle
        // public void Awake()
        // {
        //     Role = GetComponent<RoleController>();
        //     Particle = transform.Find("Footprint").GetComponent<ParticleSystem>();
        //     Particle.GetComponent<Renderer>().material = ResourceManager.GetMaterial(MaterialName.Footprint);
        // }

        // // public void Start() { }

        // public void Update()
        // {
        //     UpdateFootprint();
        // }

        // public void OnDestroy()
        // {
        //     KillTween();
        // }
        // #endregion

        // /// <summary>
        // /// 更新脚印
        // /// </summary>
        // public void UpdateFootprint()
        // {
        //     if (!Role.Animator.GetBool(RoleAnimationParameter.Move.ToString()))
        //     {
        //         FloatList["DelayFlag"] = 0;
        //         return;
        //     }

        //     FloatList["DelayFlag"] += Time.deltaTime;
        //     if (FloatList["DelayFlag"] < FloatList["Delay"]) return; // 延時

        //     if (SwitchList["Interval"]) return; // 间隔
        //     SwitchList["Interval"] = true;

        //     KillTween();
        //     AddTween(UIFN.Delay(() => { SwitchList["Interval"] = false; }, FloatList["Interval"]));

        //     direction *= -1;

        //     ParticleSystem.EmitParams ep = new ParticleSystem.EmitParams();
        //     ep.position = transform.position + transform.right * FloatList["Gap"] * direction + transform.up * 0.15f;
        //     ep.rotation = transform.rotation.eulerAngles.y;
        //     Particle.Emit(ep, 1);
        // }
    }
}