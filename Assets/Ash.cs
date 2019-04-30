using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ash : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(2.5f);
        Destroy(this.gameObject);
    }
}
