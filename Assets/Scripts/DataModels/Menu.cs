using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.DataModels
{
    public class Menu : MonoBehaviour, IMenu
    {
        private List<IMenuNode> _menuOptions;
        public IMenuNode this[int index]
        {
            get { return _menuOptions[index]; }
            set { _menuOptions[index] = value; }
        }
        public int Count => _menuOptions.Count;
        public bool IsReadOnly { get; }
        public int ColumnCount
        {
            get { return ColumnCount; }
            set { ColumnCount = value; }
        }

        public IMenuCursor Cursor { get; private set; }
        public bool HasFocus { get; set; }

        public Menu(List<IMenuNode> menuOptions = null)
        {
            _menuOptions = menuOptions ?? new List<IMenuNode>();
            Cursor = new Objects.MenuCursor(this);
        }

        public void Add(IMenuNode item)
        {
            _menuOptions.Add(item);
        }
        public void Clear()
        {
            _menuOptions.Clear();
        }
        public bool Contains(IMenuNode item)
        {
            return _menuOptions.Contains(item);
        }
        public void CopyTo(IMenuNode[] array, int arrayIndex)
        {
            _menuOptions.CopyTo(array, arrayIndex);
        }
        public IEnumerator<IMenuNode> GetEnumerator()
        {
            return _menuOptions.GetEnumerator();
        }
        public int IndexOf(IMenuNode item)
        {
            return _menuOptions.IndexOf(item);
        }
        public void Insert(int index, IMenuNode item)
        {
            _menuOptions.Insert(index, item);
        }
        public bool Remove(IMenuNode item)
        {
            return _menuOptions.Remove(item);
        }
        public void RemoveAt(int index)
        {
            _menuOptions.RemoveAt(index);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private bool Open(IMenuNode menuNode) => menuNode.SubMenu != null;

        public void Select()
        {
            throw new NotImplementedException();
        }
    }
}