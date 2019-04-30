using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    [SerializeField]
    private GameObject thunderSprite;
    [SerializeField]
    private GameObject thunderHoldingSprite;

    private bool isBeingHeld;

    private Collider2D collider;

    void Awake()
    {
        isBeingHeld = true;
        collider = GetComponent<Collider2D>();       
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }

    void Update()
    {
        if (isBeingHeld)
        {
            if (thunderSprite.activeSelf || !thunderHoldingSprite.activeSelf || collider.enabled)
            {
                thunderSprite.SetActive(false);
                thunderHoldingSprite.SetActive(true);
                collider.enabled = false;
            }
            if (Input.GetMouseButtonDown(0)
                && WorldManager.instance.GetScreenRect().Contains(Input.mousePosition))    // If player clicked, inside screen.
            {
                isBeingHeld = false;
                thunderSprite.SetActive(true);
                thunderHoldingSprite.SetActive(false);
                collider.enabled = true;
                GameObject.Find("Flash").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                this.GetComponent<AudioSource>().PlayOneShot(this.GetComponent<AudioSource>().clip);
                StartCoroutine(Die());

                Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector3(v.x, v.y, 0);
                WorldManager.instance.StopHolding();
            }
        }
        else
        {
            //  PLAY THUNDER ANIMATION, SOUND AND AFFECT WORLD.
                // maybe even Camera Shake? ;)
            thunderSprite.SetActive(true);
            thunderHoldingSprite.SetActive(false);
            collider.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Moonman" || collision.gameObject.tag == "Yeti")
        {
            Instantiate(Resources.Load("Prefabs/Ash"), collision.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }
}
