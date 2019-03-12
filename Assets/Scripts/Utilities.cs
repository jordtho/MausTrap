using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Utilities : Singleton<Utilities> {

    protected Utilities() { }

    public IEnumerator WaitForAnimation(Animation animation) {
        do {
            yield return null;
        } while(animation.isPlaying);
    }

}