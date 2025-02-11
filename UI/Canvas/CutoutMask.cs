using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace DG_Pack.UI.Canvas {
    public class CutoutMask : Image {
        private static readonly int StencilComp = Shader.PropertyToID("_StencilComp");

        public override Material materialForRendering {
            get {
                var mat = new Material(base.materialForRendering);
                mat.SetInt(StencilComp, (int)CompareFunction.NotEqual);
                return mat;
            }
        }
    }
}