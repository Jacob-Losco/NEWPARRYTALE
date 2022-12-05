using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    float speed = 5f;
    private Rigidbody2D rb;

    public Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        Vector3 direction = GameObject.FindGameObjectWithTag("Player").transform.position - this.transform.position;
        Debug.Log("Direction = " + direction);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //direction.Normalize();
        // facing = direction;
        rb.rotation = angle - 90;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    
        private void Move()
        {
            //Vector3 newPosition = speed * direction;
            //newPosition.z = 0;



            Vector3 newPosition = new Vector3(speed * transform.up.x * Time.deltaTime, speed * transform.up.y * Time.deltaTime, 0);
            this.transform.position += newPosition;

        }
    
}
