using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

namespace KILROY.Project.PostProcess
{
    [Serializable]
    [PostProcess(typeof(PostProcessColorRenderer), PostProcessEvent.AfterStack, "_Custom/Color")]
    public class PostProcessColor : PostProcessEffectSettings
    {
        public ColorParameter Color = new ColorParameter { value = UnityEngine.Color.white }; // 颜色
        [Range(0f, 1)] public FloatParameter ColorScale = new FloatParameter { value = 0.5f }; // 颜色程度
        [Range(0f, 1f)] public FloatParameter GrayScale = new FloatParameter() { value = 0 }; // 灰度程度
        [Range(0f, 1f)] public FloatParameter ReverseScale = new FloatParameter() { value = 0 }; // 反色程度
    }

    public sealed class PostProcessColorRenderer : PostProcessEffectRenderer<PostProcessColor>
    {
        public override void Render(PostProcessRenderContext context)
        {
            string name = "PostProcessColor";
            CommandBuffer cmd = context.command;

            cmd.BeginSample(name);

            PropertySheet sheet = context.propertySheets.Get(Shader.Find("_Custom/PostProcessing/Color"));
            sheet.properties.SetColor("_Color", settings.Color);
            sheet.properties.SetFloat("_ColorScale", settings.ColorScale);
            sheet.properties.SetFloat("_GrayScale", settings.GrayScale);
            sheet.properties.SetFloat("_ReverseScale", settings.ReverseScale);
            context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);

            cmd.EndSample(name);
        }
    }
}