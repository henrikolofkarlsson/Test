using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    [SerializeField]
    private GameObject enemyShipPrefab;
    [SerializeField]
    private GameObject[] powerups;

    private GameManager _gameManager;

    private void Start()
    {
        // get a handle on the game manager to access game over bool
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void startSpawning()
    {
        StartCoroutine(SpawnEnemiesRoutine());
        StartCoroutine(SpawnPowerUpsRoutine());
    }

    //Spawn enemies every five secs
    IEnumerator SpawnEnemiesRoutine()
    {
        while (_gameManager.gameOver == false) {
            float randomX = Random.Range(-9.0f, 9.0f);
            Instantiate(enemyShipPrefab, new Vector3(randomX, 8, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }

    //Spawn enemies every five secs
    IEnumerator SpawnPowerUpsRoutine()
    {
        while (_gameManager.gameOver == false)
        {
            int randomPowerUp = Random.Range(0, 3);
            float randomX = Random.Range(-9.0f, 9.0f);
            Instantiate(powerups[randomPowerUp], new Vector3(randomX, 8, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }
}
