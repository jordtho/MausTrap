using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class HealthBarHUDComponent : MonoBehaviour
    {
        public List<HeartHUDComponent> _hearts = new List<HeartHUDComponent>();

        private IEnumerator FillCoroutine;

        public void SetMaximumHealth(int value) => throw new NotImplementedException();

        public void SetCurrentHealth(int value)
        {
            for (int i = 0; i < _hearts.Count; i++)
            {
                if (i < value)
                {
                    _hearts[i].SetToFull();
                }
                else
                {
                    _hearts[i].SetToEmpty();
                }
            }
        }
    }
}