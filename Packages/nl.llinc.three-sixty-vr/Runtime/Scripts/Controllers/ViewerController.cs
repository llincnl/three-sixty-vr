using Interfaces;
using UnityEngine;
using UnityEngine.Video;
using NaughtyAttributes;

namespace Viewer360 {
    [ExecuteInEditMode]
    public class ViewerController : MonoBehaviour, IVRComponent {

        #region Fields
        [SerializeField] private ViewerType _viewerType = ViewerType.None;
        [SerializeField] private InteractorType viewerInteractionType;
        
        //Transition
        [Foldout("Transition")] [Label("Duration")] [SerializeField] private float _transitionDuration;
        [Foldout("Transition")] [Label("Color")] [SerializeField] private Color _transitionColor;

        //Video
        [Foldout("Video")] [Label("Video Clip")] [ShowIf("_viewerType", ViewerType.Video)] [SerializeField] private VideoClip _videoClip;
        [Foldout("Video")] [Label("Audio Clip")] [ShowIf("_viewerType", ViewerType.Video)] [SerializeField] private AudioClip _audioClip;
        [Foldout("Video")] [Label("Audio Delay")] [ShowIf("_viewerType", ViewerType.Video)] [SerializeField] private float _audioDelay;
        [Foldout("Video")] [Label("Rotation")] [ShowIf("_viewerType", ViewerType.Video)] [SerializeField] private float _videoRotation;
        [Foldout("Video")] [Label("Load Scene On Video Finish")] [ShowIf("_viewerType", ViewerType.Video)] [SerializeField] private bool _loadSceneOnVideoFinish;
        [Foldout("Video")] [Label("Scene")] [ShowIf("_viewerType", ViewerType.Video)] [Scene] [SerializeField] private string _videoScene;

        //Image
        [Foldout("Image")] [Label("Cubemap")] [SerializeField] [ShowIf("_viewerType", ViewerType.Image)]private Cubemap _imageCubemap;
        [Foldout("Image")] [Label("Rotation")] [SerializeField] [ShowIf("_viewerType", ViewerType.Image)]private float _imageRotation;

        //Camera
        [Foldout("Camera")] [Label("Field of View")] [SerializeField] private float _cameraFieldOfView = 100;
        [Foldout("Camera")] [Label("Background Color")] [SerializeField] private Color _cameraBackgroundColor;

        [Foldout("Pointer")] [Label("On")] [SerializeField] private bool _pointerOn;
        [Foldout("Pointer")] [Label("Sprite")] [SerializeField] private Sprite _pointerSprite;
        [Foldout("Pointer")] [Label("Size")] [SerializeField] private float _pointerSize;
        [Foldout("Pointer")] [Label("Color")] [SerializeField] private Color _pointerColor;
        
        //Home Button
        [Foldout("Home Button")] [Label("On")] [SerializeField] private bool _homeButtonOn;
        [Foldout("Home Button")] [Label("Scene")] [Scene] [SerializeField] private string _homeButtonScene;
        [Foldout("Home Button")] [Label("Duration")]  [SerializeField] private float _homeButtonDuration;
        [Foldout("Home Button")] [Label("Y Position")] [SerializeField] private float _homeButtonPositionY;
        [Foldout("Home Button")] [Label("Z Position")] [SerializeField]private float _homeButtonPositionZ;
        [Foldout("Home Button")] [Label("Rotation")] [SerializeField] private float _homeButtonRotation;
        [Foldout("Home Button")] [Label("Width")] [SerializeField] private float _homeButtonWidth;
        [Foldout("Home Button")] [Label("Height")] [SerializeField] private float _homeButtonHeight;
        [Foldout("Home Button")] [Label("Text")] [SerializeField] private string _homeButtonText;
        [Foldout("Home Button")] [Label("Text Size")] [SerializeField] private float _homeButtonTextSize;
        [Foldout("Home Button")] [Label("Text Color")] [SerializeField] private Color _homeButtonTextColor;
        [Foldout("Home Button")] [Label("Base Color")] [SerializeField] private Color _homeButtonBaseColor;
        [Foldout("Home Button")] [Label("Fill Color")] [SerializeField] private Color _homeButtonFillColor;

