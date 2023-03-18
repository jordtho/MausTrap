using Assets.Scripts.Components.Items;
using Assets.Scripts.Enums;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class ChestComponent : InteractableComponent
    {
        #region Fields

        [SerializeField] private bool _isOpen;
        [SerializeField] private bool _randomize = false;
        [SerializeField] private Sprite _closedSprite;
        [SerializeField] private Sprite _openSprite;

        #endregion

        #region Properties

        public bool Randomize { get => _randomize; set => _randomize = value; }

        #endregion

        #region Methods

        public override void OnInteract(PlayerComponent player, InventoryComponent inventory)
        {
            StartCoroutine(IOpenChest(player, inventory));
        }

        private IEnumerator IOpenChest(PlayerComponent player, InventoryComponent inventory)
        {
            if (_isOpen || player.Character.Facing != Vector2.up) { yield break; }

            _spriteRenderer.sprite = _openSprite;
            AudioManager.Instance.m_AudioSource.PlayOneShot(AudioManager.Instance.m_OpenChestSoundEffect, 0.1f);

            if (_item != null)
            {
                yield return inventory.IAddContents(_item, player, (ItemComponent item) => { _item = item; });

                if (_item != null)
                {
                    player.AwaitDialog($"You already have a {_item.name}!\nCannot carry more.", DialogAwaitType.Acknowledge, InputType.Character);

                    yield return player.DialogCoroutine;

                    _spriteRenderer.sprite = _closedSprite;
                }
                else
                {
                    gameObject.layer = 0;
                }
            }
            else
            {
                player.AwaitDialog("It's empty.", DialogAwaitType.Acknowledge, InputType.Character);

                yield return player.DialogCoroutine;
            }

            _isOpen = (_item == null);
        }

        #endregion
    }
}
