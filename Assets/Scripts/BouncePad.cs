using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float bounceForce;
    public float normalScale;
    public float maxScale;
    private float currentScale;
    public float scaleRecoveryRate;

    void Update()
    {
        if (currentScale > normalScale)
        {
            currentScale = Mathf.MoveTowards(currentScale, normalScale, scaleRecoveryRate * Time.deltaTime);
            transform.parent.localScale = new Vector2(transform.parent.localScale.x, currentScale);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (col.GetComponent<Player>().enabled == true)
            {
                if (col.GetComponent<Player>().onGround == true)
                {
                    if (col.GetComponent<Player>().isBounceOnCooldown == false)
                    {
                        col.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
                        col.GetComponent<Player>().StartCoroutine("BounceCooldown");
                        col.GetComponentInChildren<Animator>().SetBool("isJumping", true);

                        transform.parent.localScale = new Vector2(transform.parent.localScale.x, maxScale);
                        currentScale = maxScale;
                    }
                }
            }
            else
            {
                if (col.GetComponent<IndividualMovementApplying>().onGround == true)
                {
                    if (col.GetComponent<IndividualMovementApplying>().isBounceOnCooldown == false)
                    {
                        col.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
                        col.GetComponent<IndividualMovementApplying>().StartCoroutine("BounceCooldown");
                        col.GetComponentInChildren<Animator>().SetBool("isJumping", true);

                        transform.parent.localScale = new Vector2(transform.parent.localScale.x, maxScale);
                        currentScale = maxScale;
                    }
                }
            }
        }
    }
}
