using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public Transform enemy;

    public float timeBeforeSpawning = 1.5f;

    public float timeBetweenEnemies = 0.25f;

    public float timeBeforeWaves = 2.0f;

    public int enemiesPerWave = 10;

    private int _currentNumberOfEnemies = 0;

    // Поява хвиль ворогів
    private IEnumerator SpawnEnemies()
    {
        // Початкова затримка перед першою появою ворогів
        yield return new WaitForSeconds(
            timeBeforeSpawning
        );
        // Коли таймер закінчиться, починаємо виробляти ці дії
        while (true)
        {
            // Не створювати нових ворогів, поки не знищені старі

            if (_currentNumberOfEnemies <= 0)
            {
                float randDirection;
                float randDistance;

                for (int i = 0; i < enemiesPerWave; i++)
                {
                    // Задаємо випадкові змінні для відстані танапрямку
                    randDistance = Random.Range(10, 25);
                    randDirection = Random.Range(0, 360);
                    // Використовуємо змінні для задання координат появи ворога
                    float posX = transform.position.x + (
                        Mathf.Cos(
                            (randDirection) * Mathf.Deg2Rad
                        ) * randDistance
                    );
                    float posY = transform.position.y + (
                        Mathf.Sin(
                            (randDirection) * Mathf.Deg2Rad
                        ) * randDistance
                    );
                    // Використовуємо змінні для завдання координат появи ворога

                    Instantiate(
                        enemy,
                        new Vector3(posX, posY, 0),
                        transform.rotation
                    );
                    _currentNumberOfEnemies++;
                    yield return new WaitForSeconds(
                        timeBeforeSpawning
                    );
                }
                // Використовуємо змінні для завдання координат появи ворога
            }

            yield return new WaitForSeconds(
                timeBeforeWaves
            );
        }
    }

    // Процедура зменшення кількості ворогів у змінній
    public void KilledEnemy()
    {
        _currentNumberOfEnemies--;
        Debug.Log(
            "Enemy remain" + _currentNumberOfEnemies
        );
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(
            SpawnEnemies()
        );
    }

    // Update is called once per frame
    void Update()
    {
    }
}