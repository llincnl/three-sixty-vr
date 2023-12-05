using UnityEngine;

namespace Viewer360 {
    public class ViewerPersistence : MonoBehaviour {

        private static ViewerPersistence _instance;
        
        private float _rotation;
        private float _cameraRotation;
        
        void Awake() {
            if (_instance == null) {
                _instance = this;
                DontDestroyOnLoad(this);
            } else {
                Destroy(gameObject);  
            }
        }



        public void SetRotation(float rotation) {
            _rotation = rotation;
        }

        public float GetRotation() {
            return _rotation;
        }

        public void SetCameraRotation(float cameraRotation) {
            _cameraRotation = cameraRotation;
        }

        public float GetCameraRotation() {
            return _cameraRotation;
        }
    }
}