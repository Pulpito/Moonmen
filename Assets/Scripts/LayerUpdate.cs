using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerUpdate : MonoBehaviour
{

    private int orderLayer;
    public GameObject parentObject;

    void Start()
    {
        orderLayer = this.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
    }

    void Update()
    {
        if (parentObject != null)
            this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = parentObject.gameObject.GetComponent<SpriteRenderer>().sortingOrder + orderLayer;
        else
            this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(-this.transform.position.y) + orderLayer;
    }
}
