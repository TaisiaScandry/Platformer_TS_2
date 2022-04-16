using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerConroller Player = collision.gameObject.GetComponent<PlayerConroller>();
            Player.RecountHealthPoints(-1);
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.up * 5f, ForceMode2D.Impulse);
        }
    }
}