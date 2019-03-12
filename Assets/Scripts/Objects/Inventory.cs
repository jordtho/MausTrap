using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.DataModels
{
    public class Inventory : MonoBehaviour, IInventory
    {
        private List<IItem> _items;
        public IItem this[int index]
        {
            get { return _items[index]; }
            set { _items[index] = value; }
        }
        public int Count
        {
            get { return _items.Count; }
        }
        public bool IsReadOnly { get; }

        Inventory()
        {
            _items = new List<IItem>();
        }
        Inventory(List<IItem> items)
        {
            _items = items;
        }

        public void Add(IItem item)
        {
            _items.Add(item);
        }
        public void Clear()
        {
            _items.Clear();
        }
        public bool Contains(IItem item)
        {
            return _items.Contains(item);
        }
        public void CopyTo(IItem[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }
        public IEnumerator<IItem> GetEnumerator()
        {
            return _items.GetEnumerator();
        }
        public int IndexOf(IItem item)
        {
            return _items.IndexOf(item);
        }
        public void Insert(int index, IItem item)
        {
            _items.Insert(index, item);
        }
        public bool Remove(IItem item)
        {
            return _items.Remove(item);
        }
        public void RemoveAt(int index)
        {
            _items.RemoveAt(index);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
