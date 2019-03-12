using Assets.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryObject : MenuOption {

    //public string m_ItemName;
    public Item m_AssociatedItem;

    public void ShowInventoryObject() {

        if(!gameObject.activeSelf) {
            gameObject.SetActive(true);
        }

        Image[] _Images = GetComponentsInChildren<Image>(true);

        for(int i = 0; i < _Images.Length; i++) {
            if(!_Images[i].gameObject.activeInHierarchy) {
                _Images[i].gameObject.SetActive(true);
                break;
            }
        }
    }

    public override void OnCursorSelect() {

        GameManager.Instance.m_EquippedItemImage.sprite = GetComponentsInChildren<Image>()[GetComponentsInChildren<Image>().Length - 1].sprite;
        m_AssociatedItem.m_Owner.GetComponent<Player>().m_EquippedItem = m_AssociatedItem;
        m_AssociatedItem.m_Owner.GetComponent<Player>().UpdateEquippedItemQuantityText();

        //gameObject.SetActive(false);
        //MenuManager.Instance.m_ActiveMenu.RemoveMenuOption(this);
    }
}