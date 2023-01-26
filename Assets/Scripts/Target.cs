using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private float minSpeed = 14.0f;
    private float maxSpeed = 16.0f;
    private float maxTorque = 4.0f;
    private float xRange = 4.0f;
    private float ySpawnPos = -6.0f;

    private GameManager gameManager;

    public int pointValue;

    public ParticleSystem explosionPrefab;
    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomPosition();
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    //private void OnMouseDown()
    //{
    //    if (gameManager.isGameActive)
    //    {
    //        Destroy(gameObject);
    //        Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);
    //        gameManager.UpdatedScore(pointValue);
    //        if (gameObject.CompareTag("Bad") && gameManager.isGameActive)
    //        {
    //            gameManager.UpdatedLives(-1);
    //        }
    //    }
    //}

    public void DestroyTarget()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);
            gameManager.UpdatedScore(pointValue);
            if (gameObject.CompareTag("Bad") && gameManager.isGameActive)
            {
                gameManager.UpdatedLives(-1);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
