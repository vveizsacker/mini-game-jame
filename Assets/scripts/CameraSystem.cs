using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    public float cameraSpeed;
    public float borderThickness;

    Vector3 movement = Vector3.zero;

    void Update()
    {
        if(Input.mousePosition.x <= borderThickness)
        {
            movement.x = -cameraSpeed * Time.deltaTime;
        }
        if(Input.mousePosition.x >= Screen.width - borderThickness)
        {
            movement.x = cameraSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.y <= borderThickness)
        {
            movement.y = -cameraSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.y >= Screen.height - borderThickness)
        {
            movement.y = cameraSpeed * Time.deltaTime;
        }

        MoveCamera();
    }

    public void MoveCamera()
    {

        Vector3 d = movement + transform.position;
        movement.z = transform.position.z;
        transform.position = d;
        movement = Vector2.zero;
    }
}
