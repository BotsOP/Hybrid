using System;
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

    public void SendKnobValue(int ccNo, float value) {
        ControlChangeEvent midiEvent = new ControlChangeEvent((SevenBitNumber)ccNo, (SevenBitNumber)((int)(value * 127)));
        _outputDevice.SendEvent(midiEvent);

        Debug.Log("Control value " + value + " sent to port " + 33 + ", channel " + midiEvent.Channel + ", CC#" + midiEvent.ControlNumber);
    }

    public void TurnAllNotesOff() {
        _outputDevice.TurnAllNotesOff(); 
    }

}
