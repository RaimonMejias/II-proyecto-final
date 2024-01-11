using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceGame : MonoBehaviour {

    [Header("Grid Configuration")]
    public List<Platform> buttons_;
    private Platform[][] grid_;
    private int sizeI_ = 6;
    private int sizeJ_ = 5;

    [Header("Ducks Configuration")]
    private GameObject pinkDuckStartingPoint_;
    private GameObject grayDuckStartingPoint_;
    private GameObject blueDuckStartingPoint_;

    [Header("Neighborhood Configuration")]
    private int[][] neighborhood_;
    private int hoodSizeI_ = 4;
    private int hoodSizeJ_ = 2;

    [Header("Finish signs")]
    public Material winSignMaterial_;
    public Material failSignMaterial_;
    private GameObject sign_;

    [Header("Game Status")]
    public bool isWon_ = false; 
    public bool isGameover_ = false;

    private void Start() {
        CreateGrid();
        SetPlatforms();
        SetNeighbors();
        sign_ = transform.GetChild(transform.childCount - 1).gameObject;
        sign_.SetActive(false);
        sign_.transform.localRotation = Quaternion.Euler(180, 0, 0);
        sign_.transform.localScale = new Vector3(2, 2, 2);
        pinkDuckStartingPoint_ = GameObject.FindWithTag("PinkDuck").transform.parent.gameObject;
        grayDuckStartingPoint_ = GameObject.FindWithTag("GrayDuck").transform.parent.gameObject;
        blueDuckStartingPoint_ = GameObject.FindWithTag("BlueDuck").transform.parent.gameObject;   
    }

    private void Update() {
        if (buttons_.Count == 0) { return; }
        foreach (Platform button in buttons_) {
            if (!button.isPressed_) { return; }
        }
        GameOver();
    }

    public void CreateGrid() {
        grid_ = new Platform[sizeI_][];
        for (int i = 0; i < sizeI_; i++) {
            grid_[i] = new Platform[sizeJ_];
        }
        neighborhood_ = new int[hoodSizeI_][];
        int[] iValues = new int[4]{-1,  0,  0,  1};
        int[] jValues = new int[4]{ 0, -1,  1,  0};
        for (int i = 0; i < hoodSizeI_; i++) {
            neighborhood_[i] = new int[hoodSizeJ_];
            neighborhood_[i] = new int[2]{iValues[i], jValues[i]};
        }
    }

    public void SetPlatforms() {
        for (int i = 0, count = transform.childCount - 1; i < count; i++) {
            Platform platform = transform.GetChild(i).gameObject.GetComponent<Platform>();
            int iPos = (int)char.GetNumericValue(platform.name[4]);
            int jPos = (int)char.GetNumericValue(platform.name[5]);
            grid_[iPos][jPos] = platform;
        }
    }

    public void SetNeighbors() {
        for (int i = 0; i < sizeI_; i++) {
            for (int j = 0; j < sizeJ_; j++) {
                Platform[] neighbors = new Platform[hoodSizeI_];
                for (int k = 0; k < hoodSizeI_; k++) {
                    try {
                        neighbors[k] = grid_[i + neighborhood_[k][0]][j + neighborhood_[k][1]];
                    } catch(Exception) {
                        neighbors[k] = null;
                    }
                }
                if (grid_[i][j]) { grid_[i][j].neighbors_ = neighbors; }
            }
        }
    }

    public void GameOver(bool failure = false) {
        for (int i = 0; i < sizeI_; i++) {
            for (int j = 0; j < sizeJ_; j++) {
                if (grid_[i][j]) { grid_[i][j].gameObject.SetActive(false); }
            }
        }
        sign_.GetComponent<Renderer>().material = 
                (!failure)? winSignMaterial_ : failSignMaterial_;
        sign_.SetActive(true); // Instantiate((!failure)? winSign_ : failSign_, transform);
        isWon_ = !failure;
        isGameover_ = failure;
    }

    public void Reset() {
        sign_.SetActive(false);
        for (int i = 0; i < sizeI_; i++) {
            for (int j = 0; j < sizeJ_; j++) {
                if (grid_[i][j]) { grid_[i][j].gameObject.SetActive(true); }
            }
        }
        isGameover_ = false;
        isWon_ = false;
        GameObject.FindWithTag("PinkDuck").GetComponent<DuckVoiceResponse>().SetCurentPlatform(pinkDuckStartingPoint_);
        GameObject.FindWithTag("BlueDuck").GetComponent<DuckVoiceResponse>().SetCurentPlatform(blueDuckStartingPoint_);
        GameObject.FindWithTag("GrayDuck").GetComponent<DuckVoiceResponse>().SetCurentPlatform(grayDuckStartingPoint_);
    }

}

