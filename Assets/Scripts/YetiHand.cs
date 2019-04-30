using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YetiHand : MonoBehaviour
{
    public GameObject[] Blood;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Moonman")
        {
             Instantiate(Blood[Random.Range(0, Blood.Length-1)], collision.transform.position, Quaternion.identity);
             Destroy(collision.gameObject);             
        }
    }
}
