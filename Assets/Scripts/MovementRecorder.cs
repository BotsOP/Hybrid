using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRecorder : MonoBehaviour
{
    [SerializeField]
    private bool playback;
    [SerializeField]
    private int amountFramesToBeRecorded = 30;
    [SerializeField]
    private Transform[] transforms;
    private Transformation[,] transformsRecording;
    private int lastFrameRecorded;

    private void Start()
    {
        transformsRecording = new Transformation[amountFramesToBeRecorded, transforms.Length];
    }
    private void Update()
    {
        if (playback)
        {
            int playbackFrame = Time.frameCount - lastFrameRecorded;
            for (int i = 0; i < transforms.Length; i++)
            {
                transforms[i].position = transformsRecording[playbackFrame, i].position;
                transforms[i].rotation = transformsRecording[playbackFrame, i].rotation;
                transforms[i].localScale = transformsRecording[playbackFrame, i].scale;
            }
            return;
        }
        
        for (int i = 0; i < transforms.Length; i++)
        {
            Transformation trans = new Transformation(transforms[i].position, transforms[i].rotation, transforms[i].localScale);
            transformsRecording[Time.frameCount, i] = trans;
        }
        lastFrameRecorded = Time.frameCount;
    }
}

struct Transformation
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
    public Transformation(Vector3 position, Quaternion rotation, Vector3 scale)
    {
        this.position = position;
        this.rotation = rotation;
        this.scale = scale;
    }
}

