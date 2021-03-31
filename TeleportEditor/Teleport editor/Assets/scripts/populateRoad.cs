using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class populateRoad : MonoBehaviour
{

    SliderSettings sliders;

    void Start()
    {
        sliders = new SliderSettingsFactory().GetInstance();
    }

    // Update is called once per frame
    public void UpdateWorld()
    {
        //TODO: don't re-assign objects if slider settings don't change;
        //problem: values are static, are always the same,
        //seperate properties for previous values?
       /* if (!sliders.hasChanged())
            return;
        else
            Debug.Log("changed");
        */
        //for each roadblock in road (first level child)
        foreach (Transform child in gameObject.transform)
        {
            //remove previous object            
            foreach (Transform emptyChild in child) //for each placement empty object
            {
                // get all children of this placement object
                var details = emptyChild.gameObject.GetComponentsInChildren<Transform>().Skip(1).ToArray();
                foreach (Transform detailToDestroy in details)
                {
                    //destroy childObject (should not be more than one
                    GameObject.Destroy(detailToDestroy.gameObject);
                }
            }

            var rdm = new Random();
            foreach (var sliderKV in sliders.GetSlidersKV())
            {
                //find child objects based on tag to put detail on                    
                foreach (var placementTransform in child.GetComponentsInChildren<Transform>().Where(r => r.gameObject.CompareTag(sliderKV.Key)).ToArray())
                {
                    if (Random.Range(0, 100) < sliderKV.Value)
                    {
                        var placement = placementTransform.gameObject;

                        //look for and load in detail model
                        string resourceString = string.Format("details/{0}/{0}", sliderKV.Key);
                        
                        var detail = Instantiate(Resources.Load(resourceString)) as GameObject;

                        detail.transform.parent = placement.transform;

                        detail.transform.position = placement.transform.position;

                        detail.transform.rotation = placement.transform.rotation;
                    }
                }
            }
        }
    }
}
