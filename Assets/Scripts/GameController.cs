using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject square, currentBlock, stack;
    public Transform floor;
    public float bestHeight;
    public Text score;

    void Start()
    {
        SpawnNewBlock(false);
    }

    public void SpawnNewBlock(bool moveCamera)
    {
        if (moveCamera)
        {
            //TODO: Smooth moviment
            transform.position = new Vector3(0, bestHeight - 0.5f, 0);

            Camera.main.transform.position = new Vector3(0, Camera.main.transform.position.y + 0.5f, -10);

            if (Camera.main.transform.position.y > 2.5)
            {
                Camera.main.orthographicSize += 0.7f;
            }
        }
        // Instantiate at position (0, 0, 0) and zero rotation.
        currentBlock = Instantiate(square, new Vector3(0, transform.position.y, 0), Quaternion.identity);
    }
    
    public void DropBlock()
    {
        currentBlock.transform.SetParent(stack.transform);
        currentBlock.GetComponent<Block>().wasDropped = true;
        currentBlock.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        currentBlock = null;
    }    
}
