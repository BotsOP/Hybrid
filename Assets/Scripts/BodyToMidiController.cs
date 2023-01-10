using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BodyToMidiController : MonoBehaviour
{
    [Header("References")]
    public MidiOutput midiManager;

    [Header("Runtime variables")]
    public bool sendMidiMessages;

    [Header("MidiControls")]
    public PosMidiControl[] posMidiCtrls;
    public RelativePosMidiControl[] relativePosMidiCtrls;
    public ProximityMidiControl[] proxMidiCtrls;
    public VelocityMidiControl[] velocityMidiControls;
    private List<MidiControl> allCtrls;

    private void Start()
    {
        allCtrls = new List<MidiControl>();
        allCtrls.AddRange(posMidiCtrls);
        allCtrls.AddRange(relativePosMidiCtrls);
        allCtrls.AddRange(proxMidiCtrls);
        allCtrls.AddRange(velocityMidiControls);

        foreach (MidiControl ctrl in allCtrls)
        {
            ctrl.OnStart(this);
        }
    }

    void Update()
    {
        if (sendMidiMessages)
        {
            foreach (MidiControl ctrl in allCtrls)
            {
                ctrl.OnUpdate(this);
            }
        }
    }

    public void SendKnobValue(int midiCC, float value)
    {
        midiManager.SendKnobValue(midiCC, value);
    }

    public void TriggerNote(int midiCC, int note)
    {
        midiManager.TriggerNote(midiCC, note);
    }
}