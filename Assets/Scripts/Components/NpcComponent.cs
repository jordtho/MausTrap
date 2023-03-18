using Assets.Scripts.Components.Items;
using Assets.Scripts.Enums;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Components
{
    [RequireComponent(typeof(CharacterComponent))]
    public class NpcComponent : InteractableComponent
    {
        [SerializeField] private ItemComponent _requiredFetchItem;

        public override void OnInteract(PlayerComponent player, InventoryComponent inventory)
        {
            _item = GetComponentInChildren<ItemComponent>();
            StartCoroutine(IInteract(player, inventory));
        }

        private IEnumerator IInteract(PlayerComponent player, InventoryComponent inventory)
        {
            if (_dialog != string.Empty)
            {
                player.AwaitDialog(_dialog, DialogAwaitType.Acknowledge, InputType.Character);
                yield return player.DialogCoroutine;
            }

            if (_item != null)
            {
                yield return inventory.IAddContents(_item, player, (ItemComponent item) => { _item = item; });

                if (_item != null)
                {
                    player.AwaitDialog($"You already have a { _item.name }!\nCannot carry more.", DialogAwaitType.Acknowledge, InputType.Character);
                    yield return player.DialogCoroutine;
                }
            }
        }
    }
}