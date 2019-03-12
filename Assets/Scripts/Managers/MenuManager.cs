using Assets.Scripts.Interfaces;
using Assets.Scripts.Interfaces.Managers;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Managers
{
    public class MenuManager : IMenuManager
    {
        private Stack<IMenu> _menuStack;

        public MenuManager()
        {
            _menuStack = new Stack<IMenu>();
        }

        public MenuManager(Stack<IMenu> menuStack)
        {
            _menuStack = menuStack;
        }

        public void GetGameObject()
        {
            throw new NotImplementedException();
        }

        public void ClearMenuStack()
        {
            _menuStack.Clear();
        }

        public IMenu CloseMenu()
        {
            return _menuStack.Pop();
        }

        public void OpenMenu(IMenu menu)
        {
            _menuStack.Push(menu);
        }
    }
}
