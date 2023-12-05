using Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Viewer360 {
    public class CameraController : MonoBehaviour, IVRController {

        [SerializeField] private Camera _camera;
        [SerializeField] private GameObject _cameraOffset;
        [SerializeField] private Skybox _skybox;

        [SerializeField] private Material _imageMaterial;
        [SerializeField] private Material _videoMaterial;
    
        private float _fieldOfView;
        private Color _backgroundColor;

        public void Initialize() {
            _camera = FindObjectOfType<Camera>();
            _cameraOffset = _camera.gameObject;
            _skybox = _camera.GetComponent<Skybox>();
            Debug.Log("Camera Initialized");
        }

        public void UpdateController(float _cameraFieldOfView, Color _cameraBackgroundColor) {
            SetFieldOfView(_cameraFieldOfView);
            SetBackgroundColor(_cameraBackgroundColor);
            Rotate();
        }

        public void Rotate() {
            ViewerPersistence viewerPersistence = FindObjectOfType<ViewerPersistence>();
            if (viewerPersistence != null) {
                float rotation = viewerPersistence.GetRotation();
                float cameraRotation = viewerPersistence.GetCameraRotation();
                _cameraOffset.transform.localEulerAngles = new Vector3(0, rotation - cameraRotation, 0);
            }
        }

        public void SetViewerType(ViewerType viewerType) {
            if (viewerType == ViewerType.None) {
                _camera.clearFlags = CameraClearFlags.SolidColor;
                _skybox.material = null;
                _skybox.enabled = false;
            } else if (viewerType == ViewerType.Image) {
                _camera.clearFlags = CameraClearFlags.Skybox;
                _skybox.material = _imageMaterial;
                _skybox.enabled = true;
            } else if (viewerType == ViewerType.Video) {
                _camera.clearFlags = CameraClearFlags.Skybox;
                _skybox.material = _videoMaterial;
                _skybox.enabled = true;
            }
        }

        public void SetBackgroundColor(Color color) {
            _backgroundColor = color;
            _camera.backgroundColor = _backgroundColor;
        }
        
        public void SetFieldOfView(float fieldOfView) {
            _fieldOfView = fieldOfView;
            _camera.fieldOfView = _fieldOfView;
        }


    }
}