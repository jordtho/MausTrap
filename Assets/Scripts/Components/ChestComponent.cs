using Assets.Scripts.Enums;
using System.Collections;

namespace Assets.Scripts.Components
{
    public class ChestComponent : InteractableComponent
    {
        public bool _isOpen;
        public bool _randomize = false;

        public bool IsOpen { get => _isOpen; set => _isOpen = value; }

        public override void OnInteract(PlayerComponent player, InventoryComponent inventory)
        {
            Item = GetComponentInChildren<ItemComponent>();
            StartCoroutine(IOpenChest(player, inventory));
        }

        private IEnumerator IOpenChest(PlayerComponent player, InventoryComponent inventory)
        {
            if (IsOpen) { yield break; }

            UseAlternateSprite();
            AudioManager.Instance.m_AudioSource.PlayOneShot(AudioManager.Instance.m_OpenChestSoundEffect, 0.1f);

            if (Item != null)
            {
                Item = inventory.AddContents(Item);

                if (Item == null)
                {
                    AudioManager.Instance.m_AudioSource.PlayOneShot(AudioManager.Instance.m_ItemGetSoundEffect, 0.1f);
                }
                else
                {
                    player.AwaitDialog($"You already have a { Item.name }!\nCannot carry more.", DialogAwaitType.Acknowledge);
                    yield return player.DialogCoroutine;
                    ReplaceDefaultSprite();
                }
            }
            else
            {
                player.AwaitDialog("It's empty.", DialogAwaitType.Acknowledge);
                yield return player.DialogCoroutine;
            }

            IsOpen = (Item == null);
        }
    }
}
