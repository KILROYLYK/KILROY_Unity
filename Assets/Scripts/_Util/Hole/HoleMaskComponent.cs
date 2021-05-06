using UnityEngine;
using UnityEngine.UI;
using XLua;

namespace KILROY.Util
{
    public class HoleMaskComponent : Mask
    {
        public override bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
        {
            if (!isActiveAndEnabled) return true;
            return !RectTransformUtility.RectangleContainsScreenPoint(rectTransform, sp, eventCamera);
        }
    }
}