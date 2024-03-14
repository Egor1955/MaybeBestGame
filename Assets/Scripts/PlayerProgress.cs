﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerProgress : MonoBehaviour
{
    public RectTransform experienceValueRectTransform;
    public TextMeshProUGUI levelValueTMP;

    private int levelValue = 1;
    private float _experienceCurrentValue = 0;
    private float _experienceTargetValue = 100;

    // Start is called before the first frame update
    void Start()
    {
        DrawUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddExperiens(float value)
    {
        _experienceCurrentValue += value;
        if(_experienceCurrentValue >= _experienceTargetValue)
        {
            levelValue += 1;
            _experienceCurrentValue = 0;
        }
        DrawUI();
    }

    private void DrawUI()
    {
        experienceValueRectTransform.anchorMax = new Vector2(_experienceCurrentValue / _experienceTargetValue, 1);
        levelValueTMP.text = levelValue.ToString();
    }
}
