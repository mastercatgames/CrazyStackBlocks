using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public bool wasDropped, isStatic, isGrabbing, look;
    public float rotationSpeed; //-50f
    public float decrementRotationSpeed; //40f (hard)
    private GameController gameController;
    private LimitController limitController;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        limitController = GameObject.Find("LimitController").GetComponent<LimitController>();

        rotationSpeed = -50f;
        decrementRotationSpeed = 40f;

        Transform eyes = transform.Find("Face").Find("Eyes");

        //Change the color of Eyes Lid
        Color updatedColor = GetComponent<SpriteRenderer>().color;
        float amount = 0.1f;
        updatedColor = new Color(updatedColor.r - amount, updatedColor.g - amount, updatedColor.b - amount);

        eyes.Find("_rightEye").Find("Lid").GetComponent<SpriteRenderer>().color = updatedColor;
        eyes.Find("_leftEye").Find("Lid").GetComponent<SpriteRenderer>().color = updatedColor;
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
                    // limitController.GetComponent<BoxCollider2D>().enabled = true;
                    // limitController.GetComponent<SpriteRenderer>().enabled = true;
                    // isStatic = true;
                    // limitController.isMoving = true;
                    SleepBlock();
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

    public void SleepBlock()
    {
        limitController.GetComponent<BoxCollider2D>().enabled = true;
        limitController.GetComponent<SpriteRenderer>().enabled = true;
        isStatic = true;
        limitController.isMoving = true;
        gameController.timerAfterDrop = 6f; //reset timer
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "DeadZone") 
        {
            gameController.GameOver();
        }
    }
}
