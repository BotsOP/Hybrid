using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] private Transform ball1;
    [SerializeField] private Vector3 extraRot;

    private void OnDrawGizmos()
    {
        Vector3 cameraDir = ball1.transform.forward;
        transform.LookAt(ball1.position);
        transform.Rotate(90, 90, 90);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"hoi");
        Vector3 cameraDir = ball1.transform.forward;
        transform.LookAt(ball1.position);
        transform.Rotate(90, 90, 90);
    }
}
