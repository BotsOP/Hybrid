using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class DistortionManager : MonoBehaviour
{
    [SerializeField] private VisualEffect vfxPlayer1;
    [SerializeField] private VisualEffect vfxPlayer2;
    [SerializeField] private Transform chest1;
    [SerializeField] private Transform chest2;
    [SerializeField] private float distTillCalm;
    [SerializeField] private float maxRadius;
    [SerializeField] private float minRadius;
    public bool reachedCalm;

    // private void OnDrawGizmos()
    // {
    //     vfxPlayer1.SetVector3("distortPos", transform.position);
    //     vfxPlayer1.SetFloat("distortRadius", transform.lossyScale.x);
    //     vfxPlayer2.SetVector3("distortPos", transform.position);
    //     vfxPlayer2.SetFloat("distortRadius", transform.lossyScale.x);
    // }
    void Start()
    {
    }

    void Update()
    {
        if (!reachedCalm)
        {
            Vector3 avgChestPos = (chest1.position + chest2.position) / 2;
            float chestDist = Vector3.Distance(chest1.position, chest2.position);
            chestDist = Remap(chestDist, 0, 5, 1, 0);
            chestDist *= chestDist;
            Debug.Log(chestDist);
            float distortRadius = Mathf.Lerp(minRadius, maxRadius, chestDist);
            transform.localScale = new Vector3(distortRadius, distortRadius, distortRadius);
            vfxPlayer1.SetVector3("distortPos", avgChestPos);
            vfxPlayer1.SetFloat("distortRadius", distortRadius);
            vfxPlayer2.SetVector3("distortPos", avgChestPos);
            vfxPlayer2.SetFloat("distortRadius", distortRadius);
            
            if (Vector3.Distance(chest1.position, chest2.position) < distTillCalm)
            {
                vfxPlayer1.SetFloat("distortRadius", 0);
                vfxPlayer2.SetFloat("distortRadius", 0);
                reachedCalm = true;
            }
        }
    }
    
    private  float Remap (float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
