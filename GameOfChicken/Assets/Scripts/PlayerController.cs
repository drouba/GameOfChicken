using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Float Variables for moving player
    private float speed = 8.0f;
    private float turnSpeed = 2.0f;
    private float horizontalInput;
    private float forwardInput;
    // Float Variables for setting boundaries
    private float zBoundary = 9;
    private float xBoundary = 20;

    // Rigidbody variables
    private Rigidbody playerRb;

    // Game Manager variables
    private GameManager gameManager;

    //Boost variables
    private bool isBoosted = false;
    private float boostSpeed = 15;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameIsActive)
        {
            MovePlayer();
            SetBoundaries();
            if(Input.GetKeyDown(KeyCode.Space))
                Boost();
        }
    }

    // Moves player
    void MovePlayer()
    {
        
            // player inputs 
            horizontalInput = Input.GetAxis("Horizontal");
            forwardInput = Input.GetAxis("Vertical");

            // Moving player forward according to player input
            transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);

            // turning the player according to player input
            playerRb.transform.Rotate(Vector3.up * turnSpeed * horizontalInput);


    }

    void Boost()
    {
        playerRb.AddForce(transform.forward * boostSpeed, ForceMode.Impulse);
        isBoosted = true;
        StartCoroutine(BoostCoolDown());
    }

    IEnumerator BoostCoolDown()
    {
        yield return new WaitForSeconds(3);
        isBoosted = false;

    }

    // set boundaries, when player leaves boundaries he comes back on the opposite side of the screen
    void SetBoundaries()
    {
        if (transform.position.z < -zBoundary)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBoundary);
        }

        if (transform.position.z > zBoundary)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBoundary);
        }

        if (transform.position.x < -xBoundary)
        {
            transform.position = new Vector3(-xBoundary, transform.position.y, transform.position.z);
        }

        if (transform.position.x > xBoundary)
        {
            transform.position = new Vector3(xBoundary, transform.position.y, transform.position.z);
        }


    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Cat"))
        {
            gameManager.GameOver();
            Debug.Log("Game Over");
        }

        if (other.gameObject.CompareTag("Seeds"))
        {
            Destroy(other.gameObject);
            gameManager.UpdateScore(10);
        }
    }
}
