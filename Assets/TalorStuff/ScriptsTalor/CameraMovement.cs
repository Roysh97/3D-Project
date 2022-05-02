using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    bool doMovement = true;

    public float panSpeed = 65f;
    public float panBorderThickness = 15f;
    public float scrollSpeed = 10f;
    public float minY = 10f;
    public float maxY = 300F;

    // Update is called once per frame
    void Update()
    {
        // Move camera on the X & Y axis by pressing on the "w, s, d, a" or by moving the mouse position to the edge of the screen.
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        else if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        else if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        else if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        // If the player was pressing on the scroll button - move the camera slowly.
        if (Input.GetMouseButton(2))
        {
            panSpeed = 85f;
        }
        else
        {
            panSpeed = 65f;
        }

        // Camera zoom in & out by scrolling
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;
        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);   // Limit camera scrolling by minimum & maximum value.
        transform.position = pos;
    }
}
