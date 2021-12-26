using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;


    // Update is called once per frame
    void Update()
    {

        Vector3 cam = player.position + offset;
        if(cam.x < gameObject.GetComponent<Camera>().orthographicSize * Screen.width / Screen.height - 1.5f)
        {
            cam.x = gameObject.GetComponent<Camera>().orthographicSize * Screen.width / Screen.height - 1.5f;
        }
        else if (cam.x > 3000 - gameObject.GetComponent<Camera>().orthographicSize * Screen.width / Screen.height - 1.5f)
        {
            cam.x = 3000 - gameObject.GetComponent<Camera>().orthographicSize * Screen.width / Screen.height -1.5f;
        }

        if (cam.y < gameObject.GetComponent<Camera>().orthographicSize - 1.5f)
        {
            cam.y = gameObject.GetComponent<Camera>().orthographicSize - 1.5f;
        }
        else if (cam.y > 600 - gameObject.GetComponent<Camera>().orthographicSize - 1.5f)
        {
            cam.y = 600 - gameObject.GetComponent<Camera>().orthographicSize - 1.5f;
        }
        transform.position = cam;
    }
}

