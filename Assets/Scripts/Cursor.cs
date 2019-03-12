using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//This script controls the cursor used in the item menu
//The cursor behavior is functionally identical to the item menu cursor in The Link to the Past, which is the desired effect

namespace Assets.Scripts.Old
{
    public class Cursor : MonoBehaviour
    {

        private MenuOption m_Focus;
        private Vector2 m_position;
        private bool[] m_AxisInUse = new bool[2];
        IEnumerator m_CursorFlicker;
        public float m_FlickerRate = 0.35f;
        public Vector2 Position { get { return m_position; } }
        public bool Active
        {
            get { return gameObject.activeSelf; }
            set { gameObject.SetActive(value); }
        }

        void Update()
        {

            if (MenuManager.Instance.m_ActiveMenu != null && MenuManager.Instance.m_ActiveMenu.Active && MenuManager.Instance.m_ActiveMenu.m_HasActiveOptions)
            {

                UpdateSelection();
            }
            else
            {
                if (gameObject.activeSelf) { gameObject.SetActive(false); }
            }
        }

        public void UpdateSelection()
        {

            int focus = (int)m_position.y * MenuManager.Instance.m_ActiveMenu.m_ColumnCount + (int)m_position.x;
            m_Focus = MenuManager.Instance.m_ActiveMenu.m_MenuOptions[focus];

            UpdatePosition();
            UpdateDebugText();
        }

        public void Select()
        {

            TSelect(m_Focus);
        }

        public void ValidateInput(Vector2 currentInput)
        {
            if (Active && MenuManager.Instance.m_ActiveMenu.m_HasActiveOptions)
            {
                if (currentInput.x != 0)
                { //Move Cursor left/right
                    if (!m_AxisInUse[0])
                    {
                        m_AxisInUse[0] = true;
                        MenuManager.Instance.m_Cursor.TestMoveFocus(new Vector2(currentInput.x, 0));
                    }
                }
                else
                {
                    m_AxisInUse[0] = false;
                }

                if (currentInput.y != 0)
                {
                    if (!m_AxisInUse[1])
                    {
                        m_AxisInUse[1] = true;
                        MenuManager.Instance.m_Cursor.TestMoveFocus(new Vector2(0, currentInput.y));
                    }
                }
                else
                {
                    m_AxisInUse[1] = false;
                }
            }
        }

        public void MoveFocus(Vector2 movement)
        {

            Vector2 new_position = new Vector2(
                m_position.x + movement.x, // Add Horizontal component
                m_position.y - movement.y // Add Vertical component
                );

            int mod = MenuManager.Instance.m_ActiveMenu.m_MenuOptions.Count % MenuManager.Instance.m_ActiveMenu.m_ColumnCount > 0 ? 1 : 0; // If mod is 0, no extra row, if mod is 1, extra row

            new_position.x = new_position.x >= MenuManager.Instance.m_ActiveMenu.m_ColumnCount ? 0 : new_position.x; // Horizontal
            new_position.x = new_position.x < 0 ? MenuManager.Instance.m_ActiveMenu.m_ColumnCount - 1 : new_position.x; // Horizontal

            new_position.y = new_position.y >= (MenuManager.Instance.m_ActiveMenu.m_MenuOptions.Count / MenuManager.Instance.m_ActiveMenu.m_ColumnCount) + mod ? 0 : new_position.y; // Vertical
            new_position.y = new_position.y < 0 ? (MenuManager.Instance.m_ActiveMenu.m_MenuOptions.Count / MenuManager.Instance.m_ActiveMenu.m_ColumnCount) + mod - 1 : new_position.y; // Vertical

            if (new_position.y * MenuManager.Instance.m_ActiveMenu.m_ColumnCount + new_position.x >= MenuManager.Instance.m_ActiveMenu.m_MenuOptions.Count)
            { // Check if we exceed list range

                new_position.x = movement.x > 0 ? 0 : new_position.x; // Adjust Horizontal
                new_position.x = movement.x < 0 ? (MenuManager.Instance.m_ActiveMenu.m_MenuOptions.Count % MenuManager.Instance.m_ActiveMenu.m_ColumnCount) - 1 : new_position.x; // Adjust Horizontal

                new_position.y = movement.y < 0 ? 0 : new_position.y; // Adjust Vertical
                new_position.y = movement.y > 0 ? (MenuManager.Instance.m_ActiveMenu.m_MenuOptions.Count / MenuManager.Instance.m_ActiveMenu.m_ColumnCount) - 1 : new_position.y; // Adjust Vertical
            }

            m_position = new_position;
            RestartCursorFlicker();
        }

