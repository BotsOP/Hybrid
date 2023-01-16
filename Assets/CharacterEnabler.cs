using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEnabler : MonoBehaviour
{
    [SerializeField] KinectManager KM;
    [SerializeField] GameObject p1;
    [SerializeField] GameObject p2;

    void Update() {
        CharacterActivation();
    }

    void CharacterActivation() {
        if(KM.IsUserDetected (0)) {
            if(!p1.activeSelf)p1.SetActive(true);
        } else {
            if(p1.activeSelf)p1.SetActive(false);
        }
        if(KM.IsUserDetected (1)) {
            if(!p2.activeSelf)p2.SetActive(true);
        } else {
            if(p2.activeSelf)p2.SetActive(false);
        }
    }
}
