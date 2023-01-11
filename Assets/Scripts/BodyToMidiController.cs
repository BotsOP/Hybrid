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

    [Range(1, 60)] public int updatesPerSecond = 10;
    private float _updateInterval;
    private float _updateTimer;

    [Header("MidiControls")]
    public PosMidiControl[] posMidiCtrls;
    public RelativePosMidiControl[] relativePosMidiCtrls;
    public ProximityMidiControl[] proxMidiCtrls;
    public VelocityMidiControl[] velocityMidiControls;
    private List<MidiControl> allCtrls;

    private void Start()
    {
        _updateInterval = 1.0f / updatesPerSecond;
        
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
        if (_updateTimer <= 0)
        {
            _updateTimer = _updateInterval;
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
                if (ctrl != null)
                {
                    debugTextLabel.text = ctrl.UpdateRawInputValue() + " (" + ctrl.inputDescription + " / " + _updateTimer + ")";
                }
                else
                {
                    debugTextLabel.text = "No active MidiControl found";
                }
            }
            
            if (!enableDebugging) debugTextLabel.text = "";
        }
        
        _updateTimer -= Time.deltaTime;
    }

    public void SendKnobValue(int midiCC, float value)
    {
        if (value < 0 && value > 1)
        {
            if (enableDebugging)
            {
                debugTextLabel.text = "Value [" + value + "] sent to MIDI CC#" + midiCC + " - out of bounds!";
                value = Math.Clamp(value, 0, 1);
            }
        }
        else if (enableDebugging) debugTextLabel.text = "Value [" + value + "] sent to MIDI CC#" + midiCC;
        
        midiManager.SendKnobValue(midiCC, value);
    }

    public void TriggerNote(int midiCC, int note)
    {
        midiManager.TriggerNote(midiCC, note);
    }
}