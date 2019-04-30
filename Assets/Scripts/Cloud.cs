using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public Transform topSky;
    public Transform floorSky;
    private float spd;

    private void Start()
    {
        spd = Random.Range(1,3);
    }

    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, this.transform.position + new Vector3(floorSky.position.x, 0,0), Time.deltaTime * spd);
        if (this.transform.position.x > floorSky.transform.position.x)
            this.transform.position = new Vector3(topSky.position.x, Random.Range(topSky.position.y, floorSky.position.y), 0);
    }
}
