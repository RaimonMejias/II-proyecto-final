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

public enum ourScenes {
    HUB = 0, 
    MundoCien = 1,
    MundoPirata = 2,
    MundoVaporwave = 3
}

public class Teletransportación : MonoBehaviour {
    private bool lookingAt = false;

    [Header("BlackScreen")]
    public GameObject canvas_;
    
    [Header("ourScenes Configuration")]
    public ourScenes scenes_;
    private Dictionary<ourScenes, string> stringScenes_;

    public void Start() {
        canvas_ = GameObject.FindWithTag("BlackScreen");
        stringScenes_ = new Dictionary<ourScenes, string>();
        stringScenes_.Add(ourScenes.HUB, "HUB");
        stringScenes_.Add(ourScenes.MundoCien, "Mundo_Cientifico");
        stringScenes_.Add(ourScenes.MundoPirata, "Mundo_Pirata");
        stringScenes_.Add(ourScenes.MundoVaporwave, "Vaporwave");
    }

    void Update() {
        if (lookingAt && Input.GetButtonDown("Submit")) {
            Debug.Log("Teletransportando");
            Debug.Log(stringScenes_[scenes_]);
            canvas_.GetComponent<FadeScreen>().Execute(stringScenes_[scenes_]);
        }
    } 

    public void OnPointerEnter() {
        lookingAt = true;
    }

    public void OnPointerExit() {
        lookingAt = false;
    }

    public void OnPointerClick() {}

}
