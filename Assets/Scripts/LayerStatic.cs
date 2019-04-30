using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerStatic : MonoBehaviour
{
    private int orderLayer;

    void Start()
    {
        orderLayer = this.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
        this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(-transform.position.y) + orderLayer;
    }
}
