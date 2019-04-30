using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yeti : MonoBehaviour
{
    public GameObject Moonmen;
    public GameObject Target;
    public float speed;
    private float scale;
    private Animator animator;
    public GameObject Hand;
    private GameObject Points;

    void Start()
    {
        Points = GameObject.Find("Points");
        Moonmen = GameObject.Find("Moonmen");
        this.transform.SetParent(GameObject.Find("Yetis").transform);
        animator = this.GetComponent<Animator>();
        scale = this.transform.localScale.x;
    }

    void Update()
    {
        float dist = 10000;

        for(int i = 0; i < Moonmen.gameObject.transform.childCount; i++)
        {
            if (Vector3.Distance(Moonmen.gameObject.transform.GetChild(i).gameObject.transform.position, this.gameObject.transform.position) < dist)
            {
                dist = Vector3.Distance(Moonmen.gameObject.transform.GetChild(i).gameObject.transform.position, this.gameObject.transform.position);
                Target = Moonmen.gameObject.transform.GetChild(i).gameObject;
            }
        }

        if(dist < 2)
        {
            animator.SetBool("Attack", true);
            Hand.gameObject.GetComponent<EdgeCollider2D>().enabled = true;
        }
        else
        {
            animator.SetBool("Attack", false);
            Hand.gameObject.GetComponent<EdgeCollider2D>().enabled = false;
        }

        if (Target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(((this.transform.position.x + ((Target.transform.position.x) - this.transform.position.x) / 25)), ((this.transform.position.y + ((Target.transform.position.y) - this.transform.position.y) / 25)), 0), speed * Time.deltaTime);
            animator.SetBool("Move", true);

            if (Target.transform.position.x > this.transform.position.x)
                this.transform.localScale = new Vector3(-scale, scale, 1);

            else
                this.transform.localScale = new Vector3(scale, scale, 1);
        }
        else
        {
            if(this.transform.position != Points.transform.GetChild(0).transform.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, Points.transform.GetChild(0).transform.position, speed * Time.deltaTime);

                if (Points.transform.GetChild(0).transform.position.x > this.transform.position.x)
                    this.transform.localScale = new Vector3(-scale, scale, 1);

                else
                    this.transform.localScale = new Vector3(scale, scale, 1);
            }

            else
                animator.SetBool("Move", false);     
        }        
    }
}
