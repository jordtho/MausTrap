using Assets.Scripts.Enums;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class PlayerComponent : MonoBehaviour
    {
        public PlayerInputComponent _playerInput;

        public PlayerCharacterComponent _playerCharacterComponent;
        public MenuComponent _menuComponent;
        public DialogComponent _dialogComponent;

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
            //_playerInput.ButtonX += _menuComponent.OpenMenu;
            _playerInput.ButtonY = _playerCharacterComponent.UseItem;
        }

        private void SetInputMenu()
        {
            _playerInput.DirectionalPad = _menuComponent.Move;
            _playerInput.ButtonA = () => _menuComponent.Accept();
            _playerInput.ButtonB = () => _menuComponent.Cancel();
        }

        private void SetInputDialog()
        {
            _playerInput.ButtonA = () => _dialogComponent.Accept();
            _playerInput.ButtonB = () => _dialogComponent.Cancel();
        }

        public void AwaitDialog(string text, DialogAwaitType dialogAwaitType) => StartCoroutine(IAwaitDialog(text, dialogAwaitType));

        private IEnumerator IAwaitDialog(string text, DialogAwaitType dialogAwaitType)
        {
            SetInputs(InputType.Dialog);
            _dialogComponent.CreateDialog(text, dialogAwaitType);

            while(_dialogComponent._active) { yield return null; }

            SetInputs(InputType.Character);
        }
    }
}
