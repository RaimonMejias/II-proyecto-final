using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class change_skybox : MonoBehaviour {

    [Header("Skybox Configuration")]
    public Material new_skybox;
    private Material old_skybox;
    private bool new_skybox_active = false;
    
    [Header("Water Configuration")]
    private Color old_water;
    private Color old_light;
    private Material water;

    [Header("Background Audio Configuration")]
    AudioSource audio_;

    private void Start() {
       old_skybox = RenderSettings.skybox;
       water = GameObject.FindWithTag("Water").GetComponent<Renderer>().material;
       old_water = water.GetColor("_Color");
       old_light = GameObject.FindWithTag("Light").GetComponent<Light>().color;
       audio_ = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player" && !new_skybox_active){
            RenderSettings.skybox = new_skybox;
            water.SetColor("_Color", Color.black);
            GameObject.FindWithTag("Light").GetComponent<Light>().color = Color.magenta;
            new_skybox_active = true;
            StartCoroutine(Fade(true, audio_, 10.0f, 0f));
        } else {
            RenderSettings.skybox = old_skybox;
            water.SetColor("_Color", old_water);
            GameObject.FindWithTag("Light").GetComponent<Light>().color = old_light;
            new_skybox_active = false;
            StartCoroutine(Fade(false, audio_, 5.0f, 0.0f));
        }
    }

    public IEnumerator Fade(bool fadeIn, AudioSource audio, float duration, float newVolume) {
        if (fadeIn) { 
            audio_.volume = 0.0f;
            audio_.Play();
        }
        float timer = 0.0f;
        float volume = audio.volume;
        while (timer < duration) {
            timer += Time.deltaTime;
            audio.volume = Mathf.Lerp(volume, newVolume, timer / duration);
            yield return null;
        }
        if(!fadeIn) { audio_.Stop(); }
        yield break;
    }
}
