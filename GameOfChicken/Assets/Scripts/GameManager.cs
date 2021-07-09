using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    // game variables
    public bool gameIsActive = false;

    // UI Variables
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    private int score;
    public GameObject titleChicken;

    // spawning variables
    private float seedSpawnRate = 5;
    private float catSpawnRate = 1;

    public GameObject[] cats = new GameObject[4];
    public GameObject cat;
    public GameObject seeds;

    // repositionning variables
    private Vector3 startPos = new Vector3(0, 0, 0);
    private Quaternion startRot = new Quaternion(0, 0, 0, 0);


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameIsActive)
            scoreText.gameObject.SetActive(true);
        else
            scoreText.gameObject.SetActive(false);
        
    }

    IEnumerator CatSpawn()
    {
        while (gameIsActive)
        {
            yield return new WaitForSeconds(catSpawnRate);
            SpawnCat();
        }
    }

    IEnumerator SeedSpawn()
    {
        while (gameIsActive)
        {
            yield return new WaitForSeconds(seedSpawnRate);
            SpawnSeeds();
        }
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
        int catTypeIndex = Random.Range(0, cats.Length);

        if (catSpawnIndex == 0)
        {
            Instantiate(cats[catTypeIndex], catSpawnPosition[catSpawnIndex], transform.rotation * Quaternion.Euler(0f, 180f, 0f));
        }

        else if (catSpawnIndex == 1)
        {
            Instantiate(cats[catTypeIndex], catSpawnPosition[catSpawnIndex], transform.rotation);
        }

        else if (catSpawnIndex == 2)
        {
            Instantiate(cats[catTypeIndex], catSpawnPosition[catSpawnIndex], transform.rotation * Quaternion.Euler(0f, -90f, 0f));
        }

        else if (catSpawnIndex == 3)
        {
            Instantiate(cats[catTypeIndex], catSpawnPosition[catSpawnIndex], transform.rotation * Quaternion.Euler(0f, 90f, 0f));
        }

    }

    void SpawnSeeds()
    {
        float seedYPosition = 1;
        float seedXPosition = Random.Range(-19, 19);
        float seedZPosition = Random.Range(-8, 8);

        Vector3 seedSpawnPosition = new Vector3(seedXPosition, seedYPosition, seedZPosition);

        Instantiate(seeds, seedSpawnPosition, seeds.transform.rotation);

    }

    public void StartGame()
    {
        gameIsActive = true;
        score = 0;


        titleText.gameObject.SetActive(false);
        titleChicken.gameObject.SetActive(false);

        StartCoroutine(CatSpawn());
        StartCoroutine(SeedSpawn());
        UpdateScore(0);

    }

    public void GameOver()
    {
        gameIsActive = false;
        gameOverText.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        gameIsActive = false;
        titleText.gameObject.SetActive(true);
        titleChicken.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        DestroyAllObjects("Seeds");
        GameObject.FindGameObjectWithTag("Player").transform.position = startPos;
        GameObject.FindGameObjectWithTag("Player").transform.rotation = startRot;

    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    void DestroyAllObjects(string tag)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);

        for (var i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
    }
}
