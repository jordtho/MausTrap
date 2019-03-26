using Assets.Scripts.Enums;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class PlayerComponent : MonoBehaviour
    {
        public PlayerInputComponent _playerInput;

        public PlayerCharacterComponent _playerCharacterComponent;
        public DialogComponent _dialogComponent;
        public InventoryMenuComponent _inventoryMenuComponent;
        public MapComponent _mapComponent;
        public MenuComponent _menuComponent;

        public IEnumerator DialogCoroutine { get; private set; }
        public IEnumerator InventoryCoroutine { get; private set; }
        public IEnumerator MapCoroutine { get; private set; }
        public IEnumerator MenuCoroutine { get; private set; }

        private void Awake()
        {
            SetInputs(InputType.Character);
        }

        private void SetInputs(InputType inputType)
        {
            ClearInputs();

            switch (inputType)
            {
                case InputType.Character: { SetInputCharacter(); } break;
                case InputType.Menu: { SetInputMenu(); } break;
                case InputType.Inventory: { SetInputInventory(); break; }
                case InputType.Dialog: { SetInputDialog(); } break;
                default:
                    Debug.Log($"Could not set {inputType}");
                    break;
            }
        }

        private void ClearInputs()
        {
            _playerInput.DirectionalPad = null;
            _playerInput.ButtonA = null;
            _playerInput.ButtonB = null;
            _playerInput.ButtonX = null;
            _playerInput.ButtonY = null;
            _playerInput.ButtonL = null;
            _playerInput.ButtonR = null;
            _playerInput.ButtonStart = null;
            _playerInput.ButtonSelect = null;

            _playerCharacterComponent.Move(Vector2.zero);
        }

        private void SetInputCharacter()
        {
            _playerInput.DirectionalPad = _playerCharacterComponent.Move;
            _playerInput.ButtonA = () => _playerCharacterComponent.Interact(this);
            _playerInput.ButtonB = _playerCharacterComponent.Attack;
            _playerInput.ButtonX = () => AwaitInventory();
            _playerInput.ButtonY = _playerCharacterComponent.UseItem;
        }

        private void SetInputDialog()
        {
            _playerInput.ButtonA = () => _dialogComponent.Accept();
            _playerInput.ButtonB = () => _dialogComponent.Cancel();
        }

        private void SetInputInventory()
        {
            _playerInput.DirectionalPad = _inventoryMenuComponent.Move;
            _playerInput.ButtonA = () => _inventoryMenuComponent.Accept();
            _playerInput.ButtonB = () => _inventoryMenuComponent.Cancel();
            _playerInput.ButtonX = () => _inventoryMenuComponent.CloseMenu();
        }

        private void SetInputMenu()
        {
            _playerInput.DirectionalPad = _menuComponent.Move;
            _playerInput.ButtonA = () => _menuComponent.Accept();
            _playerInput.ButtonB = () => _menuComponent.Cancel();
            _playerInput.ButtonStart = () => _menuComponent.CloseMenu();
        }

        public void AwaitDialog(string text, DialogAwaitType dialogAwaitType)
        {
            DialogCoroutine = IAwaitDialog(text, dialogAwaitType);
            StartCoroutine(DialogCoroutine);
        }

        private IEnumerator IAwaitDialog(string text, DialogAwaitType dialogAwaitType)
        {
            SetInputs(InputType.Dialog);
            _dialogComponent.CreateDialog(text, dialogAwaitType);

            while(_dialogComponent.isActiveAndEnabled) { yield return null; }

            SetInputs(InputType.Character);
        }

        public void AwaitInventory()
        {
            InventoryCoroutine = IAwaitInventory();
            StartCoroutine(InventoryCoroutine);
        }

        private IEnumerator IAwaitInventory()
        {
            SetInputs(InputType.Inventory);
            _inventoryMenuComponent.OpenMenu();

            while (_inventoryMenuComponent.isActiveAndEnabled) { yield return null; }

            SetInputs(InputType.Character);
        }

        public void AwaitMap()
        {
            MapCoroutine = IAwaitMap();
            StartCoroutine(MapCoroutine);
        }

        private IEnumerator IAwaitMap()
        {
            SetInputs(InputType.Map);
            _mapComponent.OpenMap();

            while (_mapComponent.isActiveAndEnabled) { yield return null; }

            SetInputs(InputType.Character);
        }

        public void AwaitMenu()
        {
            MenuCoroutine = IAwaitMenu();
            StartCoroutine(MenuCoroutine);
        }

        private IEnumerator IAwaitMenu()
        {
            SetInputs(InputType.Menu);
            _menuComponent.OpenMenu();

            while (_menuComponent.isActiveAndEnabled) { yield return null; }

            SetInputs(InputType.Character);
        }
    }
}
