﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
/// <summary>
/// Menu manager (Mostly button interaction)
/// </summary>
public class MenuManager : MonoBehaviour
{
    public enum section { Main, Levels, Settings, Scores};

    [SerializeField] private GameObject mainMenu, settings, levels, blockScreen, scores;
    [SerializeField] private GameObject level_1, level_2, level_3, level_4, level_boss;
    [SerializeField] private GameObject level_2D, level_3D, level_4D, level_bossD;
    [SerializeField] private TextMeshProUGUI score1, score2, score3, score4, score5;
    [SerializeField] private TextMeshProUGUI sectionIndicator;
    [SerializeField] private Button babyDif, normalDif, hardDif, impossibleDif;
    [SerializeField] private SpriteRenderer fadeScreen;

    [SerializeField] private Color selectedColorDif;
    [SerializeField] private float fadeSpeed = 0.05f;


    private Color transparent = new Color(0, 0, 0, 0);
    private section currentSection;
    private bool canClick = true;
    private int maxLevel;
    private float difficulty;
    private LevelGet levelGetScript;


    private void Awake() {
        Time.timeScale = 1;
        levelGetScript = GetComponent<LevelGet>();
    }

    void Start()
    {
        StartCoroutine(FadeOut());
        maxLevel = levelGetScript.Level;
        currentSection = section.Main;
        ActivateLevels();
        XMLSave.instance.Load();
        
    }


    void Update()
    {
        maxLevel = levelGetScript.Level;
        difficulty = XMLSave.instance.dataBase.firstDB[7].value;

        switch (difficulty) {
            case 0:
                ColorBlock cb1 = babyDif.colors;
                cb1.normalColor = selectedColorDif;
                babyDif.colors = cb1;

                break;
            case 1:
                ColorBlock cb2 = normalDif.colors;
                cb2.normalColor = selectedColorDif;
                normalDif.colors = cb2;
                break;
            case 2:
                ColorBlock cb3 = hardDif.colors;
                cb3.normalColor = selectedColorDif;
                hardDif.colors = cb3;
                break;
            case 3:
                ColorBlock cb4 = impossibleDif.colors;
                cb4.normalColor = selectedColorDif;
                impossibleDif.colors = cb4;
                break;
        }
        SetScores();
    }

    public void Goto_Levels() {
        StartCoroutine(ChangeScene(section.Levels));
    }

    public void Goto_Settings() {
        StartCoroutine(ChangeScene(section.Settings));
    }

    public void Return_MainMenu() {
        StartCoroutine(ChangeScene(section.Main));
    }

    public void Goto_Scores() {
        StartCoroutine(ChangeScene(section.Scores));
    }

    public void LoadLevel1() {
        ClickEffect();
        StartCoroutine(FadeInAndLoad("Level1"));
    }
    public void LoadLevel2() {
        ClickEffect();
        StartCoroutine(FadeInAndLoad("Level2"));

    }
    public void LoadLevel3() {
        ClickEffect();
        StartCoroutine(FadeInAndLoad("Level3"));

    }
    public void LoadLevel4() {
        ClickEffect();
        StartCoroutine(FadeInAndLoad("Level4"));

    }
    public void LoadLevel5() {
        ClickEffect();
        StartCoroutine(FadeInAndLoad("BossScene"));

    }


    IEnumerator ChangeScene(section section) {
        ClickEffect();

        blockScreen.SetActive(true);
        for(float i = 0; i < 1.1f; i += 0.05f) {
            fadeScreen.color = Color.Lerp(transparent, Color.black, i);
            yield return new WaitForSeconds(fadeSpeed);
        }
        ChangeToSection(section);
        setIndicatorText();
        
        for (float i = 0; i < 1.1f; i += 0.05f) {
            fadeScreen.color = Color.Lerp(Color.black, transparent, i);
            yield return new WaitForSeconds(fadeSpeed);
            if(i > 0.8f) {
                blockScreen.SetActive(false);
            }
        }
    }

    void ChangeToSection(section section) {
        switch (section) {
            case section.Main:
                mainMenu.SetActive(true);
                settings.SetActive(false);
                levels.SetActive(false);
                scores.SetActive(false);
                currentSection = section.Main;
                break;
            case section.Levels:
                mainMenu.SetActive(false);
                settings.SetActive(false);
                levels.SetActive(true);
                scores.SetActive(false);
                currentSection = section.Levels;
                break;
            case section.Settings:
                mainMenu.SetActive(false);
                settings.SetActive(true);
                levels.SetActive(false);
                scores.SetActive(false);

                currentSection = section.Settings;
                break;
            case section.Scores:
                mainMenu.SetActive(false);
                settings.SetActive(false);
                levels.SetActive(false);
                scores.SetActive(true);
                currentSection = section.Scores;
                break;
        }
    }

