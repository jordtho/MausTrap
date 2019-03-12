using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType {

    //Types of item pick ups

    NONE,
    HEART,
    CASH_1,
    CASH_5,
    CASH_20,
    ITEM,
    MAX_PICKUPTYPE
}

public class Pickup : Interactable {

    public PickupType m_Type;
    public bool m_CanInteract = true;

    public void PickUpItem(Player player) {

        //Who is picking up the item?

        m_InteractingPlayer = player;

        //Determine Pickup Type

        if(GetComponent<Item>()) {
            m_Item = GetComponent<Item>();
            AddItem();
        }

        switch(m_Type) {
            case PickupType.NONE: break;
            case PickupType.HEART: AddHeart(); break;
            case PickupType.CASH_1: AddCash(1); break;
            case PickupType.CASH_5: AddCash(5); break;
            case PickupType.CASH_20: AddCash(20); break;
            case PickupType.ITEM: return;
            default: Debug.Log("PickUpType does not exist: AssetsScriptsPickup.cs"); break;
        }
    }

    public override void ItemGetAnimation() {

        if(m_CanInteract) {
            m_InteractingPlayer.m_AnimationHelper.UpdateAnimation(m_Item.GetComponent<Animator>());
        }
    }

    void AddHeart() {

        //Play Sound-Effect

        //Add Health
        m_InteractingPlayer.m_HP++;
        m_InteractingPlayer.UpdateHealth(m_InteractingPlayer.m_HP);

        Destroy(transform.parent.gameObject);
    }

    void AddCash(int amount) {

        //Play Sound-Effect

        //Add Cash
        m_InteractingPlayer.m_Money += amount;

        Destroy(transform.parent.gameObject);
    }
}