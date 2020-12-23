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
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        limitController = GameObject.Find("LimitController").GetComponent<LimitController>();
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

            if (!isStatic && !limitController.isMoving)
            {
                if (GetComponent<Rigidbody2D>().IsSleeping())
                {
                    limitController.GetComponent<BoxCollider2D>().enabled = true;
                    limitController.GetComponent<SpriteRenderer>().enabled = true;
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
