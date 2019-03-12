using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.DataModels
{
    public class PlayerCharacter : Character, IPlayerCharacter
    {
        public IInventory Inventory { get; set; }

        public void Interact(IInteractable target)
        {
            target.GetInteraction(this);
        }
    }
}
