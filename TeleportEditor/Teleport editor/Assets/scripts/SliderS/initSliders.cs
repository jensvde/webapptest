using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class initSliders : MonoBehaviour
{
        
    SliderSettings slidersSettings;
    List<GameObject> sliders;
    // Start is called before the first frame update
    void Start()
    {
        slidersSettings = new SliderSettingsFactory().GetInstance();
        var allSettings = slidersSettings.GetSlidersKV();

        sliders = new List<GameObject>();
        int y = 115;

        foreach (var sliderKV in allSettings)
        {
            //create slider and text
            var sliderUIObject = Instantiate(Resources.Load("UI slider")) as GameObject;

            sliderUIObject.transform.SetParent(gameObject.GetComponent<Canvas>().transform);
            sliderUIObject.name = sliderKV.Key;
            sliderUIObject.transform.localPosition = new Vector3(0 , y, 0);

            sliderUIObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            sliderUIObject.transform.localScale = new Vector3(1, 1, 1);

            y -= 25;

            sliders.Add(sliderUIObject);
            var script = sliderUIObject.GetComponent<sliderUpdate>();
            script.slidername = sliderKV.Key;

        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var UI in sliders)
        {
            var slider = UI.GetComponentInChildren<Slider>();
            var valueText = UI.GetComponentsInChildren<Text>().First(t => t.CompareTag("slider_value"));

            var slidername = UI.GetComponent<sliderUpdate>().slidername;
            slider.value = slidersSettings.GetSliderKVPbyName(slidername).Value;
            valueText.text = Math.Round(slider.value).ToString();
        }
    }
}
