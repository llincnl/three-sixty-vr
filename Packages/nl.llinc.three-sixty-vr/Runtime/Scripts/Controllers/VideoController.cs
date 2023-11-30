using Interfaces;
using UnityEngine;
using UnityEngine.Video;

namespace Viewer360 {
    public class VideoController : MonoBehaviour, IVRComponent {

        [SerializeField] private VideoPlayer _videoPlayer;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private Material _material;
        
        private VideoClip _videoClip;
        private AudioClip _audioClip;

        private bool _loadSceneOnVideoFinish;
        private string _scene;
        private float _delay;
        private float _rotation;

        void Awake() {
            ExecuteOperation();
        }
        
        public void ExecuteOperation() {
            _videoPlayer.prepareCompleted += OnComplete;
        }
        private void OnComplete(VideoPlayer videoPlayer) {
            _videoPlayer.Play();
    
            if (_delay > 0) {
                _audioSource.time = 0;
                _audioSource.Stop();
                _audioSource.volume = 1;
                _audioSource.PlayDelayed(_delay);
            } else if (_delay <= 0) {
                _audioSource.time = _delay;
                _audioSource.volume = 1;
            }
        }

        void Update() {
            if (_loadSceneOnVideoFinish) {
                if ((ulong)_videoPlayer.frame > 0 & (ulong)_videoPlayer.frame == _videoPlayer.frameCount - 2) {
                    TransitionController transitionController = FindObjectOfType<TransitionController>();
                    transitionController.LoadScene(_scene);
                }
            }
        }

        public void SetLoadSceneOnVideoFinish(bool loadSceneOnVideoFinish) {
            _loadSceneOnVideoFinish = loadSceneOnVideoFinish;
        }

        public void SetScene(string scene) {
            _scene = scene;
        }

        public void SetVideoClip(VideoClip videoClip) {
            _videoClip = videoClip;
            _videoPlayer.clip = _videoClip;
            _videoPlayer.targetTexture.Release();
        }

        public void SetAudioClip(AudioClip audioClip) {
            _audioClip = audioClip;
            _audioSource.clip = _audioClip;
        }

        public void SetAudioDelay(float delay) {
            _delay = delay;
        }

        public void SetRotation(float rotation) {
            _rotation = rotation;
            _material.SetFloat("_Rotation", _rotation);
        }
        
    }
}