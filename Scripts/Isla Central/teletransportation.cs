//-----------------------------------------------------------------------
// <copyright file="ObjectController.cs" company="Google LLC">
// Copyright 2020 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum Scenes {
    HUB = 0, 
    MundoCien = 1,
    MundoPirata = 2,
    MundoVaporwave = 3
}

public class teletransportation : MonoBehaviour {

    void OnEnable() {
        sceneName_ = SceneManager.GetActiveScene().name;
        switch (sceneName_) {
            case "HUB":
                break;
            case "Mundo_pirata":
                GameObject.FindGameObjectWithTag("TreasureCollider").GetComponent<TreasureMovement>().TreasureOpenEnvent += CanExit;
                Debug.Log("TreasureCollider");
                break;
            case "Vaporwave":
                break;
            case "Mundo_Cientifico":
                break;
            default:
                Debug.Log("No scene found");
                break;
                }
    }   

    [Header("BlackScreen")]
    public GameObject canvas_;
    
    [Header("Scenes Configuration")]
    public Scenes scenes_;
    string sceneName_;
    private Dictionary<Scenes, string> stringScenes_;
    private bool exit_;


    public void Start() {
        exit_ = false;
        canvas_ = GameObject.FindWithTag("BlackScreen");
        stringScenes_ = new Dictionary<Scenes, string>();
        stringScenes_.Add(Scenes.HUB, "HUB");
        stringScenes_.Add(Scenes.MundoCien, "Mundo_Cientifico");
        stringScenes_.Add(Scenes.MundoPirata, "Mundo_Pirata");
        stringScenes_.Add(Scenes.MundoVaporwave, "Vaporwave");
    }


    public void OnPointerEnter() {
        // Poner aquí lo de esperar 5 segundos
        Debug.Log(sceneName_);
        if (ReturnHub()) {
            exit_ = false;
            string sceneName_ = SceneManager.GetActiveScene().name;
        } else if (string.Equals(sceneName_, "HUB")){
            canvas_.GetComponent<FadeScreen>();
            canvas_.GetComponent<FadeScreen>().Execute(stringScenes_[scenes_]);
            string sceneName_ = SceneManager.GetActiveScene().name;
        }
        
    }

    bool ReturnHub() {
        if (exit_) {
            StartCoroutine(Wait());
            canvas_.GetComponent<FadeScreen>();
            canvas_.GetComponent<FadeScreen>().Execute(stringScenes_[0]);
            return true;
        }
        return false;
    }

    void CanExit() {
        exit_ = true;
    }

    private IEnumerator Wait() {
        yield return new WaitForSeconds(10);
    }

    

    public void OnPointerExit() {}
    public void OnPointerClick() {}

}
