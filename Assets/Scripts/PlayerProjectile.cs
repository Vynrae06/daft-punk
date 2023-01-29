using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 15f;
    [SerializeField] float selfDestructTime = 1f;

    Player player;
    float xSpeed;
    Rigidbody2D rigidbody2D;
    

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        xSpeed = player.transform.localScale.x * projectileSpeed;
        transform.localScale = new Vector3(player.transform.localScale.x, transform.localScale.y, transform.localScale.z);

        StartCoroutine(SelfDestruct());
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2D.velocity = new Vector2(xSpeed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.Die();
        }
        if (collision.CompareTag("Britney"))
        {
            Britney britney = FindObjectOfType<Britney>();
            britney.TakeHit();
        }
        Destroy(gameObject);
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(selfDestructTime);
        Destroy(gameObject);
    }
}
