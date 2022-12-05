using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public enum PlayerStatus { normal, punish, parry, none };
    public int patternIndex = 0;

    public Vector2 direction;
    public float speed = .75f;
    public float knockbackSpeed = 1f;
    public PlayerStatus status = PlayerStatus.normal;

    private float localspeed = 0;
    private float actualSpeed = 0;
    private bool parryCooldown = false;
    public bool inKnockback = false;

    // Start is called before the first frame update
    void Start()
    {
        status = PlayerStatus.normal;
    }

    // Update is called once per frame
    void Update()
    {
        if (inKnockback == false)
        {
            controlPlayer();
        }
        if(inKnockback == true)
        {
             direction = knockBack();
            this.transform.position -= new Vector3(direction.x, direction.y, 0);
        }
       
    }


    private void controlPlayer()
    {
        #region movement
        //Vector3 newPosition = speed * direction;
        //newPosition.z = 0;
        direction = Vector2.zero;

        bool blockMovingUp = false;
        bool blockMovingDown = false;
        bool blockMovingLeft = false;
        bool blockMovingRight = false;

        float halfHeight = 75;
        float halfWidth = 100;
        Vector2 currentPosition = Camera.main.WorldToScreenPoint(this.transform.position);

        //if((currentPosition.x + halfWidth > Screen.width))
        //{
        //    blockMovingRight = true;
        //}
        //if ((currentPosition.y + halfHeight > Screen.height))
        //{
        //    blockMovingUp = true;
        //}
        //if ((currentPosition.x - halfWidth < 0))
        //{
        //    blockMovingLeft = true;
        //}
        //if ((currentPosition.y - halfHeight < 0))
        //{
        //    blockMovingDown = true;
        //}


        if (Input.GetKey(KeyCode.W) && !blockMovingUp)
        {
            direction.y = 1;
            localspeed += speed;
        }
        if (Input.GetKey(KeyCode.D) && !blockMovingRight)
        {
            direction.x = 1;
            localspeed += speed;
        }
        if (Input.GetKey(KeyCode.S) && !blockMovingDown)
        {
            direction.y = -1;
            localspeed += speed;
        }
        if (Input.GetKey(KeyCode.A) && !blockMovingLeft)
        {
            direction.x = -1;
            localspeed += speed;
        }

        #region AdjustForMultiKey
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            localspeed -= speed;
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
        {
            localspeed -= speed;
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            localspeed -= speed;
        }
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
        {
            localspeed -= speed;
        }
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            localspeed -= speed;
        }
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            localspeed -= speed;
        }
        #endregion

        if (direction != Vector2.zero)
        {
            direction.Normalize();
        }

        Mathf.Clamp(localspeed, 0, 4);
        localspeed = Mathf.Lerp(localspeed, 0, .2f);
        direction.Normalize();
        Vector3 newPosition = new Vector3(localspeed * direction.x * Time.deltaTime, localspeed * direction.y * Time.deltaTime, 0);
        this.transform.position += newPosition;
        #endregion

        #region rotateToMouse
        var mouseScreenPos = Input.mousePosition;
        var startingScreenPos = Camera.main.WorldToScreenPoint(this.transform.position);
        mouseScreenPos.x -= startingScreenPos.x;
        mouseScreenPos.y -= startingScreenPos.y;
        var angle = Mathf.Atan2(mouseScreenPos.y, mouseScreenPos.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 60));
        #endregion

        #region onSpaceClick
        if (Input.GetKey(KeyCode.Space) && parryCooldown == false)
        {
            parryCooldown = true;
            StartCoroutine(startParryCooldown());
            startParry();
        }
        #endregion
    }

    void startParry()
    {
        StartCoroutine(initParry());
    }

    IEnumerator initParry()
    {
        yield return new WaitForSeconds(0f);
        status = PlayerStatus.parry;
        Debug.Log("Parry");
        StartCoroutine(changePattern(1));
    }

    IEnumerator changePattern(int stepType)
    {
        yield return new WaitForSeconds(.2f);
        if (stepType == 1)
        {
            status = PlayerStatus.punish;
            Debug.Log("punish");
            StartCoroutine(changePattern(2));
        }
        if (stepType == 2)
        {
            status = PlayerStatus.normal;
            Debug.Log("normal");
        }
    }

    IEnumerator startKnockbackCooldown()
    {
        yield return new WaitForSeconds(.5f);
        inKnockback = false;
    }

    IEnumerator startParryCooldown()
    {
        yield return new WaitForSeconds(.2f);
        parryCooldown = false;
    }

    public void startKnockback()
    {
        StartCoroutine(startKnockbackCooldown());
        inKnockback = true;
    }

    public Vector2 knockBack()
    {
        actualSpeed = 0;
        actualSpeed -= knockbackSpeed;
        Vector2 tempVelocity = actualSpeed * transform.up * Time.deltaTime;
        return tempVelocity;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    GameObject gameObject = collision.gameObject;
    //    if(gameObject.tag == "Arrow")
    //    {
    //        Destroy(this.gameObject);
    //    }
    //    Debug.Log(gameObject.tag);
    //}
}