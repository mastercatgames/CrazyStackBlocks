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
                    //Give an extra time (2 seconds), because IsSleeping fires fast sometimes
                    //Edit: It fires fast when we tap, hold and release the finger (without move to sides)
                    //I don't know why this happen :( but this 2 seconds works fine
                    if (gameController.timerAfterDrop > 2f)
                    {
                        SleepBlock();
                    }
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
        isStatic = true;
        limitController.Invoke("Move", 0.5f);
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
