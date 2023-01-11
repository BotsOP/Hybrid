using System;
using UnityEngine;

[Serializable]
public abstract class MidiControl
{
    [Header("MidiControl")]
    public bool isActive;
    public int midiCC;
    public Transform joint;
    [TextArea] public string inputDescription;
    [TextArea] public string effectDescription;

    protected float _minInputValue;
    protected float _maxInputValue;

    public virtual void OnStart(BodyToMidiController controller)
    {
        _minInputValue = GetMinInputValue();
        _maxInputValue = GetMaxInputValue(controller);
    }

    protected abstract float GetMinInputValue();
    protected abstract float GetMaxInputValue(BodyToMidiController controller);
    
    public virtual void OnUpdate(BodyToMidiController controller)
    {
        if (!isActive) return;
        
        float inputValue = UpdateRawInputValue();
        float mappedValue = Util.Remap(inputValue, GetMinInputValue(), GetMaxInputValue(controller), 0f, 1f);
        
        controller.SendKnobValue(midiCC, mappedValue);
    }

    public abstract float UpdateRawInputValue();
}