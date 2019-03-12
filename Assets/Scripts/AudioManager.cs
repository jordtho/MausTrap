using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager> {
	
    protected AudioManager() { }

    public AudioSource m_AudioSource;

    //Sound Effects

    public AudioClip m_OpenChestSoundEffect;
    public AudioClip m_ItemGetSoundEffect;
}
