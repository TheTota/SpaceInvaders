using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public float bulletSpeed;
    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Awake()
    {
        this.rb2d = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(transform.up * bulletSpeed);
        //rb2d.AddForce(transform.up * bulletSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.collider.tag)
        {
            case "Alien":
                collision.collider.gameObject.SetActive(false);
                break;

            //....

            default:
                break;
        }
        Destroy(this.gameObject);

    }
}
