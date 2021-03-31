using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using TMPro;

public class MouseScript : MonoBehaviour
{
    public enum LevelManipulation { Create, Destroy }; // the possible level manipulation types

    [HideInInspector]
    public LevelManipulation manipulateOption = LevelManipulation.Create; // create is the default manipulation type.
    [HideInInspector]
    public MeshRenderer mr;

    public Material goodPlace;
    public Material badPlace;

    private Vector3 mousePos;
    private bool colliding;
    private Ray ray;
    private RaycastHit hit;

    public GameObject namePanel;

    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>(); // get the mesh renderer component and store it in mr.
        namePanel = GameObject.Find("NameMenu"); //Get the namePanel
    }

    // Update is called once per frame
    void Update()
    {
        // Have the object follow the mouse cursor by getting mouse coordinates and converting them to world point.
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = new Vector3(
            (mousePos.x),
            
            6f,
            (mousePos.z)); // limit object movement to minimum -60 and maximum 60 for both x and y coordinates. Z alwasy remains 4.369.

        ray = Camera.main.ScreenPointToRay(Input.mousePosition); // send out raycast to detect objects
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.layer == 9) // check if raycast hitting user created object.
            {
                colliding = true; // Unity now knows it cannot create any new object until collision is false.
                mr.material = badPlace; // change the material to red, indicating that the user cannot place the object there.
            }
            else
            {
                colliding = false;
                mr.material = goodPlace;
            }
        }

        // after pressing the left mouse button...
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject()) // check if mouse over UI object.
            {
                if (colliding == false && manipulateOption == LevelManipulation.Create) // create an object if not colliding with anything.
                    CreateObject();
                else if (colliding == true && manipulateOption == LevelManipulation.Destroy) // select object under mouse to be destroyed.
                {
                    FindObjectOfType<RestService>().AddDeletedTpPoints(hit.collider.gameObject.GetComponent<TpPoint>().Id); //Add teleport point to list of deleted tp points
                    Destroy(hit.collider.gameObject); // remove from game.
                }
                else if (colliding == true && manipulateOption == LevelManipulation.Create)
                {
                    FindObjectOfType<CameraScript>().freezeCam = true;
                    if (hit.collider.GetComponent<TpPoint>().Name == null)
                        namePanel.GetComponentInChildren<TMP_InputField>().text = "Teleportatie punt nr " + hit.collider.GetComponent<TpPoint>().Id;
                    else
                        namePanel.GetComponentInChildren<TMP_InputField>().text = hit.collider.GetComponent<TpPoint>().Name;
                    namePanel.GetComponentsInChildren<TMP_Text>().FirstOrDefault(x => x.name.Equals("IdText")).text = "Teleportatie punt " + hit.collider.GetComponent<TpPoint>().Id; ;
                    namePanel.GetComponentInChildren<Animator>().SetTrigger("Toggle");
                }
            }
        }
    }
    void CreateObject()
    {
        GameObject newObj;
        //Create object
        newObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        newObj.transform.position = transform.position;
        newObj.layer = 9; // set to Spawned Objects layer

        //Add editor object component and feed it data.
        TpPoint tp = newObj.AddComponent<TpPoint>();
        tp.pos = newObj.transform.position;
        List<TpPoint> objs = FindObjectsOfType<TpPoint>().ToList();
        objs.Sort();
        tp.Id = objs.Last().Id + 1;

        namePanel.GetComponentInChildren<TMP_InputField>().text = "Teleportatie punt nr " + tp.Id;
        namePanel.GetComponentsInChildren<TMP_Text>().FirstOrDefault(x => x.name.Equals("IdText")).text = "Teleportatie punt " + tp.Id; ;
        namePanel.GetComponentInChildren<Animator>().SetTrigger("Toggle");
        FindObjectOfType<CameraScript>().freezeCam = true;

    }
}
