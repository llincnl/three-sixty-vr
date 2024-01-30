using System;
using Interfaces;
using UnityEngine;

namespace Viewer360 {
    
    /* A ray pointer visual behaviour that enables a constant pointer from oculus quest2 controllers and to manipulate properties(on/off, sprite, size). 
     Add elements to a {exclusionLayers} in Unity to remove pointer from specific elements
     */
    public class RayPointer: MonoBehaviour, IPointer {

        #region Fields

        [SerializeField] private GameObject pointer;
        
        [SerializeField] private Transform pointerTransform;

        [SerializeField] private Transform rayOrigin;
        
        [SerializeField] private LayerMask exclusionLayers;

        [SerializeField] private Vector3 originalScale;

        [SerializeField] private float defaultDistance = 3f;

        [SerializeField] private SpriteRenderer spriteRenderer;
        
        private Sprite _sprite;
        private Color _color;

        #endregion
        
        // preserve original scale for changing scale based on distance
        public void Initialize() {
            originalScale = pointerTransform.localScale;
        }

        // set pointer gameobject active
        public void SetPointerOn(bool on) {
            pointer.SetActive(on);
        }

        // update sprite(pointer visual) on the sprite renderer
        public void SetSprite(Sprite sprite) {
            _sprite = sprite;
            spriteRenderer.sprite = _sprite;
        }

        // set color for pointer
        public void SetColor(Color color) {
            _color = color;
            spriteRenderer.color = _color;
        }

        // Update method casts a ray to show a constant pointer on the canvas irrespective of ray hitting a collider
        void Update() {

            Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);

            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, ~exclusionLayers)) {
                pointerTransform.position = hit.point;

                pointerTransform.localRotation = Quaternion.LookRotation(hit.normal);

                pointerTransform.localScale = originalScale * hit.distance;
            }
            else {
                pointerTransform.position = rayOrigin.position + rayOrigin.forward * defaultDistance;

                pointerTransform.localRotation = rayOrigin.rotation;
            
                pointerTransform.localScale = originalScale * defaultDistance;
            }
            
        }
    }
}