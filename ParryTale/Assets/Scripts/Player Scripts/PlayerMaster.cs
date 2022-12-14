using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaster : MonoBehaviour
{
    public float speed = .75f;
    public GameObject playerBody;
    public GameObject playerShield;
    public GameObject deathFx;
    

    private Vector2 direction;
    private int health = 3;
    private bool inControl = true;
    private bool isMoving = false;
    private float knockbackSpeed = 15f;
    private bool knockbackCooldown = false;
    private float localSpeed = 0;
    private float actualSpeed = 0;
    private Rigidbody2D rb;
    private AudioSource damageSound;
    public AudioClip deathSound;
    public AudioClip LavaBurn;
    public AudioClip drowningSound;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GameObject.Find("Body");
        playerBody.GetComponent<PlayerBody>().playerScript = this;
        playerShield = GameObject.Find("Shield");
        playerShield.GetComponent<PlayerShield>().playerScript = this;

        deathFx =GetComponent<GameObject>();
        damageSound = GetComponent<AudioSource>();
        
        //dirt.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (inControl)
        {
            move();
            rotate();
        }
        if (!inControl)
        {
            Vector2 temp = speed * knockback() * Time.deltaTime;
            transform.position += new Vector3(temp.x, temp.y, 0);
            rotate();
        }
        
    }

    private void move()
    {
        direction = Vector2.zero;

        Vector2 currentPosition = Camera.main.WorldToScreenPoint(this.transform.position);

        if (Input.GetKey(KeyCode.W))
        {
            direction.y = 1;
            localSpeed += speed;
            //dirt.Play();
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction.x = 1;
            localSpeed += speed;
            //dirt.Play();
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction.y = -1;
            localSpeed += speed;
            //dirt.Play();
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction.x = -1;
            localSpeed += speed;
            //dirt.Play();

        }

        if (direction != Vector2.zero)
        {
            direction.Normalize();
        }

        #region AdjustForMultiKey
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            localSpeed -= speed;
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
        {
            localSpeed -= speed;
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            localSpeed -= speed;
        }
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
        {
            localSpeed -= speed;
        }
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            localSpeed -= speed;
        }
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            localSpeed -= speed;
        }
        #endregion

        Mathf.Clamp(localSpeed, 0, 4);
        localSpeed = Mathf.Lerp(localSpeed, 0, .1f);
        direction.Normalize();
        Vector3 newPosition = new Vector3(localSpeed * direction.x * Time.deltaTime, localSpeed * direction.y * Time.deltaTime, 0);
        this.transform.position += newPosition;
        
    }

    private void rotate()
    {
        var mouseScreenPos = Input.mousePosition;
        var startingScreenPos = Camera.main.WorldToScreenPoint(this.transform.position);
        mouseScreenPos.x -= startingScreenPos.x;
        mouseScreenPos.y -= startingScreenPos.y;
        var angle = Mathf.Atan2(mouseScreenPos.y, mouseScreenPos.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 80));
    }

    public Vector2 knockback()
    {
        Vector2 difference = transform.position - playerShield.transform.position;
        difference *= 2;
        return difference;
    }

    public void startShortKnockback()
    {
        inControl = false;
        StartCoroutine(startShortKnockbackCooldown());
    }

    public void startLongKnockback()
    {
        inControl = false;
        StartCoroutine(startLongKnockbackCooldown());
    }

    public void subtractHealth()
    {
        health--;
        //Player Damage Sound Effect
        if(health == 0)
        {
            deathWithSound();
        }
        if (!damageSound.isPlaying)
            damageSound.PlayOneShot(damageSound.clip, .6f);
    }

    public void deathWithSound()
    {
        if (!damageSound.isPlaying)
            damageSound.PlayOneShot(deathSound, .7f);
        //Instantiate(deathFx);
        Destroy(playerBody);
        Destroy(playerShield);
        //Destroy(deathFx);
        StartCoroutine(gameOverScene());
    }
    public void deathLava()
    {
        //Instantiate(deathFx);
        Destroy(playerBody);
        Destroy(playerShield);
        if (!damageSound.isPlaying)
            damageSound.PlayOneShot(LavaBurn, .7f);
        //Destroy(deathFx);
        StartCoroutine(gameOverScene());
    }

    public void deathWater()
    {
        //Instantiate(deathFx);
        Destroy(playerBody);
        Destroy(playerShield);
        if (!damageSound.isPlaying)
            damageSound.PlayOneShot(drowningSound, .7f);
        //Destroy(deathFx);
        StartCoroutine(gameOverScene());
    }
    IEnumerator gameOverScene()
    {
        yield return new WaitForSeconds(1f);

        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }

    IEnumerator startShortKnockbackCooldown()
    {
        yield return new WaitForSeconds(.2f);
        inControl = true;
    }
    IEnumerator startLongKnockbackCooldown()
    {
        yield return new WaitForSeconds(.4f);
        inControl = true;
    }
}