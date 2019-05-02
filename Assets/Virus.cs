using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    private bool isBeingHeld = true;

    [SerializeField] private GameObject virusParticles;
    [SerializeField] private GameObject virusSprite;
    [SerializeField] private Collider2D virusCollider;

    void Awake() { isBeingHeld = true; }

    void Update()
    {
        if (isBeingHeld)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -2);
            if (!virusParticles.activeSelf || !virusSprite.activeSelf)
            {
                virusSprite.SetActive(true);
                virusParticles.SetActive(true);
            }
            if (Input.GetMouseButtonDown(0)
                && WorldManager.instance.GetScreenRect().Contains(Input.mousePosition))    // If player clicked, inside screen.
            {
                isBeingHeld = false;
                Destroy(this.gameObject);
                WorldManager.instance.StopHolding();
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
