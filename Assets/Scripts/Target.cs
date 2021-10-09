using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRB;
    public GameManager gameManager;
    public ParticleSystem explosionParticle;

    public int targetValue;

    private float minForce = 12;
    private float maxForce = 16;
    private float torque = 10;
    private float xSpawnRange = 4;
    private float ySpawnPos = -2;


    void Start()
    {
        //Get the components or object on the scene.
        targetRB = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        #region Set the force, torque and position of the targets as they spawn
        targetRB.AddForce(RandomForce(), ForceMode.Impulse);
        targetRB.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = RandomSpawnPos();
        #endregion
    }

    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);

            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);

            gameManager.UpdateScore(targetValue);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if (gameObject.CompareTag("Good"))
        {
            gameManager.GameOver();
        }
    }

    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minForce, maxForce);
    }

    private float RandomTorque()
    {
        return Random.Range(-torque, torque);
    }

    private Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xSpawnRange, xSpawnRange), ySpawnPos);
    }
}