        private VideoController _videoController;
        private ImageController _imageController;
        private TransitionController _transitionController;
        private CameraController _cameraController;
        private PointerController _pointerController;
        /*private HomeButtonController _homeButtonController;*/ // TODO: To be taken up on buttons PR

        
        #endregion
        void Awake() {
            ExecuteOperation();
        }

        [Button("Update Values")]
        public void ExecuteOperation() {
            FindObjectOfType<Camera>().Render();
            GetComponents();
            UpdateViewerType();
            UpdateVideoController();
            UpdateImageController();
            UpdateTransitionController();
            UpdateCameraController();
            if (viewerInteractionType == InteractorType.GazeInteraction) {
                UpdatePointerController();
            }
            UpdateHomeButtonController();
            UpdateViewerElements();
        }
        
        
        private void GetComponents() {
            _videoController = GetComponentInChildren<VideoController>();
            _imageController = GetComponentInChildren<ImageController>();
            _transitionController = GetComponentInChildren<TransitionController>();
            _cameraController = GetComponentInChildren<CameraController>();
            if (viewerInteractionType == InteractorType.GazeInteraction) {
                _pointerController = GetComponentInChildren<PointerController>();
            }
            /*_homeButtonController = GetComponentInChildren<HomeButtonController>();*/ // TODO: To be taken up on buttons PR
        }

        private void UpdateViewerType() {   
            _cameraController.SetViewerType(_viewerType);
        }

        private void UpdateVideoController() {
            if (_viewerType == ViewerType.Video) {
                _videoController.SetVideoClip(_videoClip);
                _videoController.SetAudioClip(_audioClip);
                _videoController.SetAudioDelay(_audioDelay);
                _videoController.SetRotation(_videoRotation); 
                _videoController.SetLoadSceneOnVideoFinish(_loadSceneOnVideoFinish);
                _videoController.SetScene(_videoScene);
            } else {
                _videoController.SetVideoClip(null);
                _videoController.SetAudioClip(null);
                _videoClip = null;
                _audioClip = null;
            }
        }

        private void UpdateImageController() {
            if (_viewerType == ViewerType.Image) {
                _imageController.SetCubemap(_imageCubemap);
                _imageController.SetRotation(_imageRotation);
            } else {
                _imageController.SetCubemap(null);
                _imageCubemap = null;
            }
        }

        private void UpdateTransitionController() {
            _transitionController.SetDuration(_transitionDuration);
            _transitionController.SetColor(_transitionColor);
        }

        private void UpdateCameraController() {
            _cameraController.SetFieldOfView(_cameraFieldOfView);
            _cameraController.SetBackgroundColor(_cameraBackgroundColor);
            _cameraController.Rotate();
        }

        private void UpdatePointerController() {
            _pointerController.SetOn(_pointerOn);
            _pointerController.SetSprite(_pointerSprite);
            _pointerController.SetSize(_pointerSize);
            _pointerController.SetColor(_pointerColor);
        }

        // TODO: To be taken up on buttons PR
        private void UpdateHomeButtonController() {
            /*_homeButtonController.GetComponents();
            _homeButtonController.SetOn(_homeButtonOn);
            _homeButtonController.SetScene(_homeButtonScene);
            _homeButtonController.SetWidth(_homeButtonWidth);
            _homeButtonController.SetHeight(_homeButtonHeight);
            _homeButtonController.SetPositionY(_homeButtonPositionY);
            _homeButtonController.SetPositionZ(_homeButtonPositionZ);
            _homeButtonController.SetRotation(_homeButtonRotation);
            _homeButtonController.SetText(_homeButtonText);
            _homeButtonController.SetTextSize(_homeButtonTextSize);
            _homeButtonController.SetTextColor(_homeButtonTextColor);
            _homeButtonController.SetBaseColor(_homeButtonBaseColor);
            if (viewerInteractionType == InteractorType.GazeInteraction) {
                _homeButtonController.SetDuration(_homeButtonDuration);
                _homeButtonController.SetFillColor(_homeButtonFillColor);
            }*/
        }

        private void UpdateViewerElements() {
            ViewerElement[] elements = FindObjectsOfType<ViewerElement>();
            foreach (ViewerElement e in elements) {
                e.UpdateValues();
            }
        }


    }
}