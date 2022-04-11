using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] groceries;

    private float spawnRate;

    // gameover
    public bool gameover;

    // title
    public GameObject titleScreen;

    public GameObject gameoverScreen;

    // audio source
    private AudioSource audioSource;
    private AudioSource audioSourceBG;

    public AudioClip wrongSound;
    public AudioClip gameoverSound;

    bool playSound;

    // quote text
    public List<string> quote;

    public Text QuoteText;

    // Start is called before the first frame update
    void Start()
    {
        titleScreen.SetActive(true);
        gameoverScreen.SetActive(false);
        playSound = false;
        spawnRate = 2f;
        audioSource = GameObject.Find("Boxes").GetComponent<AudioSource>();
        audioSourceBG = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameover && playSound == false)
        {
            gameoverScreen.SetActive(true);
            audioSource.PlayOneShot (wrongSound);
            audioSourceBG.clip = gameoverSound;
            audioSourceBG.PlayOneShot(gameoverSound);
            QuoteText.text = GenQuote();
            playSound = true;
        }
    }

    IEnumerator SpawnGroceries()
    {
        while (gameover == false)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, groceries.Length);
            Instantiate(groceries[index],
            GenSpawnPos(),
            groceries[index].transform.rotation);
        }
    }

    IEnumerator IncreaseDifficulty()
    {
        while (gameover == false)
        {
            yield return new WaitForSeconds(5);
            Debug.Log("harder : " + spawnRate);
            spawnRate -= 0.05f;
        }
    }

    Vector3 GenSpawnPos()
    {
        float randZ = Random.Range(-13, 22);
        Vector3 spawnPos = new Vector3(-22f, 1f, randZ);
        return spawnPos;
    }

    public void StartGame()
    {
        // set gameover == false
        gameover = false;

        // start spawn
        StartCoroutine(SpawnGroceries());
        StartCoroutine(IncreaseDifficulty());
        titleScreen.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    string GenQuote()
    {
        int index = Random.Range(0, quote.Count);
        string showQuote = quote[index];
        return showQuote;
    }
}
