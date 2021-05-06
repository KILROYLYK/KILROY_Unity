using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using XLua;
using KILROY.Util;

namespace KILROY.Manager
{
    [Hotfix]
    public static class MenuManager
    {
        #if UNITY_EDITOR
        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="command">命令</param>
        /// <returns>菜单对象</returns>
        private static GameObject CreateMenu(MenuCommand command)
        {
            GameObject obj = new GameObject(); // 创建新物体
            GameObjectUtility.SetParentAndAlign(obj, command.context as GameObject); // 设置父节点为当前选中物体
            Undo.RegisterCreatedObjectUndo(obj, "Create" + obj.name); // 注册到Undo系统,允许撤销
            Selection.activeObject = obj; // 将新建物体设为当前选中物体
            return obj;
        }

        /// <summary>
        /// 点击
        /// </summary>
        /// <param name="command">命令</param>
        [MenuItem("GameObject/UI/_Custom/_Click", false, 0)]
        public static void _Click(MenuCommand command)
        {
            GameObject click = CreateMenu(command);
            click.name = "_Click";
            click.AddComponent<Image>();
            click.AddComponent<ClickComponent>();
        }

        /// <summary>
        /// 拖拽
        /// </summary>
        /// <param name="command">命令</param>
        [MenuItem("GameObject/UI/_Custom/_Drag", false, 0)]
        public static void _Drag(MenuCommand command)
        {
            GameObject drag = CreateMenu(command);
            drag.name = "_Drag";
            drag.AddComponent<Image>();
            drag.AddComponent<DragComponent>();
        }

        /// <summary>
        /// 挖孔
        /// </summary>
        /// <param name="command">命令</param>
        [MenuItem("GameObject/UI/_Custom/_Hole", false, 0)]
        public static void _Hole(MenuCommand command)
        {
            // 挖孔
            GameObject hole = CreateMenu(command);
            hole.name = "_Hole";
            hole.transform.localPosition = Vector3.zero;

            HoleImageComponent holeImage = hole.AddComponent<HoleImageComponent>();
            holeImage.color = new Color(0, 0, 0, 1);

            HoleMaskComponent holeMask = hole.AddComponent<HoleMaskComponent>();
            holeMask.showMaskGraphic = false;

            // 幕布
            GameObject mask = new GameObject("_Mask");
            mask.transform.SetParent(hole.transform);
            mask.transform.localPosition = Vector3.zero;

            HoleImageComponent maskImage = mask.AddComponent<HoleImageComponent>();
            maskImage.color = new Color(0, 0, 0, 0.8f);

            mask.AddComponent<Button>();
        }
        #endif
    }
}