using UnityEngine;

namespace Viewer360 {
    public class PointerController : MonoBehaviour {
        
        [SerializeField] private GameObject _pointer;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private Sprite _sprite;
        private float _size;
        private Color _color;

        public void SetOn(bool on) {
            _pointer.SetActive(on);
        }

        public void SetSprite(Sprite sprite) {
            _sprite = sprite;
            _spriteRenderer.sprite = _sprite;
        }

        public void SetSize(float size) {
            _size = size;
            _pointer.transform.localScale = new Vector3(_size, _size, _size);
        }

        public void SetColor(Color color) {
            _color = color;
            _spriteRenderer.color = _color;
        }
    }
}