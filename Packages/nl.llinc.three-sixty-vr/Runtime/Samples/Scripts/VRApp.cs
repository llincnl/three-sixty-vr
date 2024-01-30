using Viewer360;
using UnityEngine;

namespace Samples.Scripts {
    public class VRApp: MonoBehaviour {

        [SerializeField] private CompositeViewerController cvController;
        void Start() {
            cvController.Initialize();
            cvController.UpdateController();
        }

    }
}