using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public bool wasDropped;
    public bool isStatic;
    public float speed; //-50f
    public float decrementSpeed; //40f (hard)
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
        //print(Vector3.Distance(gameController.transform.position, transform.position));

        // if (Vector3.Distance(gameController.transform.position, transform.position) < 1)
        // {
        //     Debug.Log("Less than one.");
        // }
        // else
        // {
        //     Debug.Log("Greater than one.");
        // }

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
                //limitController.transform.position = new Vector3 (0, 5f, 0);
                //limitController.transform.Find("Limit").gameObject.SetActive(true);
                // gameController.SpawnNewBlock(true);

                //print((float)transform.GetComponent<Collider2D>().bounds.size.y);
            }
        }
    }

    void RotateLeft()
    {
        if (speed > -300)
            speed -= Time.deltaTime * decrementSpeed;

        transform.Rotate(Vector3.forward * speed * Time.deltaTime);

    }

    
}
