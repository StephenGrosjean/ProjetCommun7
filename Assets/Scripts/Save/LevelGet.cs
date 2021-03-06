﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
/// <summary>
/// Get level value for menu unlocking
/// </summary>
public class LevelGet : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private XMLSave saveScript;
    [SerializeField] private VolumeSlider slider;

    private int level;
    public int Level {
        get { return level; }
    }

    void Awake() {
        saveScript.Load();

        foreach (FirstDataset dataset in saveScript.dataBase.firstDB) {
            if (dataset.name == "Level") {
                level = (int)dataset.value;
            }
        }
        text.text = "Level : " + level.ToString();

    }

    public void ResetSettings() {
        SoundManager.instance.Play(SoundManager.clip.ButtonClick);
        saveScript.dataBase.firstDB[1].value = 1;
        saveScript.Save();
        slider.ResetSlider();
    }

    public void ResetGame() {
        SoundManager.instance.Play(SoundManager.clip.ButtonClick);
        saveScript.dataBase.firstDB[0].value = 0;
        saveScript.dataBase.firstDB[2].value = 0;
        saveScript.dataBase.firstDB[3].value = 0;
        saveScript.dataBase.firstDB[4].value = 0;
        saveScript.dataBase.firstDB[5].value = 0;
        saveScript.dataBase.firstDB[6].value = 0;
        saveScript.dataBase.firstDB[8].value = 0;
        saveScript.dataBase.firstDB[9].value = 1;
        saveScript.dataBase.firstDB[10].value = 0;
        saveScript.dataBase.firstDB[11].value = 0;
        saveScript.Save();

        SceneManager.LoadScene("MainMenu");
    }

}


