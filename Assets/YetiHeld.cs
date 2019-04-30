using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YetiHeld : MonoBehaviour
{
    private bool isBeingHeld;

    [SerializeField]
    private GameObject Yetiprefab;

    void Awake()
    {
        isBeingHeld = true;
    }

    void Update()
    {
        if (isBeingHeld)
        {
            if (Input.GetMouseButtonDown(0)
                && WorldManager.instance.GetScreenRect().Contains(Input.mousePosition))    // If player clicked, inside screen.
            {
                isBeingHeld = false;

                Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector3(v.x, v.y, 0);
                WorldManager.instance.StopHolding();
            }
        }
        else
        {
            Instantiate(Yetiprefab, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
