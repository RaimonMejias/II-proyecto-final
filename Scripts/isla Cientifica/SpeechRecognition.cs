using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using HuggingFace.API;
using System.Text.RegularExpressions;

public class SpeechRecognition : MonoBehaviour {

    [Header("Internal Configuration")]
    private AudioClip clip;
    private byte[] bytes;

    [Header("External Configuration")]
    public bool isRecording_ = false;
    public string heared_ = "";

    public delegate void Movement(int direction);
    public event Movement MoveDuck = delegate { };
    
    private void Update() {
        if (Input.GetButtonDown("Fire1")) { 
            StartRecording(); 
        }
        else if (Input.GetButtonDown("Fire2")) { ;
            if (isRecording_) { StopRecording(); }  
        }
        if (isRecording_ && Microphone.GetPosition(null) >= clip.samples) {
            StopRecording();
        }
    }

    private void StartRecording() {
        clip = Microphone.Start(null, false, 10, 44100);
        isRecording_ = true;
    }

    private void StopRecording() {
        var position = Microphone.GetPosition(null);
        Microphone.End(null);
        var samples = new float[position * clip.channels];
        clip.GetData(samples, 0);
        bytes = EncodeAsWAV(samples, clip.frequency, clip.channels);
        isRecording_ = false;
        SendRecording();
    }

    private void SendRecording() {
        HuggingFaceAPI.AutomaticSpeechRecognition(bytes, 
            (response) => {
                if (response.Length == 0) { 
                    heared_ = "¡Habla!";
                    return; 
                }
                response = response.ToLower();
                response =  Regex.Replace(response, @"[^A-Za-z0-9]+", ""); // Eliminar caracteres especiales
                heared_ = response;
                switch(response) {
                    case "up":    MoveDuck(0); break;
                    case "left":  MoveDuck(1); break;
                    case "right": MoveDuck(2); break;
                    case "down":  MoveDuck(3);  Debug.Log("Down"); break;
                    default: break;
                } 
            },
            (error) => {}
        );
    }
    
    private byte[] EncodeAsWAV(float[] samples, int frequency, int channels) {
        using (var memoryStream = new MemoryStream(44 + samples.Length * 2)) {
            using (var writer = new BinaryWriter(memoryStream)) {
                writer.Write("RIFF".ToCharArray());
                writer.Write(36 + samples.Length * 2);
                writer.Write("WAVE".ToCharArray());
                writer.Write("fmt ".ToCharArray());
                writer.Write(16);
                writer.Write((ushort)1);
                writer.Write((ushort)channels);
                writer.Write(frequency);
                writer.Write(frequency * channels * 2);
                writer.Write((ushort)(channels * 2));
                writer.Write((ushort)16);
                writer.Write("data".ToCharArray());
                writer.Write(samples.Length * 2);
                foreach (var sample in samples) {
                    writer.Write((short)(sample * short.MaxValue));
                }
            }
            return memoryStream.ToArray();
        }
    }
}
