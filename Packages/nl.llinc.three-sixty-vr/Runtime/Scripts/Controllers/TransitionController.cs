using DG.Tweening;
using Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Viewer360 {
    public class TransitionController : MonoBehaviour, IVRController {

        /*[SerializeField] private GazeInteractor _gazeInteractor;*/
        [SerializeField] private Image _transitionImage;

        private float _duration;
        private Color _color;
        
        public void Initialize() {
            Debug.Log("Transition Controller Initialized");
        }

        public void UpdateController(float duration, Color color) {
            SetDuration(duration);
            SetColor(color);
        }

        public void LoadScene(string sceneName) {
            ViewerPersistence viewerPersistence = FindObjectOfType<ViewerPersistence>();
            if (viewerPersistence != null) {
                viewerPersistence.SetRotation(0);
                viewerPersistence.SetCameraRotation(Camera.main.transform.localEulerAngles.y);
            }
            TransitionToScene(sceneName);
        }

        public void LoadScene(string sceneName, float rotation) {
            ViewerPersistence viewerPersistence = FindObjectOfType<ViewerPersistence>();
            if (viewerPersistence != null) {
                viewerPersistence.SetRotation(rotation);
                viewerPersistence.SetCameraRotation(Camera.main.transform.localEulerAngles.y);
            }
            TransitionToScene(sceneName);
        }

        private void TransitionToScene(string sceneName) {
            /*_gazeInteractor.Disable();*/
            _transitionImage.DOFade(1, _duration).OnComplete(() => {
                SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            });
        }

        public void SetDuration(float duration) {
            _duration = duration;
        }

        public void SetColor(Color color) {
            _color = color;
            _transitionImage.color = _color;
        }
    }   
}