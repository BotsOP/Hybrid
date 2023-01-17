using System;
using UnityEngine;

[Serializable]
public class ProximityMidiControl : MidiControl
{
    [Header("ProximityMidiControl")]
    public Transform root;
    [Range(0, 10)] public float maxDistance;

    public override void OnStart(BodyToMidiController controller)
    {
        base.OnStart(controller);
        if (root == null) Debug.LogError("Missing root transform reference on CC#" + midiCC);
    }

    protected override float GetMinInputValue()
    {
        return maxDistance;
    }

    protected override float GetMaxInputValue(BodyToMidiController controller)
    {
        return 0;
    }

    public override float UpdateRawInputValue()
    {
        if (root) return Vector3.Distance(joint.transform.position, root.transform.position);

        return maxDistance;
    }
}