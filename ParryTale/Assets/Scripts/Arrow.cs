using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    float speed = 5f;
    private Rigidbody2D rb;

    public Vector2 direction;
    public bool isReflect;
    private float angle;
    // Start is called before the first frame update
    void Start()
    {
        isReflect = false;
        rb = this.GetComponent<Rigidbody2D>();
        Vector3 direction = GameObject.FindGameObjectWithTag("Player").transform.position - this.transform.position;
        Debug.Log("Direction = " + direction);
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //direction.Normalize();
        // facing = direction;
        rb.rotation = angle - 90;
    }

    // Update is called once per frame
    void Update()
    { 
        this.transform.position = Move();
       
        if (isReflect)
        {
            rb.rotation = angle + 90;
        }
    }
    
    private Vector3 Move()
    {
        if (direction != Vector2.zero)
        {
            direction.Normalize();
        }
        
        Vector3 newPosition = new Vector3(speed * transform.up.x * Time.deltaTime, speed * transform.up.y * Time.deltaTime,0);
        newPosition += this.transform.position;
        return newPosition;
    }

    
}
