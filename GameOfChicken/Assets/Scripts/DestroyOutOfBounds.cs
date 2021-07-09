using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float xBoundary = 25;
    private float zBoundary = 15;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -xBoundary || transform.position.x > xBoundary 
            || transform.position.z < -zBoundary || transform.position.z > zBoundary)
        {
            Destroy(gameObject);
        }
    }
}
