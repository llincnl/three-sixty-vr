using UnityEngine;

namespace Viewer360 {
    public class ImageController : MonoBehaviour {
        
        [SerializeField] private Material _material;

        private Cubemap _cubemap;
        private float _rotation;
        
        public void SetCubemap(Cubemap cubemap) {
            _cubemap = cubemap;
            _material.SetTexture("_Tex", _cubemap);
        }

        public void SetRotation(float rotation) {
            _rotation = rotation;
            _material.SetFloat("_Rotation", _rotation);
        }
    }
}