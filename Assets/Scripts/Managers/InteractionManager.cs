using Assets.Scripts.Components;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class InteractionManager : MonoBehaviour
    {
        public delegate void Interaction(PlayerComponent player, InventoryComponent inventory, InteractableComponent interactable);

        public void Initialize(PlayerCharacterComponent playerCharacter)
        {
            playerCharacter.OnInteract = OnInteract;
        }

        public void OnInteract(PlayerComponent player, InventoryComponent inventory, InteractableComponent interactable)
        {
            if (interactable.Dialog != "") { player.AwaitDialog(interactable.Dialog, DialogAwaitType.Acknowledge); }

            if(interactable.Item != null)
            {
                interactable.Item = inventory.AddContents(interactable.Item);

                if (interactable.Item == null)
                {
                    AudioManager.Instance.m_AudioSource.PlayOneShot(AudioManager.Instance.m_ItemGetSoundEffect, 0.1f);
                }
                else
                {
                    player.AwaitDialog($"You already have a { interactable.Item.name }!\nCannot carry more.", DialogAwaitType.Acknowledge);
                    //StartCoroutine(ReplaceDefaultSprite());
                    if (interactable.GetComponent<Pickup>()) { interactable.GetComponent<Pickup>().m_CanInteract = false; }
                }

                //ItemGetAnimation();
            }
        }

        public void OpenChest(PlayerComponent player, InventoryComponent inventory, ChestComponent chest)
        {
            if (chest.IsOpen) { return; }

            chest.UseAlternateSprite();
            AudioManager.Instance.m_AudioSource.PlayOneShot(AudioManager.Instance.m_OpenChestSoundEffect, 0.1f);

            if (chest.Item != null)
            {
                chest.Item = inventory.AddContents(chest.Item);

                if (chest.Item == null)
                {
                    AudioManager.Instance.m_AudioSource.PlayOneShot(AudioManager.Instance.m_ItemGetSoundEffect, 0.1f);
                }
                else
                {
                    player.AwaitDialog($"You already have a { chest.Item.name }!\nCannot carry more.", DialogAwaitType.Acknowledge);
                    //StartCoroutine(ReplaceDefaultSprite());
                }

                //ItemGetAnimation();
            }
            else
            {
                player.AwaitDialog("It's empty.", DialogAwaitType.Acknowledge);
            }

            chest.IsOpen = (chest.Item == null);
        }
    }
}
