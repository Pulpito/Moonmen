using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonmanHolding : MonoBehaviour
{
    private bool isBeingHeld;

    [SerializeField]
    private GameObject mmprefab;

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
            Instantiate(mmprefab, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
