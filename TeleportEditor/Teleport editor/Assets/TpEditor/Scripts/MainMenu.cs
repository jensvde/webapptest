using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class MainMenu : MonoBehaviour
{
    public MeshFilter mouseObject;
    public MouseScript user;
    [SerializeField]
    private Color defaultColor, highlightedColor;
    private Dictionary<string, Image> buttonsBorders;

    // Start is called before the first frame update
    void Start()
    {
        InitializeBorderImages(); //Initialize borderimages
        ChooseAdd();
    }

    private void InitializeBorderImages()
    {
        buttonsBorders = new Dictionary<string, Image>(); //Used to highlight when clicked

        GameObject bttns = GameObject.Find("MainMenu"); //Get all gameobjects from the mainmenus menu(s)
        foreach (Button obj in bttns.GetComponentsInChildren<Button>()) //Find all the buttons
        {
            buttonsBorders.Add(obj.name, obj.GetComponentInChildren<Image>()); //Add the image to dictionary. This will return the first one, which is the Border image. 
        }

    }

    private IEnumerator FlickerAndDefaultColor(string whichOne)
    {//Flicker for a few seconds and then set back to default color, for reset button + save button
        float tick = 0.2f;
        bool toggler = false;
        while (tick >= 0)
        {
            tick -= Time.smoothDeltaTime;
            if (toggler)
            {
                toggler = !toggler;
                buttonsBorders[whichOne].color = highlightedColor;
            }
            else
            {
                toggler = !toggler;
                buttonsBorders[whichOne].color = defaultColor;
            }
            yield return null;
        }
        buttonsBorders[whichOne].color = defaultColor; //Set back to default color
    }

    private void HighlightButton(string whichOne)
    {
        foreach (Image img in buttonsBorders.Values)
        { //Set all borders to the default color
            img.color = defaultColor;
        }

        if (whichOne.Equals("SaveButton") || whichOne.Equals("ResetButton"))
        {
            StartCoroutine(FlickerAndDefaultColor(whichOne)); //Flicker the color of the save/reset button for three times. 
        }
        else
        { //Highlight the currently clicked button  
            buttonsBorders[whichOne].color = highlightedColor;
        }
    }

    public void ChooseAdd()
    {
        HighlightButton("AddButton");
        user.manipulateOption = MouseScript.LevelManipulation.Create; // set mode to create
        user.mr.enabled = true; // show mouse object mesh
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere); // create object, get new object's mesh and set mouse object's mesh to that, then destroy
        mouseObject.mesh = sphere.GetComponent<MeshFilter>().mesh;
        Destroy(sphere);

    }

    public void ChooseDestroy()
    {
        HighlightButton("DestroyButton");
        user.manipulateOption = MouseScript.LevelManipulation.Destroy; // set mode to destroy
        user.mr.enabled = false; // hide mouse mesh
    }

    public void ChooseReset()
    {
        HighlightButton("ResetButton");
        foreach (TpPoint obj in GameObject.FindObjectsOfType<TpPoint>())
        {
            Destroy(obj.gameObject);
        }
        GetComponent<RestService>().DoReset();
    }

    public void ChooseSave()
    {
        HighlightButton("SaveButton");
        StartCoroutine(GetComponent<RestService>().UploadTeleportPoints());
    }

    public void ChooseSaveName()
    {
        int id = int.Parse(user.namePanel.GetComponentsInChildren<TMP_Text>().FirstOrDefault(x => x.name.Equals("IdText")).text.ToString().Split(' ')[2]);
        TpPoint tpText = FindObjectsOfType<TpPoint>().FirstOrDefault(x => x.Id == id);
        tpText.Name = user.namePanel.GetComponentInChildren<TMP_InputField>().text;
        user.namePanel.GetComponentInChildren<Animator>().SetTrigger("Toggle");
        FindObjectOfType<CameraScript>().freezeCam = false;
    }

    public void ChooseCancelName()
    {
        int id = int.Parse(user.namePanel.GetComponentsInChildren<TMP_Text>().FirstOrDefault(x => x.name.Equals("IdText")).text.ToString().Split(' ')[2]);
        TpPoint tpText = FindObjectsOfType<TpPoint>().FirstOrDefault(x => x.Id == id);
        if (user.namePanel.GetComponentInChildren<TMP_InputField>().text.Length < 1)
        {
            tpText.Name = "Teleporatiepunt " + id; //If no name is set, set the default one
        }
        user.namePanel.GetComponentInChildren<Animator>().SetTrigger("Toggle");
        FindObjectOfType<CameraScript>().freezeCam = false;
    }
}
