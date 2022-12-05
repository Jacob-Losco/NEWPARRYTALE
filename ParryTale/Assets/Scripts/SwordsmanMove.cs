using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordsmanMove : MonoBehaviour
{
    public enum TrackType {patternUp, patternDown, vector, knockback, none };
    public TrackType status = TrackType.patternUp;

    public float speed = .2f;
    private GameObject player;
    private PlayerMaster playerMaster;
    private string name;
    private Renderer renderer;
    public int counter = 0;
    public int countTarget;
    private int rotateSpeed = 2;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerMaster = player.GetComponent<PlayerMaster>();
        if (player == null)
            Debug.Log("Player not found...");
        status = TrackType.patternUp;
        countTarget = Random.Range(4, 8);
        speed = .2f;
        StartCoroutine(startForward());
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 temp = speed * ProcessAI() * Time.deltaTime;
        transform.position += new Vector3(temp.x, temp.y, 0);
    }

    Vector2 ProcessAI()
    {
        Vector3 dir = player.transform.position - this.transform.position;
        Vector2 returnDir = Vector2.zero;
        if(status == TrackType.vector)
        {
            returnDir = VectorTrack(dir);
        }
        if(status == TrackType.patternUp)
        {
            returnDir = new Vector2(0, 1);
        }
        if(status == TrackType.patternDown)
        {
            returnDir = new Vector2(0, -1);
        }
        if(status == TrackType.knockback)
        {
            returnDir = knockback();
        }
        
        return returnDir;
    }

    private Vector2 VectorTrack(Vector3 rawDirection)
    {
        Vector2 temp = new Vector2(rawDirection.x, rawDirection.y);
        temp.Normalize();
        if (Mathf.Abs(temp.x) < 0.1)
            temp.x = 0;
        if (Mathf.Abs(temp.y) < 0.1)
            temp.y = 0;
        var angle = Mathf.Atan2(temp.y, temp.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 60));
        return temp;
    }
    private Vector2 knockback()
    {
        Vector2 tempVelocity = -1 * transform.up;
        return -tempVelocity;
    }

    private void rotate()
    {
        this.transform.Rotate(0, 0, rotateSpeed);
    }

    IEnumerator startForward()
    {
        yield return new WaitForSeconds(1f);
        status = TrackType.patternDown;
        counter += 1;
        if(counter < countTarget)
        {
            StartCoroutine(startBack());
        }
        if (counter >= countTarget)
        {
            counter = 0;
            countTarget = Random.Range(4, 8);
            status = TrackType.vector;
            speed = 4f;
        }
    }
    IEnumerator startBack()
    {
        yield return new WaitForSeconds(1f);
        status = TrackType.patternUp;
        counter += 1;
        if (counter < countTarget)
        {
            StartCoroutine(startForward());
        }
        if(counter >= countTarget)
        {
            counter = 0;
            countTarget = Random.Range(4, 8);
            status = TrackType.vector;
            speed = 4f;
        }
    }
    public void startKnockback()
    {
        status = TrackType.knockback;
        speed = 2f;
        StartCoroutine(startKnockbackCooldown());
    }

    public void startParriedKnockback()
    {
        status = TrackType.knockback;
        speed = 2f;
        StartCoroutine(startParriedKnockbackCooldown());
    }

    IEnumerator startKnockbackCooldown()
    {
        yield return new WaitForSeconds(.5f);
        status = TrackType.vector;
    }
    IEnumerator startParriedKnockbackCooldown()
    {
        yield return new WaitForSeconds(1.5f);
        speed = .2f;
        status = TrackType.patternUp;
        StartCoroutine(startForward());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject gameObject = collision.gameObject;
        if (gameObject.tag == "Arrow")
        {
            Destroy(this.gameObject);
        }
        if(gameObject.tag == "Lava")
        {
            Destroy(this.gameObject);
        }
        if(gameObject.tag == "Body")
        {
            status = TrackType.knockback;
            startKnockback();
        }
        
    }


}
