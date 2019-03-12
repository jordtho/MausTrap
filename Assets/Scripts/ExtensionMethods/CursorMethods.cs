using Assets.Scripts.Interfaces;
using Assets.Scripts.Objects;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ExtensionMethods
{
    public static class CursorMethods
    {
        public static IEnumerator ICursorFlicker(this MenuCursor cursor)
        {
            float _time = 0f;
            cursor.GetComponentInChildren<Image>().enabled = true;

            while (cursor.gameObject.activeSelf)
            {
                _time += Time.deltaTime;
                if (_time > cursor.FlickerRate)
                {
                    cursor.GetComponentInChildren<Image>().enabled = !cursor.GetComponentInChildren<Image>().enabled;
                    _time = 0f;
                }
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
