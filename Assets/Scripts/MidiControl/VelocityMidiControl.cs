using System;
using UnityEngine;

[Serializable]
public class VelocityMidiControl : MidiControl
{
    [Header("VelocityMidiControl variables")]
    [Range(0f, 1f)] public float triggerThreshold = 1;
    [Range(0f, 1f)] public float triggerCooldown = 0.2f;

    private Vector3 lastFramePos;
    private float triggerTimer;
    
    protected override float GetMinInputValue()
    {
        return 0;
    }

    public override void OnUpdate(BodyToMidiController controller)
    {
        float acceleration = UpdateRawInputValue();
        if (acceleration > triggerThreshold && triggerTimer <= 0)
        {
            controller.TriggerNote(midiCC, 40);
            triggerTimer = triggerCooldown;
        }
        
        triggerTimer -= Time.deltaTime;
    }

    protected override float GetMaxInputValue(BodyToMidiController controller)
    {
        return 1.337f;
    }

    public override float UpdateRawInputValue()
    {
        Vector3 thisFramePos = joint.position;
        float acceleration = Vector3.Distance(lastFramePos, thisFramePos);
        lastFramePos = thisFramePos;

        return acceleration;
    }
}