using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XLua;
using DG.Tweening;
using KILROY.Constant.Enum;
using KILROY.Constant.Resource;
using KILROY.Manager;

namespace KILROY.Tool
{
    [Hotfix]
    public static class UIFN
    {
        /// <summary>
        /// 延时器
        /// </summary>
        /// <param name="callback">回调</param>
        /// <param name="time">延时</param>
        public static Sequence Delay(Action callback, float time)
        {
            Sequence delay = DOTween.Sequence();
            delay.PrependInterval(time);
            delay.onComplete = () => { callback(); };
            return delay;
        }

        /// <summary>
        /// 设置矩形尺寸
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="size">尺寸</param>
        public static void SetRectSize(RectTransform rect, Vector2 size)
        {
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
        }

        /// <summary>
        /// 设置矩形位置
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="position">位置</param>
        /// <param name="isAnchor">是否换算锚点</param>
        public static void SetRectPosition(RectTransform rect, Vector2 position, bool isAnchor = false)
        {
            if (isAnchor)
            {
                Vector2 pivot = rect.pivot;
                position += new Vector2((float)(0.5 - pivot.x) * rect.rect.width, (float)(0.5 - pivot.y) * rect.rect.height);
            }

            rect.localPosition = position;
        }

        /// <summary>
        /// 获取向量方向
        /// </summary>
        /// <param name="axiaX">水平向量</param>
        /// <param name="axiaY">垂直向量</param>
        /// <returns>方向角度</returns>
        public static int GetAxiaDirection(float axiaX, float axiaY)
        {
            int angle = 0;

            if (axiaX == 0 && axiaY == 0) return angle;

            #if KILROY_PC
            if (axiaX == 0 && axiaY > 0) angle = 0; // 前
            if (axiaX == 0 && axiaY < 0) angle = 180; // 后
            if (axiaX > 0 && axiaY == 0) angle = 90; // 右
            if (axiaX < 0 && axiaY == 0) angle = -90; // 左
            if (axiaY > 0 && axiaX > 0) angle = 45; // 前右
            if (axiaY > 0 && axiaX < 0) angle = -45; // 前左
            if (axiaY < 0 && axiaX > 0) angle = 135; // 后右
            if (axiaY < 0 && axiaX < 0) angle = -135; // 后左
            #endif

            #if KILROY_PHONE
            angle = (int)(Math.Atan2(axiaX, axiaY) * 180 / Math.PI);
            #endif

            return angle;
        }

        /// <summary>
        /// 显示动画控制器按钮
        /// </summary>
        /// <param name="animator">动画控制器</param>
        /// <param name="canvas">显示的Canvas</param>
        public static void ShowAnimatorButton(Animator animator, Transform canvas)
        {
            GameObject prefab = ResourceManager.GetPrefab(PrefabName.Button);

            for (int i = 0, n = animator.runtimeAnimatorController.animationClips.Length; i < n; i++)
            {
                AnimationClip clip = animator.runtimeAnimatorController.animationClips[i];
                GameObject button = UnityEngine.Object.Instantiate(prefab);
                string animationName = clip.name.Substring(0, 1).ToUpper() + clip.name.Substring(1);
                string buttonName = "Button" + animationName;

                button.name = buttonName;
                button.transform.SetParent(canvas, false);
                button.transform.Find("Text").GetComponent<Text>().text = buttonName;
                button.GetComponent<RectTransform>().anchoredPosition = new Vector2(100, -40 - i * 60);
                button.GetComponent<Button>().onClick.AddListener(() => { animator.Play(animationName); });
            }
        }

        /// <summary>
        /// 直角三角形获取B角度
        /// </summary>
        /// <param name="sideA">直角边A</param>
        /// <param name="sideB">直角边B</param>
        /// <returns>B角角度</returns>
        public static float RightTriangleGetAngleB(float sideA, float sideB)
        {
            float sideC = Mathf.Sqrt(Mathf.Pow(sideA, 2) + Mathf.Pow(sideB, 2));
            return Mathf.Asin(sideA / sideC) / Mathf.PI * 180;
        }

