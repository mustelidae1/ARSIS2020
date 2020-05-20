using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBeltController : MonoBehaviour
{
    [SerializeField]
    private Camera vrCamera;  
    [SerializeField]
    private float rotationRange = 60; // limit of where menus can move left to right 
    [SerializeField]
    private float movementThreshold = 10f; // how fast you need to be moving before it tries to correct it to center 
    [SerializeField]
    private float correctionSpeed = 1000f; // how fast it should attempt to move towards the center 
    [SerializeField]
    private Vector3 groundOffset = new Vector3(0, 0.25f, 0);
    [SerializeField]
    private float groundMultiplier = 0.5f; // scales how much it moves with the player's head 

    // private float lookRotation = 0;
    private float currentRotationLimit = 0;
    private Vector3 lastCameraPosition;

    // set starting rotation limit to be in the middle of the area, 
    // and position where the camera is 
    void Start()
    {
        lastCameraPosition = vrCamera.transform.position;
        float lookRotation = vrCamera.transform.eulerAngles.y;
        currentRotationLimit = lookRotation - rotationRange / 2;
    }

    // TODO resolve issue of looking down past -90 deg pitch flipping look rotation 180 deg
    // TODO resolve 360 to 0 deg jump issue
    // TODO smooth speed (positionDelta) with a moving average
    void Update()
    {
        // get camera's position 
        var cameraPosition = vrCamera.transform.position;

        // get camera's change from the last value 
        var positionDelta = cameraPosition - lastCameraPosition;

        // scale by time
        positionDelta *= Time.deltaTime; 

        var speed = positionDelta.magnitude;
        lastCameraPosition = cameraPosition;
        //var movementPercent = positionDelta.magnitude / movementThreshold;
        var beltPosition = cameraPosition;
        beltPosition.y = (beltPosition.y * groundMultiplier);
        beltPosition += groundOffset;
        float lookAngle = vrCamera.transform.eulerAngles.y;

        // calculate if rotation needs to change 
        if (lookAngle < currentRotationLimit)
        {
            currentRotationLimit = lookAngle;
        }
        if (lookAngle > currentRotationLimit + rotationRange)
        {
            currentRotationLimit = lookAngle - rotationRange;
        }

        //const int thresholdMultiplier = 100000;

        speed -= movementThreshold / 100000;
        bool stationary = speed <= 0;

        //print(speed * 100000);
        //print(positionDelta.magnitude);

        // calculate if translational position needs to change 
        var newAngle = currentRotationLimit + rotationRange / 2;

        if (!stationary)
        {
            //targetRotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(lookRotation, Vector3.up), correctionSpeed * speed);
            newAngle = Mathf.Lerp(newAngle, lookAngle, speed * correctionSpeed);
            currentRotationLimit = newAngle - rotationRange / 2;
        }
        transform.rotation = Quaternion.AngleAxis(newAngle, Vector3.up);
        //print(headRotation);
        transform.position = beltPosition;
    }
}

// UI Height: Set by camera height 

// UI Rotation: If the player is moving, it is rotated towards the camera rotation
// UI Rotation: If the player is stationary, it does not rotate unless the player rotates the camera past the limit
