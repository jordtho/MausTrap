using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public abstract class MenuComponent : MonoBehaviour
    {
        public List<MenuOptionComponent> _options;
        public int _columnCount = 1;
        private void Awake()
        {
            _options = GetComponentsInChildren<MenuOptionComponent>().ToList();
            gameObject.SetActive(false);
        }

        public void OpenMenu() => gameObject.SetActive(true);

        public void CloseMenu() => gameObject.SetActive(false);

        public void Move(Vector2 inputs)
        {
            
        }

        public void Accept()
        {

        }

        public void Cancel()
        {
            CloseMenu();
        }
    }
}
