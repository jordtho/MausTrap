using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    List<Animator> m_Hearts = new List<Animator>();
    float m_HeartFillRate = 0.1f;

	void Awake () {

        m_Hearts.Clear();
        foreach(Animator _Animator in GetComponentsInChildren<Animator>()) {
            m_Hearts.Add(_Animator);
            //_Animator.SetInteger("state", 2);
        }
	}
	
    public void SetMaximumHealth(int value) {

    }

    public void SetCurrentHealth(int value) {
        //for(int i = 0; i < m_HeartImages.Count; ++i) {
        //    m_HeartImages[i].enabled = i < value ? true : false;
        //}

        StartCoroutine(IFillHearts(value));
    }

    IEnumerator IFillHearts(int health) {

        for(int i = 0; i < m_Hearts.Count; ++i) {

            if(i < health) {

                if(m_Hearts[i].GetInteger("state") == 2) { yield return null; }
                else if(m_Hearts[i].GetInteger("state") == 1) {
                    if(i + 1 < m_Hearts.Count) { m_Hearts[i + 1].SetInteger("state", 1); }
                    m_Hearts[i].SetInteger("state", 2);
                    yield return new WaitForSeconds(m_HeartFillRate);
                }
                else if(m_Hearts[i].GetInteger("state") == 0) {
                    m_Hearts[i].SetInteger("state", 2);
                    yield return new WaitForSeconds(m_HeartFillRate);
                }
            }
            else {

                m_Hearts[i].SetInteger("state", 0);
            }
        }
    }

    //IEnumerator IFillHearts(int health) {

    //    Animator[] _Hearts = GetComponentsInChildren<Animator>(true);
    //    int _i = 0;

    //    foreach(var _Heart in _Hearts) { if(_Heart.GetInteger("state") != 0) { ++_i; } }
    //    if(_i == GetComponentsInChildren<Animator>().Length || health <= 0) { yield return null; }


    //    for(int i = _i - 1; i < _i - 1 + health; ++i) {

    //        if(i >= _Hearts.Length) { break; }

    //        if(_Hearts[i].GetInteger("state") == 1) { if(i < _Hearts.Length - 1) { _Hearts[i + 1].SetInteger("state", 1); } }
    //        _Hearts[i].SetInteger("state", 0);

    //        yield return new WaitForSeconds(m_HeartFillRate);
    //    }
    //}
}