        #region 修改向量的其中一个值

        /// <summary>
        /// 重载-修改向量的其中一个值
        /// </summary>
        /// <param name="vector">向量</param>
        /// <param name="axis">轴</param>
        /// <param name="value">值</param>
        /// <returns>向量</returns>
        public static Vector2 SetVector(Vector2 vector, VectorAxis axis, float value)
        {
            switch (axis)
            {
                case VectorAxis.X:
                    vector.x = value;
                    break;
                case VectorAxis.Y:
                    vector.y = value;
                    break;
            }

            return vector;
        }

        /// <summary>
        /// 重载-修改向量的其中一个值
        /// </summary>
        /// <param name="vector">向量</param>
        /// <param name="axis">轴</param>
        /// <param name="value">值</param>
        /// <returns>向量</returns>
        public static Vector3 SetVector(Vector3 vector, VectorAxis axis, float value)
        {
            switch (axis)
            {
                case VectorAxis.X:
                    vector.x = value;
                    break;
                case VectorAxis.Y:
                    vector.y = value;
                    break;
                case VectorAxis.Z:
                    vector.z = value;
                    break;
            }

            return vector;
        }

        /// <summary>
        /// 重载-修改向量的其中一个值
        /// </summary>
        /// <param name="vector">向量</param>
        /// <param name="axis">轴</param>
        /// <param name="value">值</param>
        /// <returns>向量</returns>
        public static Vector4 SetVector(Vector4 vector, VectorAxis axis, float value)
        {
            switch (axis)
            {
                case VectorAxis.X:
                    vector.x = value;
                    break;
                case VectorAxis.Y:
                    vector.y = value;
                    break;
                case VectorAxis.Z:
                    vector.z = value;
                    break;
                case VectorAxis.W:
                    vector.w = value;
                    break;
            }

            return vector;
        }

        #endregion

        #region 移动

        /// <summary>
        /// 向前移动
        /// </summary>
        /// <param name="target">目标</param>
        /// <param name="Speed">速度</param>
        public static void MoveForward(Transform target, float speed) { target.Translate(target.forward * speed * Time.deltaTime, Space.World); }

        /// <summary>
        /// 向上移动
        /// </summary>
        /// <param name="target">目标</param>
        /// <param name="Speed">速度</param>
        public static void MoveUp(Transform target, float speed) { target.Translate(target.up * speed * Time.deltaTime, Space.World); }

        /// <summary>
        /// 缓冲移动
        /// </summary>
        /// <param name="target">目标</param>
        /// <param name="position">位置</param>
        /// <param name="speed">速度</param>
        public static void MoveTween(Transform target, Vector3 position, float speed) { target.position = Vector3.Lerp(target.position, position, Mathf.Clamp01(speed * Time.deltaTime)); }

        #endregion

        #region 旋转

        /// <summary>
        /// 缓冲旋转
        /// </summary>
        /// <param name="target">目标</param>
        /// <param name="rotation">旋转</param>
        /// <param name="speed">速度</param>
        public static void RotateTween(Transform target, Vector3 rotation, float speed) { target.rotation = Quaternion.Slerp(target.rotation, Quaternion.Euler(rotation), Mathf.Clamp01(speed * Time.deltaTime)); }

        #endregion

        #region 射线

        /// <summary>
        /// 绘制射线（只在场景面板可见）
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="endPosition">结束位置</param>
        /// <param name="color">颜色</param>
        public static void DrawRay(Vector3 startPosition, Vector3 endPosition, Color color)
        {
            #if UNITY_EDITOR
            Debug.DrawRay(startPosition, endPosition, color);
            #endif
        }

