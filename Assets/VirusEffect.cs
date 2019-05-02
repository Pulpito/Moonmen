using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusEffect : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Moonman" || other.gameObject.tag == "Yeti")
        {
            if (Random.Range(0, 100) < 10)
            {
            if (other.gameObject.tag == "Moonman") { other.GetComponent<Moonman>().GetSick(); }
            }
        }
    }
}
