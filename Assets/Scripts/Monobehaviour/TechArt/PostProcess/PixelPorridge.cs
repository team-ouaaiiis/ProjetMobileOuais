using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(OutlinePostProcessRenderer), PostProcessEvent.AfterStack, "Custom/OutlinePostProcess")]
public sealed class OutlinePostProcess : PostProcessEffectSettings
{
    [Range(0f, 20f), Tooltip("Width")]
    public FloatParameter width = new FloatParameter { value = 1f };
}

public sealed class OutlinePostProcessRenderer : PostProcessEffectRenderer<OutlinePostProcess>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/OutlinePostProcess"));
        sheet.properties.SetFloat("_Width", settings.width);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}