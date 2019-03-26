using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Components
{
    public class MenuCursorComponent : MonoBehaviour
    {
        private MenuOptionComponent _focus;
        public float _flicker = 0.35f;
        private bool[] m_AxisInUse = new bool[2];

        private IEnumerator FlickerCoroutine { get; set; }
        public int[] Position { get; private set; } = new[] { 0, 0 };
        public bool Active { get => isActiveAndEnabled; set => gameObject.SetActive(value); }
        public MenuComponent ActiveMenu { get; set; }
        public Image Image { get; set; }

        private void Awake()
        {
            Image = GetComponentInChildren<Image>();
        }

        void Update()
        {
            if (ActiveMenu != null && ActiveMenu.isActiveAndEnabled /*&& ActiveMenu.HasActiveOptions*/)
            {
                UpdateSelection();
            }
            else
            {
                if (Active) { Active = false; }
            }
        }

        public void UpdateSelection()
        {
            int focus = Position[1] * ActiveMenu._columnCount + Position[0];
            _focus = ActiveMenu._options[focus];

            UpdateTransform();
            UpdateDebugText();
        }

        public void Select() => _focus.OnSelect();

        public void Move(int[] input)
        {
            var position = new int[] {
                Position[0] + input[0],
                Position[1] - input[1]
            };

            int mod = ActiveMenu._options.Count % ActiveMenu._columnCount > 0 ? 1 : 0; // If mod is 0, no extra row, if mod is 1, extra row

            position[0] = position[0] >= ActiveMenu._columnCount ? 0 : position[0];
            position[0] = position[0] < 0 ? ActiveMenu._columnCount - 1 : position[0];

            position[1] = position[1] >= (ActiveMenu._options.Count / ActiveMenu._columnCount) + mod ? 0 : position[1];
            position[1] = position[1] < 0 ? (ActiveMenu._options.Count / ActiveMenu._columnCount) + mod - 1 : position[1];

            if (position[1] * ActiveMenu._columnCount + position[0] >= ActiveMenu._options.Count)
            {
                position[0] = input[0] > 0 ? 0 : position[0]; // Adjust Horizontal
                position[0] = input[0] < 0 ? (ActiveMenu._options.Count % ActiveMenu._columnCount) - 1 : position[0]; // Adjust Horizontal

                position[1] = input[1] < 0 ? 0 : position[1]; // Adjust Vertical
                position[1] = input[1] > 0 ? (ActiveMenu._options.Count / ActiveMenu._columnCount) - 1 : position[1]; // Adjust Vertical
            }

            Position = position;
            RestartCursorFlicker();
        }

        public void SetFocus(int[] position)
        {
            if (position[1] * ActiveMenu._columnCount + position[0] >= ActiveMenu._options.Count)
            {
                Position = new[] { 0, 0 };
            }
            else
            {
                Position = position;
            }
        }

        private void UpdateTransform()
        {
            transform.position = _focus.transform.position;
        }

        public void HideCursor() => gameObject.SetActive(false);

        public void ShowCursor()
        {
            // (ActiveMenu.HasActiveOptions)
            {
                gameObject.SetActive(true);
                RestartCursorFlicker();
            }
        }

        private void UpdateDebugText() => MenuManager.Instance.m_DebugText.text = "Cur ( " + Position[0] + ", " + Position[1] + " )";

        private void RestartCursorFlicker()
        {
            if (FlickerCoroutine != null) { StopCoroutine(FlickerCoroutine); }
            FlickerCoroutine = ICursorFlicker();
            StartCoroutine(FlickerCoroutine);

        }

        IEnumerator ICursorFlicker()
        {
            float _time = 0f;
            Image.enabled = true;

            while (Active)
            {
                _time += Time.deltaTime;
                if (_time > _flicker)
                {
                    Image.enabled = !Image.enabled;
                    _time = 0f;
                }
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
