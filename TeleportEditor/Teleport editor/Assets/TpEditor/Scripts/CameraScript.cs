using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    public Slider cameraSpeedSlide;

    private float xAxis;
    private float yAxis;
    private float zoom;
    private Camera cam;
    public bool freezeCam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponentInParent<Camera>(); // get the camera component for later use
    }

    // Update is called once per frame
    void Update()
    {
        if (!freezeCam)
        {
            xAxis = Input.GetAxis("Horizontal"); // get user input
            yAxis = Input.GetAxis("Vertical");

            zoom = Input.GetAxis("Mouse ScrollWheel") * 20; //To zoom faster

            // move camera based on info from xAxis and yAxis
            transform.Translate(new Vector3(-xAxis * -cameraSpeedSlide.value, -yAxis * -cameraSpeedSlide.value, 0.0f));

            //change camera's orthographic size to create zooming in and out. 
            if (zoom < 0)
                cam.orthographicSize -= zoom * -cameraSpeedSlide.value;

            if (zoom > 0)
                cam.orthographicSize += zoom * cameraSpeedSlide.value;
        }
    }

    public void zoomIn()
    { //Pressed the plus button
        cam.orthographicSize -= 10;
    }
    public void zoomOut()
    {//Pressed the minus button
        cam.orthographicSize += 10;
    }
}
