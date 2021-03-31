using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class testSettings : MonoBehaviour
{
    SliderSettings settings = new SliderSettingsFactory().GetInstance();

    public Slider carSlider;
    public Slider pedSlider;
    public Slider bikeSlider;
    public Slider transSlider;

    Text ui;
    // Start is called before the first frame update
    void Start()
    {
        ui = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
        pedSlider.value = settings.GetSliderKVPbyName("pedestrian").Value;
        bikeSlider.value = settings.GetSliderKVPbyName("bicycle").Value;
        carSlider.value = settings.GetSliderKVPbyName("car").Value;
        transSlider.value = settings.GetSliderKVPbyName("van").Value;

        string text = "Pedestrians: " + Math.Round(pedSlider.value);
        text += "\nBikes = " + Math.Round(bikeSlider.value);
        text += "\nCars = " + Math.Round(carSlider.value);
        text += "\nTransport = " + Math.Round(transSlider.value);
        // Debug.Log(text);
        ui.text = text;

    }
}
