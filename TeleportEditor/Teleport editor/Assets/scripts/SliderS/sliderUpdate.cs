using System.Collections;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class sliderUpdate : MonoBehaviour
{
    //https://docs.unity3d.com/540/Documentation/ScriptReference/UI.Slider-onValueChanged.html
    public string slidername;

    Slider slider;
    Text nameField;
    Text valueField;
    SliderSettings settings = new SliderSettingsFactory().GetInstance();

    // Start is called before the first frame update
    void Start()
    {

        nameField = gameObject.GetComponentsInChildren<Text>().First(t => t.CompareTag("slider_name"));
        nameField.text = slidername;

        slider = gameObject.GetComponentInChildren<Slider>();
        slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }


    public void ValueChangeCheck()
    {
        settings.UpdateSliderValue(slidername, slider.value);
        var updateObjects = GameObject.FindGameObjectsWithTag("update_world");        
        //call update world function in everyobject with the tag
        foreach (var uw in updateObjects)
        {            
            uw.GetComponent<populateRoad>().UpdateWorld();
        }
    }
}