        /// <summary>
        /// 重载-发出射线
        /// </summary>
        /// <param name="position">位置</param>
        /// <param name="direction">方向</param>
        /// <param name="distance">距离</param>
        /// <param name="color">颜色</param>
        /// <param name="isShow">是否显示</param>
        /// <returns>是否碰撞</returns>
        public static bool Raycast(Vector3 position, Vector3 direction, float distance, Color color, bool isShow)
        {
            if (isShow) DrawRay(position, direction * distance, color);
            return Physics.Raycast(position, direction, distance);
        }

        /// <summary>
        /// 重载-发出射线
        /// </summary>
        /// <param name="position">位置</param>
        /// <param name="direction">方向</param>
        /// <param name="distance">距离</param>
        /// <param name="color">颜色</param>
        /// <param name="isShow">是否显示</param>
        /// <param name="hit">碰撞信息</param>
        /// <returns>是否碰撞</returns>
        public static bool Raycast(Vector3 position, Vector3 direction, float distance, Color color, bool isShow, out RaycastHit hit)
        {
            if (isShow) DrawRay(position, direction * distance, color);
            return Physics.Raycast(new Ray(position, direction), out hit, distance);
        }

        #endregion

        #region 渲染纹理

        /// <summary>
        /// 纹理-2D转渲染
        /// </summary>
        /// <param name="texture">2D纹理</param>
        /// <returns>渲染纹理</returns>
        public static RenderTexture Texture2DToRender(Texture2D texture2D)
        {
            RenderTexture textureRender = new RenderTexture(texture2D.width, texture2D.height, 0);

            RenderTexture.active = textureRender;
            Graphics.Blit(texture2D, textureRender);

            return textureRender;
        }

        /// <summary>
        /// 纹理-渲染转2D
        /// </summary>
        /// <param name="texture">2D纹理</param>
        /// <returns>渲染纹理</returns>
        public static Texture2D TextureRenderTo2D(RenderTexture textureRender)
        {
            int width = textureRender.width;
            int height = textureRender.height;
            Texture2D texture2D = new Texture2D(width, height, TextureFormat.ARGB32, false);

            RenderTexture.active = textureRender;
            texture2D.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            texture2D.Apply();

            return texture2D;
        }

        #endregion

        #region 获取抛物线

        /// <summary>
        /// 重载-抛物线
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="endPosition">结束位置</param>
        /// <param name="height">高度</param>
        /// <param name="time">当前时间</param>
        /// <returns>抛物线位置</returns>
        public static Vector2 GetParabola(Vector2 startPosition, Vector2 endPosition, float height, float time)
        {
            Vector2 mid = Vector2.Lerp(startPosition, endPosition, time);
            float Func(float x) => 4 * (-height * x * x + height * x);
            return new Vector2(mid.x, Func(time) + Mathf.Lerp(startPosition.y, endPosition.y, time));
        }

        /// <summary>
        /// 重载-抛物线
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="endPosition">结束位置</param>
        /// <param name="height">高度</param>
        /// <param name="time">当前时间</param>
        /// <returns>抛物线位置</returns>
        public static Vector3 GetParabola(Vector3 startPosition, Vector3 endPosition, float height, float time)
        {
            Vector3 mid = Vector3.Lerp(startPosition, endPosition, time);
            float Func(float x) => 4 * (-height * x * x + height * x);
            return new Vector3(mid.x, Func(time) + Mathf.Lerp(startPosition.y, endPosition.y, time), mid.z);
        }

        #endregion

        #region 获取皮肤材质属性

        /// <summary>
        /// 重载-获取皮肤材质属性
        /// </summary>
        /// <param name="skinMesh">皮肤材质</param>
        /// <returns>皮肤材质属性</returns>
        public static MaterialPropertyBlock GetMaterialProperty(SkinnedMeshRenderer skinMesh)
        {
            MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
            skinMesh.GetPropertyBlock(propertyBlock);
            return propertyBlock;
        }