        public void TestMoveFocus(Vector2 movement)
        {

            int mod = MenuManager.Instance.m_ActiveMenu.m_MenuOptions.Count % MenuManager.Instance.m_ActiveMenu.m_ColumnCount > 0 ? 1 : 0;

            if (movement.x > 0)
            {

                for (int i = ((int)m_position.y * MenuManager.Instance.m_ActiveMenu.m_ColumnCount + (int)m_position.x) + 1; ;)
                {

                    if (i >= MenuManager.Instance.m_ActiveMenu.m_MenuOptions.Count) { i = 0; }
                    if (MenuManager.Instance.m_ActiveMenu.m_MenuOptions[i].gameObject.activeInHierarchy)
                    {
                        m_position = MenuManager.Instance.m_ActiveMenu.m_MenuOptions[i].m_Coordinates;
                        break;
                    }
                    ++i;
                }
            }

            if (movement.x < 0)
            {

                for (int i = ((int)m_position.y * MenuManager.Instance.m_ActiveMenu.m_ColumnCount + (int)m_position.x) - 1; ;)
                {

                    if (i < 0) { i = MenuManager.Instance.m_ActiveMenu.m_MenuOptions.Count - 1; }
                    if (MenuManager.Instance.m_ActiveMenu.m_MenuOptions[i].gameObject.activeInHierarchy)
                    {
                        m_position = MenuManager.Instance.m_ActiveMenu.m_MenuOptions[i].m_Coordinates;
                        break;
                    }
                    --i;
                }
            }

            if (movement.y > 0)
            {
                for (int i = ((int)m_position.y * MenuManager.Instance.m_ActiveMenu.m_ColumnCount + (int)m_position.x); ;)
                {

                    i -= MenuManager.Instance.m_ActiveMenu.m_ColumnCount;
                    if (i < 0) { i += (MenuManager.Instance.m_ActiveMenu.m_MenuOptions.Count / MenuManager.Instance.m_ActiveMenu.m_ColumnCount + mod) * MenuManager.Instance.m_ActiveMenu.m_ColumnCount; }
                    if (i >= MenuManager.Instance.m_ActiveMenu.m_MenuOptions.Count) { i -= MenuManager.Instance.m_ActiveMenu.m_ColumnCount; }
                    if (MenuManager.Instance.m_ActiveMenu.m_MenuOptions[i].gameObject.activeInHierarchy)
                    {
                        m_position = MenuManager.Instance.m_ActiveMenu.m_MenuOptions[i].m_Coordinates;
                        break;
                    }
                }
            }

            if (movement.y < 0)
            {
                for (int i = ((int)m_position.y * MenuManager.Instance.m_ActiveMenu.m_ColumnCount + (int)m_position.x); ;)
                {

                    i += MenuManager.Instance.m_ActiveMenu.m_ColumnCount;
                    if (i >= MenuManager.Instance.m_ActiveMenu.m_MenuOptions.Count) { i -= (MenuManager.Instance.m_ActiveMenu.m_MenuOptions.Count / MenuManager.Instance.m_ActiveMenu.m_ColumnCount + mod) * MenuManager.Instance.m_ActiveMenu.m_ColumnCount; }
                    if (i < 0) { i += MenuManager.Instance.m_ActiveMenu.m_ColumnCount; }
                    if (MenuManager.Instance.m_ActiveMenu.m_MenuOptions[i].gameObject.activeInHierarchy)
                    {
                        m_position = MenuManager.Instance.m_ActiveMenu.m_MenuOptions[i].m_Coordinates;
                        break;
                    }
                }
            }

            RestartCursorFlicker();
        }

        public void SetFocus(Vector2 new_position)
        {

            if (new_position.y * MenuManager.Instance.m_ActiveMenu.m_ColumnCount + new_position.x >= MenuManager.Instance.m_ActiveMenu.m_MenuOptions.Count)
            { // Check if we exceed list range

                m_position = Vector2.zero;

            }
            else
            {

                m_position = new_position;
            }
        }

        private void UpdatePosition()
        {

            transform.position = m_Focus.transform.position;
        }

        private void TSelect<T>(T TMenuOption) where T : MenuOption
        {

            TMenuOption.OnCursorSelect();
        }

        public void HideCursor()
        {
            gameObject.SetActive(false);
        }

        public void ShowCursor()
        {

            if (MenuManager.Instance.m_ActiveMenu.m_HasActiveOptions)
            {
                gameObject.SetActive(true);
                RestartCursorFlicker();
            }
        }

        private void UpdateDebugText()
        {

            MenuManager.Instance.m_DebugText.text = "Cur ( " + m_position.x + ", " + m_position.y + " )";
        }

        private void RestartCursorFlicker()
        {

            if (m_CursorFlicker != null) { StopCoroutine(m_CursorFlicker); }
            m_CursorFlicker = ICursorFlicker();
            StartCoroutine(m_CursorFlicker);

        }

        IEnumerator ICursorFlicker()
        {

            float _time = 0f;
            GetComponentInChildren<Image>().enabled = true;

            while (gameObject.activeSelf)
            {
                _time += Time.deltaTime;
                if (_time > m_FlickerRate)
                {
                    GetComponentInChildren<Image>().enabled = !GetComponentInChildren<Image>().enabled;
                    _time = 0f;
                }
                yield return new WaitForEndOfFrame();
            }
        }
    }
}