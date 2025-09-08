using UnityEngine;

namespace StarterAssets
{
    public class UICanvasControllerInput : MonoBehaviour
    {
        [Header("Output")]
        public EntityInputsManager inputs;

        public void VirtualMoveInput(Vector2 virtualMoveDirection)
        {
            inputs.MoveInput(virtualMoveDirection);
        }   
    }
}
