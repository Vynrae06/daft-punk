﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.EventSystems.StandaloneInputModule;

public class AcessCode : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] symbols;
    [SerializeField] TextMeshProUGUI[] dashes;
    [SerializeField] Color selectedColor;
    [SerializeField] Color unSelectedColor;
    [SerializeField] GameObject SceneM;
    [SerializeField] TextMeshProUGUI message;
    [SerializeField] float loadMainMenuDelay;
    [SerializeField] Slider loadMainMenuSlider;

    int currentSymbol = 0;

    string correctCode = "A▲B►X◄";
    string inputCode = string.Empty;

    bool loadingMainMenu;

    // Start is called before the first frame update
    void Start()
    {
        UpdateCurrentSelectedColor();
    }

    // ► ▼ ▲ ◄
    void Update()
    {
        CodeInput();
        CheckCode();
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ResetCode();
        }
    }

    void ResetCode()
    {
        foreach (TextMeshProUGUI symbol in symbols)
        {
            symbol.text = string.Empty;
        }
        currentSymbol = 0;
        UpdateCurrentSelectedColor();
        inputCode = string.Empty;
    }

    private void CodeInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
            UpdateText("A");
        else if (Input.GetKeyDown(KeyCode.B))
            UpdateText("B");
        else if (Input.GetKeyDown(KeyCode.X))
            UpdateText("X");
        else if (Input.GetKeyDown(KeyCode.Y))
            UpdateText("Y");
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            UpdateText("◄");
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            UpdateText("►");
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            UpdateText("▲");
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            UpdateText("▼");
    }

    void UpdateText(string code)
    {
        symbols[currentSymbol].text = code;
        currentSymbol++;
        inputCode += code;
        UpdateCurrentSelectedColor();
        if (currentSymbol == 1)
        {
            message.color = Color.white;
            message.text = string.Empty;
        }
    }

    private void CheckCode()
    {
        if (currentSymbol == symbols.Length)
        {
            if (string.Equals(inputCode, correctCode))
            {
                message.text = "BON CODE!\nAPPUYEZ SUR LE BUZZER!";
                StartCoroutine(LoadMainMenu());
            }
            else
            {
                ResetCode();
                message.text = "MAUVAIS CODE!";
                message.color = Color.red;
            }
        }
    }

    void UpdateCurrentSelectedColor()
    {
        if (currentSymbol != symbols.Length)
        {
            symbols[currentSymbol].color = selectedColor;
            dashes[currentSymbol].color = selectedColor;
        }

        for (int i = 0; i < symbols.Length; i++)
        {
            if (i != currentSymbol)
            {
                symbols[i].color = unSelectedColor;
                dashes[i].color = unSelectedColor;
            }
        }
    }

    IEnumerator LoadMainMenu()
    {
        loadMainMenuSlider.gameObject.SetActive(true);

        float time = 0;
        while (time < loadMainMenuDelay)
        {
            loadMainMenuSlider.value = Mathf.Lerp(0, 1, time / loadMainMenuDelay);
            time += Time.deltaTime;
            yield return null;
        }

        SceneM.transform.GetComponent<Scenes>().LoadMainMenu();
    }
}