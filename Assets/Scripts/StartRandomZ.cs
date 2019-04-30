using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRandomZ : MonoBehaviour
{
    void Start()
    {
        int rnd = Random.Range(0, 360);
        this.gameObject.transform.eulerAngles = new Vector3(0, 0, rnd);
    }
}
