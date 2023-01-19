using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class targetMove2 : MonoBehaviour
{
   
    public AnimationCurve curve;
    public Transform target;
    public Transform target2;
    public float speed;
    public Transform lightTrans;
    public Transform lightLookAt;
    public float timeOffset;
    
    void Update()
    {
        float lerpValue = curve.Evaluate((Time.time + timeOffset) / speed % 1);
        transform.position = Vector3.Lerp(target.position, target2.position, lerpValue);
        lightTrans.LookAt(lightLookAt.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(target.position, target2.position);
    }
}
