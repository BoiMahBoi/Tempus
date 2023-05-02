using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crumble : MonoBehaviour
{

    public float crumbleTime;
    public float respawnObjectTime;
    public bool canCarryCrates;

    private bool isCrumbling = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() != null && !isCrumbling && !collision.gameObject.CompareTag("Tilemap"))
        {
            if(collision.gameObject.CompareTag("Crate") && !canCarryCrates)
            {
                Debug.Log("Pik");
                LifeIsSuffering();
            } else if ((!collision.gameObject.CompareTag("Crate") || !(collision.gameObject.name == "Crate")) && !collision.gameObject.CompareTag("Collectable"))
            {
                Debug.Log("Penis");
                LifeIsSuffering();
            }
        }
    }

    public void LifeIsSuffering()
    {
        StartCoroutine(CrumbleObject(crumbleTime));
    }

    public void LifeIsNotSuffering()
    {
        StopAllCoroutines();
        LifeIsBack();
    }

    private IEnumerator CrumbleObject(float crumbleTime)
    {
        isCrumbling = true;
        yield return new WaitForSeconds(crumbleTime);
        LifeIsGone();
        yield return new WaitForSeconds(respawnObjectTime);
        LifeIsBack();
    }

    private void LifeIsGone()
    {
        gameObject.GetComponent<ParticleSystem>().Play();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void LifeIsBack()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        isCrumbling = false;
    }
}