    #region enable_levels
    void ActivateLevels() {
        
        switch (maxLevel) {
            case 0:
                level_1.SetActive(true);
                level_2.SetActive(false);
                level_3.SetActive(false);
                level_4.SetActive(false);
                level_boss.SetActive(false);

                level_2D.SetActive(true);
                level_3D.SetActive(true);
                level_4D.SetActive(true);
                level_bossD.SetActive(true);
                break;
            case 1:
                level_1.SetActive(true);
                level_2.SetActive(true);
                level_3.SetActive(false);
                level_4.SetActive(false);
                level_boss.SetActive(false);

                level_2D.SetActive(false);
                level_3D.SetActive(true);
                level_4D.SetActive(true);
                level_bossD.SetActive(true);
                break;
            case 2:
                level_1.SetActive(true);
                level_2.SetActive(true);
                level_3.SetActive(true);
                level_4.SetActive(false);
                level_boss.SetActive(false);

                level_2D.SetActive(false);
                level_3D.SetActive(false);
                level_4D.SetActive(true);
                level_bossD.SetActive(true);
                break;
            case 3:
                level_1.SetActive(true);
                level_2.SetActive(true);
                level_3.SetActive(true);
                level_4.SetActive(true);
                level_boss.SetActive(false);

                level_2D.SetActive(false);
                level_3D.SetActive(false);
                level_4D.SetActive(false);
                level_bossD.SetActive(true);
                break;
            case 4:
                level_1.SetActive(true);
                level_2.SetActive(true);
                level_3.SetActive(true);
                level_4.SetActive(true);
                level_boss.SetActive(true);

                level_2D.SetActive(false);
                level_3D.SetActive(false);
                level_4D.SetActive(false);
                level_bossD.SetActive(false);
                break;
        }
    }
    #endregion

    void setIndicatorText() {
        switch (currentSection) {
            case section.Main:
                sectionIndicator.text = "Main Menu";
                break;
            case section.Settings:
                sectionIndicator.text = "Settings";
                break;
            case section.Levels:
                sectionIndicator.text = "Levels";
                break;
            case section.Scores:
                sectionIndicator.text = "Scores";
                break;
        }
    }

    void ClickEffect() {
        SoundManager.instance.Play(SoundManager.clip.ButtonClick);
        if (!Application.isEditor) {
            VibrationController.Vibrate(50);
        }
    }

    void SetScores() {
        score1.text = XMLSave.instance.dataBase.firstDB[2].value.ToString();
        score2.text = XMLSave.instance.dataBase.firstDB[3].value.ToString();
        score3.text = XMLSave.instance.dataBase.firstDB[4].value.ToString();
        score4.text = XMLSave.instance.dataBase.firstDB[5].value.ToString();
        score5.text = XMLSave.instance.dataBase.firstDB[6].value.ToString();

    }

    #region difficulty_functions
    public void SetBaby() {
        XMLSave.instance.Load();
        XMLSave.instance.dataBase.firstDB[7].value = 0;
        XMLSave.instance.Save();
        XMLSave.instance.Load();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    public void SetNormal() {
        XMLSave.instance.Load();
        XMLSave.instance.dataBase.firstDB[7].value = 1;
        XMLSave.instance.Save();
        XMLSave.instance.Load();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    public void SetHard() {
        XMLSave.instance.Load();
        XMLSave.instance.dataBase.firstDB[7].value = 2;
        XMLSave.instance.Save();
        XMLSave.instance.Load();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    public void SetImpossible() {
        XMLSave.instance.Load();
        XMLSave.instance.dataBase.firstDB[7].value = 3;
        XMLSave.instance.Save();
        XMLSave.instance.Load();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion

    IEnumerator FadeOut() {
        for (float i = 0; i < 1.1f; i += 0.05f) {
            fadeScreen.color = Color.Lerp(Color.black, transparent, i);
            yield return new WaitForSeconds(fadeSpeed);
        }
    }

    IEnumerator FadeInAndLoad(string level) {
        for (float i = 0; i < 1.1f; i += 0.05f) {
            fadeScreen.color = Color.Lerp(transparent, Color.black, i);
            yield return new WaitForSeconds(fadeSpeed);
        }
        SceneManager.LoadScene(level);
    }
}
