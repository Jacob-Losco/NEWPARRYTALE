using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaster : MonoBehaviour
{
    public float speed = .75f;
    public GameObject playerBody;
    public GameObject playerShield;

    private Vector2 direction;
    private int health = 3;
    private bool inControl = true;
    private float knockbackSpeed = 15f;
    private float localSpeed = 0;
    private float actualSpeed = 0;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GameObject.Find("Body");
        playerBody.GetComponent<PlayerBody>().playerScript = this;
        playerShield = GameObject.Find("Shield");
        playerShield.GetComponent<PlayerShield>().playerScript = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (inControl)
        {
            move();
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
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction.x = 1;
            localSpeed += speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction.y = -1;
            localSpeed += speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction.x = -1;
            localSpeed += speed;
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
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    public void knockback()
    {
        Debug.Log("Function Ran");
        Vector3 difference = transform.position - playerShield.transform.position;
        transform.position = new Vector3(transform.position.x + difference.x, transform.position.y + difference.y, 0);
    }

    public void subtractHealth()
    {
        health--;
        if(health == 0)
        {
            death();
        }
    }

    public void death()
    {
        Destroy(playerBody);
        Destroy(playerShield);
        Destroy(this);
    }
}