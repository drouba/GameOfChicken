using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject cat;
    public GameObject seeds;

    private float spawnDelay = 2.0f;
    private float seedSpawnRepeatRate = 5;
    private float catSpawnRepeatRate = 1;

    


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnSeeds", spawnDelay, seedSpawnRepeatRate);
        InvokeRepeating("SpawnCat", spawnDelay, catSpawnRepeatRate);
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    // spawns seeds at random locations
    void SpawnSeeds()
    {
        float seedYPosition = 1;
        float seedXPosition = Random.Range(-19, 19);
        float seedZPosition = Random.Range(-8, 8);

        Vector3 seedSpawnPosition = new Vector3 (seedXPosition, seedYPosition, seedZPosition);

        Instantiate(seeds, seedSpawnPosition, seeds.transform.rotation);
        
        
    }

    void SpawnCat()
    {
        float catYPosition = 0.5f;
        float catRandomXPosition = Random.Range(-18, 18);
        float catZPosition = 11;
        float catRandomZPosition = Random.Range(-8, 8);
        float catXPosition = 22;

        Vector3[] catSpawnPosition = new Vector3[4];
        catSpawnPosition[0] = new Vector3(catRandomXPosition, catYPosition, catZPosition);
        catSpawnPosition[1] = new Vector3(catRandomXPosition, catYPosition, -catZPosition);
        catSpawnPosition[2] = new Vector3(catXPosition, catYPosition, catRandomZPosition);
        catSpawnPosition[3] = new Vector3(-catXPosition, catYPosition, catRandomZPosition);

        int catSpawnIndex = Random.Range(0, catSpawnPosition.Length);

        if (catSpawnIndex == 0)
        {
            Instantiate(cat, catSpawnPosition[catSpawnIndex], transform.rotation * Quaternion.Euler(0f, 180f, 0f));
        }

        else if (catSpawnIndex == 1)
        {
            Instantiate(cat, catSpawnPosition[catSpawnIndex], transform.rotation );
        }
        
        else if (catSpawnIndex == 2)
        {
            Instantiate(cat, catSpawnPosition[catSpawnIndex], transform.rotation * Quaternion.Euler(0f, -90f, 0f));
        }

        else if (catSpawnIndex == 3)
        {
            Instantiate(cat, catSpawnPosition[catSpawnIndex], transform.rotation * Quaternion.Euler(0f, 90f, 0f));
        }

    }
        
    
}
