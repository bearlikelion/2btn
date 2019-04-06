using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform targetPlayer;
    [SerializeField]
    private float followSpeed = 0.0f;
    [SerializeField]
    private float rotationSpeed = 100f;
    [SerializeField]
    private Vector3 cameraOffset;


    void LateUpdate()
    {
        if (followSpeed < 0.05f)
        {
            followSpeed += 0.0005f;
        }

        Vector3 finalPosition = targetPlayer.position + cameraOffset;

        Vector3 smoothPosition = Vector3.Lerp(transform.position, finalPosition, followSpeed);

        transform.position = smoothPosition;

        //Not efficient, change it
        if(targetPlayer.GetComponent<PlayerController>().currentSide == PlayerController.SIDE.TOP)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 180), rotationSpeed * Time.deltaTime);
        }                                                                                                  
        else if (targetPlayer.GetComponent<PlayerController>().currentSide == PlayerController.SIDE.LEFT)       
        {                                                                                                  
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, -90), rotationSpeed * Time.deltaTime);
        }                                                                                                  
        else if (targetPlayer.GetComponent<PlayerController>().currentSide == PlayerController.SIDE.RIGHT)      
        {                                                                                                  
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 90), rotationSpeed * Time.deltaTime);
        }                                                                                                  
        else if (targetPlayer.GetComponent<PlayerController>().currentSide == PlayerController.SIDE.BOTTOM)     
        {                                                                                                  
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 0), rotationSpeed * Time.deltaTime);
        }
    }
}