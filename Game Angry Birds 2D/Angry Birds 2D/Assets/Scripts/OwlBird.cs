using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlBird : Bird
{
    public GameObject deathEffect;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }

}
