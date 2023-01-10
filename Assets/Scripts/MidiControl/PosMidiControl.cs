using System;
using UnityEngine;

[Serializable]
public class PosMidiControl : MidiControl
{
    public enum Dimension { X, Y, Z }
    
    [Header("PosMidiControl variables")]
    public Dimension dimension;

    [Range(-10, +10)] public float minCoordValue;
    [Range(-10, +10)] public float maxCoordValue;

    protected override float GetMinInputValue()
    {
        return minCoordValue;
    }
    
    protected override float GetMaxInputValue(BodyToMidiController controller)
    {
        return maxCoordValue;
    }

    protected override float UpdateRawInputValue()
    {
        return dimension switch
        {
            Dimension.X => joint.position.x,
            Dimension.Y => joint.position.y,
            Dimension.Z => joint.position.z,
            _ => -1
        };
    }
}