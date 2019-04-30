using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moonman : MonoBehaviour
{
    public float x;
    public float y;
    private float scale;
    public int secondsMov = 2;
    public int TimeNextMov = 0;
    private float spd = 1;
    private float stp;
    private float sec;
    int scl = -1;
    private bool panic = false;
    public AudioClip[] FearCrys;
    public AudioClip Pop;
    private bool canCry = true;

    public bool isFired;
    public bool isIced;
    public bool isDrowned;
    public float isGravited = 1;
    private float randGir;

    public float Yclimbed = 0;

    public GameObject[] Blood;

    private bool canCoroutineDrowned = true;
    private bool canCoroutineFired = true;

    private GameObject Yetis;
    private GameObject SoundSource;

    private void Start()
    {
        randGir = Random.Range(20, 120);
        this.gameObject.transform.SetParent(GameObject.Find("Moonmen").transform);

        Yetis = GameObject.Find("Yetis");
        scale = this.transform.localScale.x;

        SoundSource = GameObject.Find("Sound Source");

        sec = Random.Range(0.5f, 1.5f);
        stp = Random.Range(0.4f, 1f);

        x = Random.Range(-1f, 1f);
        y = Random.Range(-1f, 1f);

        if (x < 0)
            x = -1;
        else if (x > 0)
            x = 1;

        if (y < 0)
            y = -1;
        else if (y > 0)
            y = 1;

        if (x > 0)
            this.transform.localScale = new Vector3(-scale, scale, 1);

        else if (x < 0)
            this.transform.localScale = new Vector3(scale, scale, 1);

        StartCoroutine(Counter());
    }

    private IEnumerator Cry()
    {
        canCry = false;
        this.gameObject.GetComponent<AudioSource>().PlayOneShot(FearCrys[Random.Range(0, FearCrys.Length)]);
        yield return new WaitForSeconds(Random.Range(1.85f, 3.25f));
        canCry = true;
    }

    void Update()
    {
        if (Yetis.gameObject.transform.childCount > 0)
            panic = true;
        else
            panic = false;

        if (panic && !isFired && !isDrowned && !isIced)
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            this.gameObject.transform.GetChild(2).gameObject.SetActive(false);
            this.gameObject.transform.GetChild(3).gameObject.SetActive(true);

            if (canCry)
                StartCoroutine(Cry());

            Vector3 moveVector = Yetis.gameObject.transform.GetChild(0).gameObject.transform.position - this.transform.position;
            if (isGravited == 1 && Yclimbed <= 0) transform.Translate(-moveVector.normalized * Time.deltaTime, Space.World);

            if (Yetis.gameObject.transform.GetChild(0).gameObject.transform.position.x < this.transform.position.x)
                this.transform.localScale = new Vector3(-scale, scale, 1);

            else
                this.transform.localScale = new Vector3(scale, scale, 1);
        }
        else
        {
            if (isFired)
            {
                if (canCoroutineFired)
                    StartCoroutine(DieFired());

                this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
                this.gameObject.transform.GetChild(2).gameObject.SetActive(false);
                this.gameObject.transform.GetChild(3).gameObject.SetActive(true);
                this.gameObject.transform.GetChild(5).gameObject.SetActive(true);
                this.gameObject.transform.GetChild(6).gameObject.SetActive(false);

                if (canCry)
                    StartCoroutine(Cry());

                Vector3 moveVector = (Vector3.right * x + Vector3.up * y);

                if (isGravited == 1 && Yclimbed <= 0) transform.Translate(moveVector * spd * 2 * Time.deltaTime, Space.World);

                this.transform.localScale += new Vector3(0, 0.001f * scl, 0);
                if (this.transform.localScale.y < 0.22 || this.transform.localScale.y > 0.25)
                    scl *= -1;

                if (TimeNextMov >= secondsMov)
                {
                    x = Random.Range(-1f, 1f);
                    y = Random.Range(-1f, 1f);

                    int rnd = Random.Range(0, 2);

                    if (rnd == 0)
                    {
                        if (x < 0)
                            x = -1;
                        else if (x > 0)
                            x = 1;
                    }

                    else
                    {
                        if (y < 0)
                            y = -1;
                        else if (y > 0)
                            y = 1;
                    }

                    if (x > 0)
                        this.transform.localScale = new Vector3(-scale, scale, 1);

                    else if (x < 0)
                        this.transform.localScale = new Vector3(scale, scale, 1);

                    TimeNextMov = 0;
                }
            }

            else if (isIced)
            {
                this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
                this.gameObject.transform.GetChild(2).gameObject.SetActive(false);
                this.gameObject.transform.GetChild(3).gameObject.SetActive(false);
                this.gameObject.transform.GetChild(5).gameObject.SetActive(false);
                this.gameObject.transform.GetChild(6).gameObject.SetActive(true);
            }

            else if (isDrowned)
            {
                if (canCoroutineDrowned)
                    StartCoroutine(DieDrowned());

                this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
                this.gameObject.transform.GetChild(2).gameObject.SetActive(true);
                this.gameObject.transform.GetChild(3).gameObject.SetActive(false);
                this.gameObject.transform.GetChild(5).gameObject.SetActive(false);
                this.gameObject.transform.GetChild(6).gameObject.SetActive(false);

                Vector3 moveVector = (Vector3.right * x + Vector3.up * y);

                if (isGravited == 1 && Yclimbed <= 0) transform.Translate(moveVector * (spd / 5) * Time.deltaTime, Space.World);

                this.transform.localScale += new Vector3(0, 0.001f * scl, 0);
                if (this.transform.localScale.y < 0.22 || this.transform.localScale.y > 0.25)
                    scl *= -1;

                if (TimeNextMov >= secondsMov)
                {
                    x = Random.Range(-1f, 1f);
                    y = Random.Range(-1f, 1f);

                    int rnd = Random.Range(0, 2);

                    if (rnd == 0)
                    {
                        if (x < 0)
                            x = -1;
                        else if (x > 0)
                            x = 1;
                    }

                    else
                    {
                        if (y < 0)
                            y = -1;
                        else if (y > 0)
                            y = 1;
                    }

                    if (x > 0)
                        this.transform.localScale = new Vector3(-scale, scale, 1);

                    else if (x < 0)
                        this.transform.localScale = new Vector3(scale, scale, 1);

                    TimeNextMov = 0;
                }    
            }

            else
            {
                for (int i = 0; i < this.gameObject.transform.childCount; i++)
                    this.gameObject.transform.GetChild(i).gameObject.SetActive(false);
                this.gameObject.transform.GetChild(0).gameObject.SetActive(true);

                Vector3 moveVector2 = (Vector3.right * x + Vector3.up * y);

                if (isGravited == 1 && Yclimbed <= 0) transform.Translate(moveVector2 * spd * Time.deltaTime, Space.World);

                this.transform.localScale += new Vector3(0, 0.001f * scl, 0);
                if (this.transform.localScale.y < 0.22 || this.transform.localScale.y > 0.25)
                    scl *= -1;

                if (TimeNextMov >= secondsMov)
                {
                    x = Random.Range(-1f, 1f);
                    y = Random.Range(-1f, 1f);

                    int rnd = Random.Range(0, 2);

                    if (rnd == 0)
                    {
                        if (x < 0)
                            x = -1;
                        else if (x > 0)
                            x = 1;
                    }

                    else
                    {
                        if (y < 0)
                            y = -1;
                        else if (y > 0)
                            y = 1;
                    }

                    if (x > 0)
                        this.transform.localScale = new Vector3(-scale, scale, 1);

                    else if (x < 0)
                        this.transform.localScale = new Vector3(scale, scale, 1);

                    TimeNextMov = 0;
                }
            }
        }

        if (isGravited == 0)
        {
            this.GetComponent<CircleCollider2D>().enabled = false;
            float y = transform.position.y;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, 1, 0), 3 * Time.deltaTime);
            Yclimbed += transform.position.y - y;
            this.gameObject.transform.eulerAngles += new Vector3(0, 0, -randGir * Time.deltaTime);
        }
        else if (isGravited == 1)
        {
            if (Yclimbed > 0)
            {
                this.GetComponent<CircleCollider2D>().enabled = false;
                float y = transform.position.y;
                transform.position = Vector3.MoveTowards(transform.position, transform.position - new Vector3(0, 1, 0), 3 * Time.deltaTime);
                Yclimbed -= y - transform.position.y;
                this.gameObject.transform.eulerAngles += new Vector3(0, 0, randGir*Time.deltaTime);
            }
            else
            {
                this.GetComponent<CircleCollider2D>().enabled = true;
                this.transform.SetPositionAndRotation(this.transform.position, Quaternion.identity);
            }
        }
        else if (isGravited == 2)
        {
            SoundSource.gameObject.GetComponent<AudioSource>().PlayOneShot(Pop);
            Instantiate(Blood[Random.Range(0, Blood.Length - 1)], this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    private IEnumerator DieFired()
    {
        canCoroutineFired = false;

        yield return new WaitForSeconds(3);
        if (isFired)
        {
            Instantiate(Resources.Load("Prefabs/Ash"), this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        
        canCoroutineFired = true;
    }
    private IEnumerator DieDrowned()
    {
        canCoroutineDrowned = false;
        
        yield return new WaitForSeconds(3);
        if (isDrowned && !isIced)
        {
            SoundSource.gameObject.GetComponent<AudioSource>().PlayOneShot(Pop);
            Instantiate(Blood[Random.Range(0, Blood.Length - 1)], this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        canCoroutineDrowned = true;
    }

    private IEnumerator Counter()
    {
        spd = 0;
        yield return new WaitForSeconds(stp);
        TimeNextMov++;
        spd = 1;
        yield return new WaitForSeconds(sec);
        StartCoroutine(Counter());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EndWorld")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            if (collision.gameObject.GetComponent<Label>().nameLabel == "WallUp")
            {
                y = y * -1;
            }

            else if (collision.gameObject.GetComponent<Label>().nameLabel == "WallRight")
            {
                x = x * -1;
            }

            else if (collision.gameObject.GetComponent<Label>().nameLabel == "WallDown")
            {
                y = y * -1;
            }

            else if (collision.gameObject.GetComponent<Label>().nameLabel == "WallLeft")
            {
                x = x * -1;
            }

            if (x > 0)
                this.transform.localScale = new Vector3(-scale, scale, 1);

            else if (x < 0)
                this.transform.localScale = new Vector3(scale, scale, 1);

            TimeNextMov = 0;
        }     
    }
}
