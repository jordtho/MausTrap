using Assets.Scripts.Interfaces;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.DataModels
{
    public abstract class Item : MonoBehaviour, IItem
    {
        public string Name { get; set; }

        public void Use() => StartCoroutine(UseCoroutine());

        protected abstract IEnumerator UseCoroutine();
    }
}
