using Assets.Scripts.Old;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOption : MonoBehaviour {

    [HideInInspector] public Assets.Scripts.Old.Cursor m_Cursor;
    public bool m_Selected;
    public Menu m_NextMenu;
    public Vector2 m_Coordinates = new Vector2();


    void Awake() {

        m_Cursor = MenuManager.Instance.m_Cursor;
    }

    public virtual void OnCursorSelect() {

        if(m_NextMenu != null) {

            MenuManager.Instance.OpenMenu(m_NextMenu);
        }
    }
}