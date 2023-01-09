using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyToMidiController : MonoBehaviour
{
    [Header("References")]
    public MidiOutput midiManager;

    [Header("Physical room boundaries")]
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float minZ;
    public float maxZ;

    [Header("Body points")]
    public Transform leftHandTx;
    public Transform rightHandTx;
    public Transform headTx;
    public Transform hipTx;

    [Header("Realtime variables")]
    public bool sendMidiMessages;

    void Update()
    {
        if (sendMidiMessages)
        {
            float leftHandX = Util.Remap(leftHandTx.position.x, minX, maxX, 0f, 1f);
            midiManager.SendKnobValue(18, leftHandX);

            float leftHandY = Util.Remap(leftHandTx.position.y, minY, maxY, 0f, 1f);
            midiManager.SendKnobValue(19, leftHandY);

            float leftHandZ = Util.Remap(leftHandTx.position.z, minZ, maxZ, 0f, 1f);
            midiManager.SendKnobValue(20, leftHandZ);
        }
        else
        {
            Debug.Log(leftHandTx.position.x + ", " + leftHandTx.position.y + ", " + leftHandTx.position.z);
        }
    }
}