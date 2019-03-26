using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class ItemComponent : MonoBehaviour
    {
        public int _quantity;
        public bool _consumable;
        //public float _cooldown;

        public Animator Animator { get; set; }
        public SpriteRenderer SpriteRenderer { get; set; }

        void Awake()
        {
            Animator = GetComponent<Animator>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void ItemGetAnimation() => StartCoroutine(IItemGetAnimation());

        private IEnumerator IItemGetAnimation()
        {
            Animator.SetInteger("state", 1);
            SpriteRenderer.enabled = true;
            yield return new WaitForSeconds(1.5f);
            SpriteRenderer.enabled = false;
        }
    }
}
