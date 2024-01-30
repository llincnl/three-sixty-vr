using Interfaces;
using UnityEngine;

namespace Viewer360 {
    /*A gaze pointer visual behaviour to manipulate properties(on/off, sprite, size)*/
    public class GazePointer: MonoBehaviour, IPointer {

        #region Fields

        [SerializeField] private GameObject pointer;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private Sprite _sprite;
        private float _size;
        private Color _color;
        
        #endregion
        
        public void Initialize() {
            Debug.Log("Gaze pointer initialized");
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

        // set custom size for pointer
        public void SetSize(float size) {
            _size = size;
            pointer.transform.localScale = new Vector3(_size, _size, _size);
        }

        // set color for pointer
        public void SetColor(Color color) {
            _color = color;
            spriteRenderer.color = _color;
        }
    }
}