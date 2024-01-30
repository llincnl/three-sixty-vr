using Interfaces;
using UnityEngine;

namespace Viewer360 {
    public class ImageController : MonoBehaviour, IVRController {
        
        [SerializeField] private Material _material;

        private Cubemap _cubemap;
        private float _rotation;
        
        public void Initialize() {
            Debug.Log("Image Controller initialized");
        }

        public void UpdateController(Material material, Cubemap cubemap, float rotation) {
            SetMaterial(material);
            SetCubemap(cubemap);
            SetRotation(rotation);
        }

        public void SetCubemap(Cubemap cubemap) {
            _cubemap = cubemap;
            _material.SetTexture("_Tex", _cubemap);
        }

        public void SetRotation(float rotation) {
            _rotation = rotation;
            _material.SetFloat("_Rotation", _rotation);
        }

        public void SetMaterial(Material material) {
            _material = material;
        }


    }
}