using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SliderSettingsFactory
{
    static SliderSettings settings;

    public SliderSettingsFactory()
    {
        if (settings == null)
           settings = new SliderSettings(new string[] { "bicycle", "pedestrian", "car", "bus", "van" });
    }


    public SliderSettings GetInstance()
    {
        return settings;
    }
}
