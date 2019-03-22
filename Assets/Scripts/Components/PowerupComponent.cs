using UnityEngine;

namespace Assets.Scripts.Components
{
    public class PowerupComponent : MonoBehaviour
    {
        public PickupType _type;
        public ItemComponent _item;

        private void Awake()
        {
            _item = GetComponent<ItemComponent>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Heart!");
            var playerCharacter = collision.GetComponent<PlayerCharacterComponent>();

            if (playerCharacter != null)
            {
                Get(playerCharacter);
            }
        }

        public void Get(PlayerCharacterComponent playerCharacter)
        {
            switch (_type)
            {
                case PickupType.NONE: break;
                case PickupType.HEART: AddHeart(playerCharacter); break;
                case PickupType.CASH_1: AddCash(playerCharacter, 1); break;
                case PickupType.CASH_5: AddCash(playerCharacter, 5); break;
                case PickupType.CASH_20: AddCash(playerCharacter, 20); break;
                case PickupType.ITEM: AddItem(playerCharacter); break;
                default: Debug.Log("PickUpType does not exist: AssetsScriptsPickup.cs"); break;
            }
        }

        public void AddItem(PlayerCharacterComponent playerCharacter)
        {

        }

        void AddHeart(PlayerCharacterComponent playerCharacter)
        {
            playerCharacter.RestoreHealth(1);
            Destroy(transform.parent.gameObject);
        }

        void AddCash(PlayerCharacterComponent playerCharacter, int amount)
        {
            playerCharacter._money += amount;
            Destroy(transform.parent.gameObject);
        }
    }
}
