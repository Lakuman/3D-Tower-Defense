using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private bool doMovement = true; 
    // Speed of the camera

    public float panSpeed = 100f;

    //Max and min scroll distance
    public float minY = 30f;
    public float maxY = 200f;

    // Limit on the screen where the camera moves
    public float panBorderThickness = 10f;

    public float scrollSpeed = 5f;
    // Update is called once per frame
    void Update () {
        //If user want that movement stops he just need to press 'escape'
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            doMovement = !doMovement;
        }
        if (!doMovement)
        {
            return;
        }
        // Player can move the camera by using (w,s,a,d) or || by using the mouse.
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        // Zooming
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }
}
