using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using KILROY.Base;
using KILROY.Tool;
using KILROY.Project.Model;

namespace KILROY.Project.View
{
    public class ArmsFollow : BaseBehaviour
    {
        // #region Parameter
        //
        // [Header("【角色信息】")] [SerializeField] private Transform Role; // 角色
        // [SerializeField] private Transform Hand; // 手
        //
        // [Header("【武器信息】")] [SerializeField] private SkinnedMeshRenderer[] SkinMesh; // 皮肤网格
        //
        // private GameObject ParticleDust = null; // 粒子灰尘
        // private Transform BoxLight = null; // 灯光盒子
        // private List<MaterialPropertyBlock> MaterialList = null; // 材质参数列表
        //
        // private Dictionary<string, bool> SwitchList = new Dictionary<string, bool>() // 开关列表
        // {
        //     { "IsSwitchHand", false }, // 是否切换在手
        //     { "IsToHand", false }, // 是否即将在手
        //     { "IsInHand", false }, // 是否在手
        //     { "IsShow", false } // 是否显示
        // };
        //
        // private Dictionary<string, float> FloatList = new Dictionary<string, float>() // 数值列表
        // {
        //     { "FadeTime", 1 }, // 显隐时间
        //     { "FadeTimeFast", 0.2f }, // 快速显隐时间
        //     { "FollowSpeed", 12 }, // 跟随速度
        //     { "KeepHand", 4 }, // 保持武器在手时间
        //     { "KeepHandFlag", 0 }, // 累积保持武器在手时间
        //     { "KeepStorage", 5 }, // 保持武器收纳时间
        //     { "KeepStorageFlag", 0 }, // 累积保持武器收纳时间
        // };
        //
        // private const string TweenId = "ArmsFade"; // 动效标识-武器显隐
        //
        // #endregion
        //
        // #region Cycle
        //
        // public void Awake()
        // {
        //     ParticleDust = transform.Find("ParticleDust").gameObject;
        //     BoxLight = transform.Find("BoxLight");
        //     MaterialList = UIFN.GetMaterialProperty(SkinMesh);
        //
        //     UIFN.SetMaterialProperty(SkinMesh, "_DissolveScale", 1);
        // }
        //
        // // public void Start() { }
        //
        // public void Update() { UpdateFollow(); }
        //
        // public void OnDestroy() { KillTween(); }
        //
        // #endregion
        //
        // /// <summary>
        // /// 显示武器
        // /// </summary>
        // private void ShowArms()
        // {
        //     float duration = SwitchList["IsToHand"] ? FloatList["FadeTimeFast"] : FloatList["FadeTime"];
        //
        //     SwitchList["IsShow"] = true;
        //
        //     KillTween(TweenId);
        //     Sequence tween = DOTween.Sequence();
        //     if (!SwitchList["IsToHand"])
        //     {
        //         tween.AppendCallback(() => { ParticleDust.SetActive(true); });
        //         tween.Append(DOTween.To(setter: value => { UIFN.SetMaterialProperty(SkinMesh, "_DissolveScale", value, MaterialList); }, startValue: 0, endValue: 1, duration: duration).SetEase(Ease.Linear));
        //         foreach (Transform item in BoxLight) tween.Join(item.GetComponent<Light>().DOIntensity(0, duration).SetEase(Ease.Linear));
        //     }
        //
        //     tween.AppendCallback(() => { SwitchList["IsInHand"] = SwitchList["IsToHand"]; });
        //     tween.Append(DOTween.To(setter: value => { UIFN.SetMaterialProperty(SkinMesh, "_DissolveScale", value, MaterialList); }, startValue: 1, endValue: 0, duration: duration).SetEase(Ease.Linear));
        //     foreach (Transform item in BoxLight) tween.Join(item.GetComponent<Light>().DOIntensity(2, duration).SetEase(Ease.Linear));
        //     AddTween(TweenId, tween);
        // }
        //
        // /// <summary>
        // /// 隐藏武器
        // /// </summary>
        // private void HideArms()
        // {
        //     SwitchList["IsShow"] = false;
        //
        //     KillTween(TweenId);
        //     Sequence tween = DOTween.Sequence();
        //     tween.Append(DOTween.To(setter: value => { UIFN.SetMaterialProperty(SkinMesh, "_DissolveScale", value, MaterialList); }, startValue: 0, endValue: 1, duration: FloatList["FadeTime"]).SetEase(Ease.Linear));
        //     foreach (Transform item in BoxLight) tween.Join(item.GetComponent<Light>().DOIntensity(0, FloatList["FadeTime"]).SetEase(Ease.Linear));
        //     AddTween(TweenId, tween);
        // }
        //
        // /// <summary>
        // /// 更新跟随
        // /// </summary>
        // private void UpdateFollow()
        // {
        //     if (RoleData.Animation.Current.IsName(RoleAnimState.Death.ToString()))
        //     {
        //         SwitchList["IsSwitchHand"] = false;
        //         SwitchList["IsToHand"] = false;
        //         SwitchList["IsInHand"] = false;
        //         if (SwitchList["IsShow"]) HideArms();
        //         return;
        //     }
        //
        //     if (RoleData.State.IsAttack)
        //     {
        //         SwitchList["IsSwitchHand"] = true;
        //         FloatList["KeepHandFlag"] = 0;
        //         FloatList["KeepStorageFlag"] = 0;
        //     }
        //
        //     if (FloatList["KeepHandFlag"] < FloatList["KeepHand"]) FloatList["KeepHandFlag"] += Time.deltaTime;
        //     if (FloatList["KeepHandFlag"] >= FloatList["KeepHand"]
        //         || (!RoleData.State.IsAttack && SwitchList["IsSwitchHand"] && (RoleData.Animation.Current.IsName(RoleAnimState.MoveTree.ToString()) || RoleData.Animation.Current.IsName(RoleAnimState.JumpTree.ToString())))) SwitchList["IsSwitchHand"] = false;
        //
        //     if ((SwitchList["IsSwitchHand"] && !SwitchList["IsToHand"])
        //         || (!SwitchList["IsSwitchHand"] && SwitchList["IsToHand"]))
        //     {
        //         SwitchList["IsToHand"] = SwitchList["IsSwitchHand"];
        //         ShowArms();
        //     }
        //
        //     if (FloatList["KeepStorageFlag"] >= FloatList["KeepStorage"] && SwitchList["IsShow"]) HideArms();
        //
        //     if (SwitchList["IsInHand"])
        //     {
        //         transform.position = Hand.position;
        //         transform.rotation = Quaternion.Euler(Hand.rotation.eulerAngles);
        //     }
        //     else
        //     {
        //         if (FloatList["KeepStorageFlag"] < FloatList["KeepStorage"]) FloatList["KeepStorageFlag"] += Time.deltaTime;
        //         UIFN.MoveTween(transform, Role.position + Role.rotation * new Vector3(0.15f, 0.75f, -0.35f), FloatList["FollowSpeed"]);
        //         transform.rotation = Quaternion.Euler(new Vector3(-60, Role.localEulerAngles.y - 90, 90));
        //     }
        // }
    }
}