using System;
using UnityEngine;

[Serializable]
public class RelativePosMidiControl : PosMidiControl
{
    [Header("RelativePosMidiControl variables")]
    public Transform root;

    public override void OnStart(BodyToMidiController controller)
    {
        if (root == null) Debug.LogError("Missing root transform reference!");
    }

    protected override float UpdateRawInputValue()
    {
        Vector3 relativePos = joint.transform.position - root.transform.position;

        return dimension switch
        {
            Dimension.X => relativePos.x,
            Dimension.Y => relativePos.y,
            Dimension.Z => relativePos.z,
            _ => -1
        };
    }
}