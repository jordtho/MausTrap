using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Old
{
    public class Menu : MenuOption
    {

        public List<MenuOption> m_MenuOptions = new List<MenuOption>();
        public int m_ColumnCount = 0; // Leave as 0 to keep all objects in the same row
        public Vector2 m_CursorCoordinates;
        public Menu m_PreviousMenu = null; //This will be set when this menu is initially accessed. Current plan is if Previous Menu is called when = null, menu will close?
        public bool m_HideWhenNotActive;
        public bool m_HasActiveOptions = false;

        public bool Active
        {
            get { return gameObject.activeSelf; }
            set { gameObject.SetActive(value); }
        }

        void Awake()
        {

            if (m_HideWhenNotActive && MenuManager.Instance.m_ActiveMenu != this) { Active = false; }

        }

        //We don't know for sure what type of menu options we will be working with (items, settings, dialog responses, etc.)
        public void AddCoordinatesToMenuOptions<T>() where T : MenuOption
        {

            int _RowCount = GetComponentsInChildren<T>(true).Length / m_ColumnCount;
            _RowCount += GetComponentsInChildren<T>(true).Length % m_ColumnCount > 0 ? 1 : 0;

            for (int j = 0; j < _RowCount; j++)
            {
                for (int i = 0; i < m_ColumnCount; i++)
                {
                    if (j * m_ColumnCount + i >= GetComponentsInChildren<T>(true).Length) { break; }
                    GetComponentsInChildren<T>(true)[j * m_ColumnCount + i].m_Coordinates = new Vector2(i, j);
                }
            }
        }


        public void RebuildMenuOptionsFromChildren<T>() where T : MenuOption
        {

            m_MenuOptions.Clear();
            m_HasActiveOptions = false;

            for (int i = 0; i < GetComponentsInChildren<T>(true).Length; i++)
            {
                if (GetComponent<T>() == GetComponentsInChildren<T>(true)[i])
                {
                    //break;
                }
                else
                {
                    m_MenuOptions.Add(GetComponentsInChildren<T>(true)[i]);
                }
            }

            foreach (T _MenuObject in GetComponentsInChildren<T>())
            {
                if (_MenuObject.gameObject.GetInstanceID() != GetInstanceID())
                {
                    m_HasActiveOptions = true;
                    break;
                }
            }
        }

        public void RemoveMenuOption<T>(T option) where T : MenuOption
        {

            m_MenuOptions.Remove(option);
        }
    }
}