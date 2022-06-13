using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Darkisu.BillboardObject
{
    public class RenderResultToRenderTexture
    {
        public Camera TargetCamera
        {
            get;
            set;
        }

        public RenderTexture ResultBuffer
        {
            get;
            set;
        }

        public Shader RenderingShader
        {
            get;
            set;
        }

        public void Render()
        {
            var originalTarget = TargetCamera.targetTexture;
            TargetCamera.targetTexture = ResultBuffer;
            TargetCamera.RenderWithShader(RenderingShader, "Opaque");
            TargetCamera.targetTexture = originalTarget;
        }
    }
}
