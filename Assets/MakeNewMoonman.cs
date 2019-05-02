using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeNewMoonman : MonoBehaviour
{
    [SerializeField] private GameObject moonmanPrefab;

    private float waitSeconds = 3;
    private float waited;

    private bool sick = false;
    private Color col = Color.black;

    void Start() { waited = 0; }

    public void MoonmanInfo(bool isSick, Color sickColor)
    {
        sick = isSick; col = sickColor;
    }

    void Update()
    {
        waited += Time.deltaTime;
        if(waited > waitSeconds)
        {
            GameObject parentObj = GameObject.Find("Moonmen");
            GameObject newMan = Instantiate(moonmanPrefab);
            newMan.transform.position = transform.position;
            Moonman m = newMan.GetComponent<Moonman>();
            m.isSick = sick;
            if (sick) { m.GetComponent<SpriteRenderer>().color = col; }
            newMan.transform.parent = parentObj.transform;
            Destroy(this.gameObject);
        }
    }
}