        /// <summary>
        /// 重载-获取皮肤材质属性
        /// </summary>
        /// <param name="skinMesh">皮肤材质</param>
        /// <returns>皮肤材质属性</returns>
        public static List<MaterialPropertyBlock> GetMaterialProperty(SkinnedMeshRenderer[] skinMesh)
        {
            List<MaterialPropertyBlock> propertyList = new List<MaterialPropertyBlock>();
            foreach (SkinnedMeshRenderer item in skinMesh)
            {
                MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
                propertyList.Add(propertyBlock);
                item.GetPropertyBlock(propertyBlock);
            }

            return propertyList;
        }

        #endregion

        #region 设置皮肤材质属性

        /// <summary>
        /// 重载-设置皮肤无材质属性
        /// </summary>
        /// <param name="skinMesh">皮肤材质</param>
        /// <param name="name">属性名称</param>
        /// <param name="value">值</param>
        /// <param name="property">属性</param>
        public static void SetMaterialProperty(SkinnedMeshRenderer skinMesh, string name, float value, MaterialPropertyBlock property = null)
        {
            if (property == null) property = GetMaterialProperty(skinMesh);

            property.SetFloat(name, value);
            skinMesh.SetPropertyBlock(property);
        }

        /// <summary>
        /// 重载-设置皮肤无材质属性
        /// </summary>
        /// <param name="skinMesh">皮肤材质</param>
        /// <param name="name">属性名称</param>
        /// <param name="value">值</param>
        /// <param name="property">属性</param>
        public static void SetMaterialProperty(SkinnedMeshRenderer[] skinMesh, string name, float value, List<MaterialPropertyBlock> property = null)
        {
            if (property == null) property = GetMaterialProperty(skinMesh);

            for (int i = 0, n = skinMesh.Length; i < n; i++)
            {
                MaterialPropertyBlock propertyBlock = property[i];
                propertyBlock.SetFloat(name, value);
                skinMesh[i].SetPropertyBlock(propertyBlock);
            }
        }

        #endregion

        #region 获取皮肤材质

        /// <summary>
        /// 重载-获取皮肤材质
        /// </summary>
        /// <param name="skinMesh">皮肤网格</param>
        /// <returns>材质列表</returns>
        public static Material[] GetMaterial(SkinnedMeshRenderer skinMesh) { return skinMesh.materials; }

        /// <summary>
        /// 重载-获取皮肤材质
        /// </summary>
        /// <param name="skinMesh">皮肤网格</param>
        /// <returns>材质列表</returns>
        public static Material[] GetMaterial(SkinnedMeshRenderer[] skinMesh)
        {
            List<Material> list = new List<Material>();
            foreach (SkinnedMeshRenderer item in skinMesh) list.AddRange(item.materials);
            return list.ToArray();
        }

        #endregion

        #region 设置皮肤材质

        /// <summary>
        /// 重载-设置皮肤材质
        /// </summary>
        /// <param name="skinMesh">皮肤网格</param>
        /// <param name="material">材质</param>
        public static void SetMaterial(SkinnedMeshRenderer skinMesh, Material material) { skinMesh.material = material; }

        /// <summary>
        /// 重载-设置皮肤材质
        /// </summary>
        /// <param name="skinMesh">皮肤网格</param>
        /// <param name="material">材质</param>
        public static void SetMaterial(SkinnedMeshRenderer skinMesh, Material[] material) { skinMesh.materials = material; }

        /// <summary>
        /// 重载-设置皮肤材质
        /// </summary>
        /// <param name="skinMesh">皮肤网格</param>
        /// <param name="material">材质</param>
        public static void SetMaterial(SkinnedMeshRenderer[] skinMesh, Material[] material)
        {
            int i = 0;
            foreach (SkinnedMeshRenderer item in skinMesh)
            {
                int length = item.materials.Length;
                item.materials = material.Skip(i).Take(length).ToArray();
                i += length;
            }
        }

        #endregion
    }
}