using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public bool wasDropped, isStatic, isGrabbing;
    public float rotationSpeed; //-50f
    public float decrementRotationSpeed; //40f (hard)
    private GameController gameController;
    private LimitController limitController;
    // private DynamicJoystick dynamicJoystick;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        limitController = GameObject.Find("LimitController").GetComponent<LimitController>();
        // dynamicJoystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<DynamicJoystick>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!isGrabbing)
        {
            if (!wasDropped)
            {
                RotateLeft();
            }

            if (!isStatic)
            {
                if (GetComponent<Rigidbody2D>().IsSleeping())
                {
                    isStatic = true;
                    limitController.isMoving = true;
                }
            }
        }
    }

    void RotateLeft()
    {
        if (rotationSpeed > -300)
            rotationSpeed -= Time.deltaTime * decrementRotationSpeed;
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
