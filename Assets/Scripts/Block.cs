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
        rotationSpeed = -50f;
        decrementRotationSpeed = 40f;

        if (GameObject.Find("GameController"))
            gameController = GameObject.Find("GameController").GetComponent<GameController>();
        if (GameObject.Find("LimitController"))
            limitController = GameObject.Find("LimitController").GetComponent<LimitController>();
        else
        {
            //Menu...
            //* Random rotation
            //* Don't increase speed rotation;
            int randomRotation = Random.Range(0, 2);
            if (randomRotation == 0)
                rotationSpeed = -20f;
            else
                rotationSpeed = 20f;
            decrementRotationSpeed = 0f;
        }

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

            if (!isStatic && (GameObject.Find("LimitController") && !limitController.isMoving))
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
        if (other.name == "DeadZone" && !gameController.isInGameOver)
        {
            gameController.GameOver();
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.relativeVelocity.magnitude > 2)
        {
            GameObject.Find("SFX").transform.Find("Hit").GetComponent<AudioSource>().Play();
            transform.Find("DustParticleHit").GetComponent<ParticleSystem>().Play();
        }
    }
}
