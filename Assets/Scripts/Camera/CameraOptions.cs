using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraOptions : MonoBehaviour
{

    public float maxVertSens = 1000f;
    public float minVertSens = 1f;
    public static float currentSensY = 400f;

    public float maxHoriSens = 1000f;
    public float minHoriSens = 1f;
    public static float currentSensX = 400f;

    public CameraController view;

    public Slider VertSlider;
    public Slider HoriSlider;

    private void Awake()
    {
        view = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
    }
    void Start()
    {
        if (VertSlider != null)
        {
            VertSlider.minValue = minVertSens;
            VertSlider.maxValue = maxVertSens;
            VertSlider.value = view.sensY;
            VertSlider.onValueChanged.AddListener(OnVerticalSliderValueChanged);
        }
        if (HoriSlider != null)
        {
            HoriSlider.minValue = minHoriSens;
            HoriSlider.maxValue = maxHoriSens;
            HoriSlider.value = view.sensX;
            HoriSlider.onValueChanged.AddListener(OnHorizontalSliderValueChanged);
        }
    }

    private void OnVerticalSliderValueChanged(float newValue)
    {
        view.sensY = newValue;
        currentSensY = newValue;
    }
    private void OnHorizontalSliderValueChanged(float newValue)
    {
        view.sensX = newValue;
        currentSensX = newValue;
    }

}

