using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Components
{
    public class MoneyHUDComponent : MonoBehaviour
    {
        public Text _moneyTextComponent;

        public void UpdateMoneyComponent(int value) => _moneyTextComponent.text = $"${value}";
    }
}
