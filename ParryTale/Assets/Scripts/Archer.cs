using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    public float coolDown = 3f;

    public GameObject arrow;
    public GameObject shootPoint;
    public GameObject player;
    public float speed = 5f;

    private Rigidbody2D rb;
    private Vector2 facing;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
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
        yield return new WaitForSeconds(coolDown);
        GameObject go = Instantiate(arrow);

        go.transform.position = shootPoint.transform.position;
        StartCoroutine(shoot());
    }
}
