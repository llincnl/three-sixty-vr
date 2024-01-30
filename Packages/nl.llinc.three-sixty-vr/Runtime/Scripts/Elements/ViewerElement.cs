using UnityEngine;

namespace Viewer360 {
    public abstract class ViewerElement : MonoBehaviour {

        public abstract void UpdateValues();
        public abstract void GetComponents();
    }
}