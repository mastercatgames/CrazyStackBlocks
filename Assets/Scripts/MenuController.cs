using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public float timeToShow = 0.8f;
    // Start is called before the first frame update
    void Start()
    {
        Transform blocks = GameObject.Find("Blocks").transform;
        timeToShow = 1f;

        foreach (Transform block in blocks)
        {
            StartCoroutine(SetActiveAfterTime(block.gameObject, true, timeToShow));            
            timeToShow += 0.3f;
        }
    }

    public IEnumerator SetActiveAfterTime(GameObject gameObject, bool active, float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(active);
    }
}
