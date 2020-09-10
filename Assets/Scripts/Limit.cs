using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limit : MonoBehaviour
{
    public bool detect = true;

    private void OnTriggerEnter2D(Collider2D col)
    {
        for (int i = 0; i < GameObject.Find("LevelManager").GetComponent<LevelManager>().ySize; i++)
        {
            if (col.CompareTag("Alien" + i) && detect)
            {
                detect = false;
                col.GetComponentInParent<LevelManager>().WaveReachesLimit();
                StartCoroutine(wait());
            }
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.2f);
        detect = true;
    }
}
