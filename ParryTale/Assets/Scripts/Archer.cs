using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    public float coolDown;

    public GameObject arrow;
    public GameObject shootPoint;
    public GameObject player;
    public float speed = 5f;

    private Rigidbody2D rb;
    private Vector2 facing;
    private AudioSource BowDraw;
    private ArcherContainer archerContainer;
    // Start is called before the first frame update
    void Start()
    {
        archerContainer = GetComponentInParent<ArcherContainer>();
        rb = this.GetComponent<Rigidbody2D>();
        BowDraw = GetComponent<AudioSource>();
        StartCoroutine(shoot());
    }

    // Update is called once per frame
    void Update()
    {
        rotate();
        //Debug.Log("Rotated Archer");
    }
    private void rotate()
    {
        Vector3 direction = GameObject.FindGameObjectWithTag("Player").transform.position - this.transform.position;
        //Debug.Log("Direction = " + direction);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //direction.Normalize();
        // facing = direction;
        rb.rotation = angle - 196;
    }
    IEnumerator shoot()
    {
        coolDown = Random.Range(2, 4);
        StartCoroutine(playBowDrawSound(coolDown - .6f));
        yield return new WaitForSeconds(coolDown);
        GameObject go = Instantiate(arrow);

        go.transform.position = shootPoint.transform.position;
        StartCoroutine(shoot());
    }

    IEnumerator playBowDrawSound(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        if(!BowDraw.isPlaying)
            BowDraw.PlayOneShot(BowDraw.clip, .75f);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject gameObj = collision.gameObject;

        if(gameObj.tag == "Arrow")
        {
            archerContainer.playDeathSound();
            Destroy(gameObj);
            Destroy(this.gameObject);
        }
    }
}
