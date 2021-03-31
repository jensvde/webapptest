using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SliderSettings
{
    List<KeyVal<string, float>> sliderValues;
    List<KeyVal<string, float>> prevValues;

    public SliderSettings(string[] sliderNames)
    {
        sliderValues = new List<KeyVal<string, float>>();
        prevValues = new List<KeyVal<string, float>>();

        foreach (string sn in sliderNames)
        {
            sliderValues.Add(new KeyVal<string, float>(sn, 0));
            prevValues.Add(new KeyVal<string, float>(sn, 0));
        }
    }


    public void UpdateSliderValue(string slidername, float newValue)
    {
        var kvp = GetSliderKVPbyName(slidername);
        var prevkvp = GetPrevKVPbyName(slidername);
        var nextKVP = GetNextKVP(GetIndexFromName(kvp.Key));
        prevkvp.Value = kvp.Value;
        //if the new value is below 0, take away from next slider
        if (newValue < 0)
        {            
            kvp.Value = 0;
            UpdateSliderValue(nextKVP.Key, nextKVP.Value -= (-1 * newValue));
            return;
        }


        kvp.Value = newValue;
        //if the combined percentage is over 100, take away from next slider
        float fullperc = CheckFullPercentage();
        if (fullperc > 100)
        {
            UpdateSliderValue(nextKVP.Key, nextKVP.Value - (fullperc - 100));
        }
    }


    public List<KeyVal<string, float>> GetSlidersKV()
    {
        return sliderValues;
    }


    public KeyVal<string, float> GetSliderKVPbyName(string slidername)
    {
        return sliderValues.Find(kv => kv.Key.Equals(slidername));
    }
    private KeyVal<string, float> GetPrevKVPbyName(string slidername)
    {
        return prevValues.Find(kv => kv.Key.Equals(slidername));
    }
    private KeyVal<string, float> GetNextKVP(int index)
    {
        return sliderValues[(index + 1) % sliderValues.Count()];
    }

    private int GetIndexFromName(string slidername)
    {
        return sliderValues.FindIndex(kv => kv.Key.Equals(slidername));
    }

    private float CheckFullPercentage()
    {
        return sliderValues.Sum(kv => kv.Value);
    }
    
    public bool hasChanged()
    {
        foreach(var kvp in sliderValues)
        {
            var prev = GetPrevKVPbyName(kvp.Key);
            if(Math.Round(prev.Value) != Math.Round(kvp.Value))
            {
                return true;
            }
        }

        return false;
    }
    
    
}
