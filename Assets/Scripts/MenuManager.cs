using Assets.Scripts.Old;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : Singleton<MenuManager> {

    protected MenuManager() { }

    public Menu m_ActiveMenu;
    public List<Menu> m_MenuStack;
    public Text m_DebugText;
    public Assets.Scripts.Old.Cursor m_Cursor;

    private void ShowMenu(Menu menu) {

        menu.Active = true;
    }

    private void HideMenu(Menu menu) {

        menu.Active = false;
    }

    public void OpenMenu(Menu menu) {

        m_MenuStack.Add(menu);
        SetActiveMenu();

        if(!m_Cursor.isActiveAndEnabled) {
            m_Cursor.ShowCursor();
        }
    }

    public void CloseCurrentMenu() {
        if(m_MenuStack.Count > 1) {
            m_MenuStack.RemoveAt(m_MenuStack.Count - 1);
            SetActiveMenu();
        } else {
            //Close menu entirely, probably. This will be more important to flesh out in Overworld.
            m_Cursor.HideCursor();
            HideMenu(m_ActiveMenu);
            m_MenuStack.Clear();
        }
    }

    private void SetActiveMenu() {

        if(m_MenuStack.Count > 0) {
            if(m_ActiveMenu != null) {
                m_ActiveMenu.m_CursorCoordinates = m_Cursor.Position;
                if(m_ActiveMenu.m_HideWhenNotActive) { HideMenu(m_ActiveMenu); }
            }

            m_ActiveMenu = m_MenuStack[m_MenuStack.Count - 1];
            m_ActiveMenu.Active = true;
            if(m_ActiveMenu.m_HideWhenNotActive) { ShowMenu(m_ActiveMenu); }
            m_Cursor.SetFocus(m_ActiveMenu.m_CursorCoordinates);
        }
    }

    public Menu NewMenu<T>(List<T> menuOptions, int o_MenuColumns = 0, string o_MenuName = "NewMenu") where T : MenuOption {
    //public Menu NewMenu(List<MenuOption> menuOptions, int o_MenuColumns = 0, string o_MenuName = "NewMenu") {

        GameObject obj = new GameObject(o_MenuName);
        Menu a = obj.AddComponent(typeof(Menu)) as Menu;

        foreach(T option in menuOptions) { a.m_MenuOptions.Add(option); }

        //a.m_MenuOptions = menuOptions;

        a.m_ColumnCount = o_MenuColumns > 0 ? o_MenuColumns : menuOptions.Count;

        return a;
    }
}