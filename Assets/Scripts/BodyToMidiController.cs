using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BodyToMidiController : MonoBehaviour
{
    [Header("References")]
    public MidiOutput midiManager;
    [SerializeField] private TMP_Text debugTextLabel;

    [Header("Runtime variables")]
    public bool sendMidiMessages;
    public bool enableDebugging;

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
        else if (enableDebugging)
        {
            MidiControl ctrl = allCtrls.FirstOrDefault(c => c.isActive);
            if (ctrl != null) debugTextLabel.text = ctrl.UpdateRawInputValue() + " (" + ctrl.inputDescription + ")";
            else debugTextLabel.text = "No active MidiControl found";
        }
        else debugTextLabel.text = "";
    }

    public void SendKnobValue(int midiCC, float value)
    {
        if (enableDebugging) debugTextLabel.text = "Value [" + value + "] sent to MIDI CC#" + midiCC;
        
        midiManager.SendKnobValue(midiCC, value);
    }

    public void TriggerNote(int midiCC, int note)
    {
        midiManager.TriggerNote(midiCC, note);
    }
}