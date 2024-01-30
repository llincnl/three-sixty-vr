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
        [SerializeField] private ViewerType viewerType = ViewerType.None;
        [SerializeField] private InteractionType interactionType = InteractionType.None;

        // Image
        [Foldout("Image")] [Label("Cubemap")] [SerializeField] [ShowIf("viewerType", ViewerType.Image)]private Cubemap _imageCubemap;
        [Foldout("Image")] [Label("Rotation")] [SerializeField] [ShowIf("viewerType", ViewerType.Image)]private float _imageRotation;
        [Foldout("Image")] [Label("Rotation")] [SerializeField] [ShowIf("viewerType", ViewerType.Image)] private Material _imageMaterial;
        
        // Video
        [Foldout("Video")] [Label("Video Clip")] [ShowIf("viewerType", ViewerType.Video)] [SerializeField] private VideoClip _videoClip;
        [Foldout("Video")] [Label("Audio Clip")] [ShowIf("viewerType", ViewerType.Video)] [SerializeField] private AudioClip _audioClip;
        [Foldout("Video")] [Label("Audio Delay")] [ShowIf("viewerType", ViewerType.Video)] [SerializeField] private float _audioDelay;
        [Foldout("Video")] [Label("Rotation")] [ShowIf("viewerType", ViewerType.Video)] [SerializeField] private float _videoRotation;
        [Foldout("Video")] [Label("Load Scene On Video Finish")] [ShowIf("viewerType", ViewerType.Video)] [SerializeField] private bool _loadSceneOnVideoFinish;
        [Foldout("Video")] [Label("Scene")] [ShowIf("viewerType", ViewerType.Video)] [Scene] [SerializeField] private string _videoScene;
        [Foldout("Video")] [Label("Player")] [ShowIf("viewerType", ViewerType.Video)] [SerializeField]
        private GameObject _videoPlayer;
        
        // Transition
        [Foldout("Transition")] [Label("Duration")] [SerializeField] private float _transitionDuration;
        [Foldout("Transition")] [Label("Color")] [SerializeField] private Color _transitionColor;
        
        
        // Camera
        [Foldout("Camera")] [Label("Field of View")] [SerializeField] private float _cameraFieldOfView = 100;
        [Foldout("Camera")] [Label("Background Color")] [SerializeField] private Color _cameraBackgroundColor;
        
        // Pointers
        [Foldout("Pointer")] [Label("On")] [SerializeField] private bool _pointerOn;
        [Foldout("Pointer")] [Label("Sprite")] [SerializeField] private Sprite _pointerSprite;
        [Foldout("Pointer")] [Label("Size")] [SerializeField] private float _pointerSize;
        [Foldout("Pointer")] [Label("Color")] [SerializeField] private Color _pointerColor;
        [Foldout("Pointer")] [Label("Sprite Renderer")] [SerializeField] private SpriteRenderer _spriteRenderer;

        // Controllers
        private ImageController _imageController;
        private CameraController _cameraController;
        private TransitionController _transitionController;
        private VideoController _videoController;
        private PointerController _pointerController;
        
        #endregion
        
        // Start rendering and get all controller components
        private void Awake() {
            ComponentsInitialisation();
        }

        void ComponentsInitialisation() {
            FindObjectOfType<Camera>().Render();
            GetComponents();
        }

        // Call controller initializers that map gameobjects and files to monobehaviour fields
        public void Initialize() {
            _imageController.Initialize();
            _cameraController.Initialize();
            _transitionController.Initialize();
            _videoController.Initialize();
            _pointerController.Initialize();
        }

        // Update all controllers
        [Button("Update Values")]
        public void UpdateController() {
            ComponentsInitialisation();
            UpdateViewerElements();
            if (viewerType == ViewerType.Image) {
                _imageController.UpdateController(_imageMaterial, _imageCubemap, _imageRotation);
                _videoPlayer.SetActive(false);
                _videoController.enabled = false;
            }
            else if (viewerType == ViewerType.Video) {
                _videoController.UpdateController(_videoClip, _audioClip, _loadSceneOnVideoFinish, _videoScene, _audioDelay, _videoRotation);
                _imageController.enabled = false;
            }
            else {
                _imageController.enabled = false;
                _imageCubemap = null;
                _videoController.enabled = false;
            }
            _transitionController.UpdateController(_transitionDuration, _transitionColor);
            _cameraController.UpdateController(viewerType, _cameraFieldOfView, _cameraBackgroundColor);
            _pointerController.UpdateController(interactionType, _pointerOn, _pointerSprite, _pointerColor, _pointerSize);
        }
        
        // Find components attached to the parent gameobject
        private void GetComponents() {
            _videoController = GetComponentInChildren<VideoController>();
            _imageController = GetComponentInChildren<ImageController>();
            _transitionController = GetComponentInChildren<TransitionController>();
            _cameraController = GetComponentInChildren<CameraController>();
            _pointerController = GetComponentInChildren<PointerController>();
        }
        
        private void UpdateViewerElements() {
            ViewerElement[] elements = FindObjectsOfType<ViewerElement>();
            foreach (ViewerElement e in elements) {
                e.UpdateValues();
            }
        }
    }
}