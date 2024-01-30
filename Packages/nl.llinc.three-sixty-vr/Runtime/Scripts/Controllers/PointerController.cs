using System.Diagnostics.CodeAnalysis;
using Interfaces;
using NaughtyAttributes;
using UnityEngine;

namespace Viewer360 {
    
    /*A composite pointer controller to enable/disable two pointer interaction types and set the required (common)properties*/
    public class PointerController : MonoBehaviour, IVRController {

        #region Fields

        [SerializeField] private InteractionType _interactionType;
        
        [ShowIf("_interactionType", InteractionType.GazeInteraction)] private GazePointer _gazePointer;
        [ShowIf("_interactionType", InteractionType.RayInteraction)] private RayPointer _rayPointer;
        
        #endregion
        public void Initialize() {
            Debug.Log("Pointer Controller initialized");
        }
        
        // Update visual properties of pointer for selected interaction type
        [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalse")]
        public void UpdateController(InteractionType interactionType, bool pointerOn, Sprite pointerSprite, Color pointerColor, float pointerSize) {
            SetInteractionType(interactionType);
            if (pointerOn) {
                switch (interactionType) {
                    case InteractionType.GazeInteraction:
                        _gazePointer.SetPointerOn(pointerOn);
                        _gazePointer.SetSprite(pointerSprite);
                        _gazePointer.SetColor(pointerColor);
                        _gazePointer.SetSize(pointerSize);
                        break;
                    case InteractionType.RayInteraction:
                        _rayPointer.SetPointerOn(pointerOn);
                        _rayPointer.SetSprite(pointerSprite);
                        _rayPointer.SetColor(pointerColor);
                        break;
                    case InteractionType.None:
                        break;
                    default:
                        break;
                }
            }
        }

        public void SetInteractionType(InteractionType interactionType) {
            _interactionType = interactionType;
        }
    }
}