using UnityEngine;
using System.Collections;

public class BoosterSpawner : MonoBehaviour
{
    public ObjectPool boosterPool;
    public float spawnRate = 15;
    public float timer = 0;
    public float HeightOffset = 2;
    public float deathzone = -19;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SpawnBooster();
    }

    void SpawnBooster()
    {
        if (timer < spawnRate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            GameObject booster = boosterPool.CreateObject();
            float lowestpoint = transform.position.y - HeightOffset;
            float Highestpoint = transform.position.y + HeightOffset;
            Instantiate(booster, new Vector3(transform.position.x, Random.Range(lowestpoint, Highestpoint), transform.position.z), transform.rotation);
            timer = 0;
        }

    }
}
