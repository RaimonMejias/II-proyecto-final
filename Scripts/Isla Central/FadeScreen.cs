using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour {

    private GameObject canvas_;

    public void Execute(string scene) {
        StartCoroutine(ExecuteTh(scene));
    }

    private IEnumerator ExecuteTh(string scene) {
        canvas_ =  GameObject.FindWithTag("BlackScreen");
        Image blackScreen_ = canvas_.transform.GetChild(0).GetComponent<Image>();
        yield return Fade(true, blackScreen_, 1f, 1.0f);
        SceneManager.LoadScene(scene);
        yield return Fade(false, blackScreen_, 5f, 0.0f);
    }

    public IEnumerator Fade(bool fadeIn, Image screen, float duration, float newAlpha) {
        Color screenColor = screen.color;
        screen.color = new Color(screenColor.r, screenColor.g, screenColor.b, (fadeIn)? 0.0f : 1.0f);
        float timer = 0.0f;
        float alphaValue = screen.color.a;
        while (timer < duration) {
            timer += Time.deltaTime;
            screen.color = new Color(screenColor.r, screenColor.g, screenColor.b, Mathf.Lerp(alphaValue, newAlpha, timer / duration));
            yield return null;
        }
        yield break;
    }
}
