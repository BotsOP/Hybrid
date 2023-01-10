using System;
using System.Collections;
using System.Linq;
using System.Text;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Composing;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Multimedia;
using Melanchall.DryWetMidi.Standards;
using UnityEngine;


// ...
public class MidiOutput : MonoBehaviour
{
    private const string OutputDeviceName = "Unity Midi Receiver";
    private OutputDevice _outputDevice;

    public int midiPort = 33;

    public delegate MidiEvent EventCallback(MidiEvent rawEvent, long rawTime, TimeSpan playbackTime);


    void Start()
    {
        // LogAllDevices();

        InitializeOutputDevice();

    }

    void LogAllDevices() {
        foreach (var outputDevice in OutputDevice.GetAll())
        {
            Debug.Log(outputDevice.Name);
        }
    }

    private void InitializeOutputDevice()
    {
        Debug.Log($"Initializing output device [{OutputDeviceName}]...");

        var allOutputDevices = OutputDevice.GetAll();
        if (!allOutputDevices.Any(d => d.Name == OutputDeviceName))
        {
            var allDevicesList = string.Join(Environment.NewLine, allOutputDevices.Select(d => $"  {d.Name}"));
            Debug.Log($"There is no [{OutputDeviceName}] device presented in the system. Here the list of all device:{Environment.NewLine}{allDevicesList}");
            return;
        }

        _outputDevice = OutputDevice.GetByName(OutputDeviceName);
        Debug.Log($"Output device [{OutputDeviceName}] initialized.");
    }

    public void SendNote(int note) {
        _outputDevice.SendEvent(new NoteOnEvent((SevenBitNumber)note, SevenBitNumber.MaxValue));
        Debug.Log("Note " + note + " sent.");
    }

    public void TriggerNote(int note, int velocity = 127)
    {
        StartCoroutine(_TriggerNote(note, velocity));
    }

    private IEnumerator _TriggerNote(int note, int velocity)
    {
        NoteOnEvent noteOnEvent = new NoteOnEvent((SevenBitNumber) 40, (SevenBitNumber) 127);
        noteOnEvent.Channel = (FourBitNumber) 1;
        _outputDevice.SendEvent(noteOnEvent);

        yield return new WaitForSeconds(0.02f);
        
        NoteOffEvent noteOffEvent = new NoteOffEvent((SevenBitNumber) 40, (SevenBitNumber) 127);
        noteOffEvent.Channel = (FourBitNumber) 1;
        _outputDevice.SendEvent(noteOffEvent);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NoteOnEvent noteOnEvent = new NoteOnEvent((SevenBitNumber) 40, SevenBitNumber.MaxValue);
            noteOnEvent.Channel = (FourBitNumber) 1;
            
            _outputDevice.SendEvent(noteOnEvent);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            NoteOffEvent noteOffEvent = new NoteOffEvent((SevenBitNumber) 40, SevenBitNumber.MaxValue);
            noteOffEvent.Channel = (FourBitNumber) 1;
            
            _outputDevice.SendEvent(noteOffEvent);
        }
    }
    
    public void SendKnobValue(int midiCC, float value)
    {
        ControlChangeEvent midiEvent = new ControlChangeEvent((SevenBitNumber)midiCC, (SevenBitNumber)(value * 127));
        _outputDevice.SendEvent(midiEvent);
    }

    private void TurnAllNotesOff() {
        _outputDevice.TurnAllNotesOff(); 
    }

    private void OnDestroy()
    {
        TurnAllNotesOff();
    }
}
