using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    // game variables
    public bool gameIsActive = false;
    private bool gameOver = false;
    private bool selection = false;
    public float difficulty;

    // UI Variables
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI maxScoreText;
    public TextMeshProUGUI newMaxScoreText;
    public TextMeshProUGUI endScoreText;
    public TextMeshProUGUI difficultySelection;
    private int maxScore = 0;
    private int score;
    public GameObject titleChicken;

    // spawning variables
    private float seedSpawnRate = 10;
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

        if (!gameIsActive && !gameOver && (Input.GetKeyDown(KeyCode.Return)))
            DifficultySelection();

        if (selection && Input.GetKeyDown(KeyCode.Alpha1))
        {
            difficulty = 0.25f;
            StartGame();
        }

        if (selection && Input.GetKeyDown(KeyCode.Alpha2))
        {
            difficulty = 0.5f;
            StartGame();
        }

        if (selection && Input .GetKeyDown(KeyCode.Alpha3))
        {
            difficulty = 0.75f;
            StartGame();
        }

        if (!gameIsActive && gameOver && Input.GetKeyDown(KeyCode.Return))
            RestartGame();

        
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
        gameOver = false;
        score = 0;

        difficultySelection.gameObject.SetActive(false);

        StartCoroutine(CatSpawn());
        StartCoroutine(SeedSpawn());
        UpdateScore(0);

    }

    public void GameOver()
    {
        gameIsActive = false;
        gameOver = true;

        StopAllCoroutines();

        if (score > maxScore)
        {
            maxScore = score;
            maxScoreText.text = "Maximum score: " + maxScore;
            newMaxScoreText.gameObject.SetActive(true);

        }

        gameOverText.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        gameOverText.gameObject.SetActive(false);
        gameIsActive = false;
        gameOver = false;
        selection = false;
        titleText.gameObject.SetActive(true);
        titleChicken.gameObject.SetActive(true); 
        newMaxScoreText.gameObject.SetActive(false);
        DestroyAllObjects("Seeds");
        DestroyAllObjects("Cat");
        GameObject.FindGameObjectWithTag("Player").transform.position = startPos;
        GameObject.FindGameObjectWithTag("Player").transform.rotation = startRot;

    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
        endScoreText.text = "Score: " + score;
    }

    void DestroyAllObjects(string tag)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);

        for (var i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
    }

    void DifficultySelection()
    {
        titleChicken.gameObject.SetActive(false);
        difficultySelection.gameObject.SetActive(true);
        titleText.gameObject.SetActive(false);
        selection = true;

     

    }
}
