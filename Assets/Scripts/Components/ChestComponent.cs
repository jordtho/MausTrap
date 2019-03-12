using UnityEngine;

namespace Assets.Scripts.Components
{
    public class ChestComponent : InteractableComponent
    {
        public bool _isOpen;

        public bool IsOpen { get => _isOpen; set => _isOpen = value; }
    }
}
