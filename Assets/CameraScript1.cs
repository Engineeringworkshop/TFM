using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript1 : MonoBehaviour
{
    private Camera camera;

    public float MovementSpeedX;
    public float MovementSpeedY;
    public float MovementSpeedZ;

    private void OnValidate()
    {
        if (camera == null)
        {
            camera = GetComponent<Camera>();
        }
    }

    private void Update()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x + Time.deltaTime * MovementSpeedX, 
            gameObject.transform.position.y + Time.deltaTime * MovementSpeedY, 
            gameObject.transform.position.z + Time.deltaTime * MovementSpeedZ);
    }
}
