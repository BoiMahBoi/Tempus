using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crumble : MonoBehaviour
{

    public float crumbleTime;
    public float respawnObjectTime;

    private bool isCrumbling = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() != null && !isCrumbling)
        {
            LifeIsSuffering();
        }
    }

    public void LifeIsSuffering()
    {
        StartCoroutine(CrumbleObject(crumbleTime));
    }

    private IEnumerator CrumbleObject(float crumbleTime)
    {
        isCrumbling = true;
        yield return new WaitForSeconds(crumbleTime);
        gameObject.GetComponent<ParticleSystem>().Play();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(respawnObjectTime);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        isCrumbling = false;
    }
}
