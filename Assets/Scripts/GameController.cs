using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject square;
    public GameObject currentBlock;

    public GameObject stack;
    public Transform floor;

    public float bestHeight;

    public Text score;

    // private LimitController limitController;



    // public GameObject objectTop;
    // public GameObject objectBottom;
    // public Vector3 centerPoint;
    // public float CameraSize;

    void Start()
    {
        SpawnNewBlock(false);
    }

    // Update is called once per frame
    // void Update()
    // {
    //     centerPoint = (objectTop.transform.position - objectBottom.transform.position)/2;
    // centerPoint.z  = -10;

    // CameraSize = (objectTop.transform.position - objectBottom.transform.position).magnitude 
    //            -objectBottom.transform.localScale.x;

    // Camera.main.transform.position = centerPoint;
    // Camera.main.orthographicSize = CameraSize / 2;
    // }

    public void SpawnNewBlock(bool moveCamera)
    {
        if (moveCamera)
        {
            //TODO: Smooth moviment
            //transform.position = new Vector3(0, transform.position.y + 0.7f, 0);
            transform.position = new Vector3(0, bestHeight - 0.5f, 0);

            Camera.main.transform.position = new Vector3(0, Camera.main.transform.position.y + 0.5f, -10);

            if (Camera.main.transform.position.y > 2.5)
            {
                Camera.main.orthographicSize += 0.7f;
            }
            //transform.Find("Main Camera").GetComponent<Camera>().transform.position = new Vector3 (0, transform.Find("Main Camera").GetComponent<Camera>().transform.position.y + 1f, -10f);
            //transform.Find("Main Camera").GetComponent<Camera>().orthographicSize = transform.Find("Main Camera").GetComponent<Camera>().orthographicSize + 1f;
        }
        // Instantiate at position (0, 0, 0) and zero rotation.
        currentBlock = Instantiate(square, new Vector3(0, transform.position.y, 0), Quaternion.identity);

        //currentBlock.transform.SetParent(transform);
        // currentBlock.transform.SetParent(transform.parent.Find("Floor"));

        //print(transform.Find("Square(Clone)"));
        //currentBlock.transform.SetParent(stack.transform);
        //currentBlock.transform.SetParent(null);

        // float verticalSize = GetVerticalSize(stack);
        // Debug.Log(verticalSize);
    }

    // float GetVerticalSize(GameObject stack)
    // {
    //     var b = new Bounds(stack.transform.position, Vector3.zero);
    //     foreach (Renderer r in stack.GetComponentsInChildren<Renderer>())
    //     {
    //         b.Encapsulate(r.bounds);
    //     }

    //     float size = b.max.y - b.min.y;
    //     return size;
    // }

    public void DropBlock()
    {
        //currentBlock.transform.SetParent(floor);
        

        currentBlock.transform.SetParent(stack.transform);
        currentBlock.GetComponent<Block>().wasDropped = true;
        currentBlock.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        // currentBlock.transform.Find("Limit").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

        currentBlock = null;
        //print(currentBlock.transform.lossyScale.y);
    }
    
}
