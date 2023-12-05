using System;
using Interfaces;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Video;

namespace Viewer360 {
    
    /* A composite viewer controller that aggregates the top-level controllers and
    manages their initialization, update, and cleanup if any */
    [ExecuteInEditMode]
    public class CompositeViewerController: MonoBehaviour, IVRController {

        #region Fields
        
        // Types
        [SerializeField] private ViewerType _viewerType = ViewerType.None;
        [SerializeField] private InteractorType _interactorType = InteractorType.None;

        // Image
        [Foldout("Image")] [Label("Cubemap")] [SerializeField] [ShowIf("_viewerType", ViewerType.Image)]private Cubemap _imageCubemap;
        [Foldout("Image")] [Label("Rotation")] [SerializeField] [ShowIf("_viewerType", ViewerType.Image)]private float _imageRotation;
        [Foldout("Image")] [Label("Rotation")] [SerializeField] [ShowIf("_viewerType", ViewerType.Image)] private Material _imageMaterial;
        
        // Video
        [Foldout("Video")] [Label("Video Clip")] [ShowIf("_viewerType", ViewerType.Video)] [SerializeField] private VideoClip _videoClip;
        [Foldout("Video")] [Label("Audio Clip")] [ShowIf("_viewerType", ViewerType.Video)] [SerializeField] private AudioClip _audioClip;
        [Foldout("Video")] [Label("Audio Delay")] [ShowIf("_viewerType", ViewerType.Video)] [SerializeField] private float _audioDelay;
        [Foldout("Video")] [Label("Rotation")] [ShowIf("_viewerType", ViewerType.Video)] [SerializeField] private float _videoRotation;
        [Foldout("Video")] [Label("Load Scene On Video Finish")] [ShowIf("_viewerType", ViewerType.Video)] [SerializeField] private bool _loadSceneOnVideoFinish;
        [Foldout("Video")] [Label("Scene")] [ShowIf("_viewerType", ViewerType.Video)] [Scene] [SerializeField] private string _videoScene;
        
        // Transition
        [Foldout("Transition")] [Label("Duration")] [SerializeField] private float _transitionDuration;
        [Foldout("Transition")] [Label("Color")] [SerializeField] private Color _transitionColor;
        
        
        // Camera
        [Foldout("Camera")] [Label("Field of View")] [SerializeField] private float _cameraFieldOfView = 100;
        [Foldout("Camera")] [Label("Background Color")] [SerializeField] private Color _cameraBackgroundColor;
        
        // Controllers
        private ImageController _imageController;
        private CameraController _cameraController;
        private TransitionController _transitionController;
        private VideoController _videoController;
        
        #endregion
        
        // Start rendering and get all controller components
        private void Awake() {
            FindObjectOfType<Camera>().Render();
            GetComponents();
        }

        // Call initializers that map gameobjects and files to monobehaviour fields
        public void Initialize() {
            _imageController.Initialize();
            _cameraController.Initialize();
            _transitionController.Initialize();
            _videoController.Initialize();
            
            
        }

        // Update values on each controller
        [Button("Update Values")]
        public void UpdateController() {
            if (_viewerType == ViewerType.Image) {
                _imageController.UpdateController(_imageMaterial, _imageCubemap, _imageRotation);
            }
            else if (_viewerType == ViewerType.Video) {
                _videoController.UpdateController(_videoClip, _audioClip, _loadSceneOnVideoFinish, _videoScene, _audioDelay, _videoRotation);
            }
            else {
                _imageController.UpdateController(null, null, 0);
                _imageCubemap = null;
                _videoController.UpdateController(null, null, _loadSceneOnVideoFinish, _videoScene, _audioDelay, _videoRotation);
            }
            _transitionController.UpdateController(_transitionDuration, _transitionColor);
            _cameraController.UpdateController(_cameraFieldOfView, _cameraBackgroundColor);
        }
        
        // Find components attached to the parent gameobject
        private void GetComponents() {
            _videoController = GetComponentInChildren<VideoController>();
            _imageController = GetComponentInChildren<ImageController>();
            _transitionController = GetComponentInChildren<TransitionController>();
            _cameraController = GetComponentInChildren<CameraController>();
        }
    }
}