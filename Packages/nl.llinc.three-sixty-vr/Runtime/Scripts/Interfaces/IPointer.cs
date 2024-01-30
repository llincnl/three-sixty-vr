using UnityEngine;

namespace Interfaces {
    
    /* A pointer interface providing abstract pointer behaviours*/
    public interface IPointer {

        public void Initialize();

        public void SetPointerOn(bool on);

        public void SetSprite(Sprite sprite);

        public void SetSize(float size) { }

        public void SetColor(Color color);

    }
}