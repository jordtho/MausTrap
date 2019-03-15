using Assets.Scripts.Enums;

namespace Assets.Scripts.Components
{
    public class NpcComponent : InteractableComponent
    {
        public Item m_RequiredFetchItem;

        public override void OnInteract(PlayerComponent player, InventoryComponent inventory)
        {
            if (Dialog != "") { player.AwaitDialog(Dialog, DialogAwaitType.Acknowledge); }

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
                }
            }
        }
    }
}