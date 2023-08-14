using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpForce = 10.0f;
    private bool canJump = true;

    void Update()
    {
        // Left-Right
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.position += new Vector3(horizontalInput, 0, 0) * moveSpeed * Time.deltaTime;

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            canJump = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            canJump = true;
            string platformColor = GetComponent<Collider>().gameObject.GetComponent<SpriteRenderer>().sprite.name;
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.BallPassedPlatform(platformColor);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Si no esta colisionando con suelo o plataforma no puede saltar
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            canJump = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Platform")
        {
            // Cambio de color en el gamemanager
            string platformColor = collider.gameObject.GetComponent<SpriteRenderer>().sprite.name;
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.BallPassedPlatform(platformColor);
        }
    }
}
