using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    Vector3 dir;
    public float speed;
    public float minScale, maxScale;
    void Start()
    {
        dir = AsteroidsGameManager.Instance._earth.transform.position - transform.position;
        var scale = Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(scale, scale, scale);
    }
    void Update()
    {
        transform.position += speed * Time.deltaTime * dir;
        transform.Rotate(Random.Range(0f, 20f) * Time.deltaTime, Random.Range(0f, 20f) * Time.deltaTime, Random.Range(0f, 20f) * Time.deltaTime);
        if(!AsteroidsGameManager.Instance.isGameStarted )
        {
            Destroy(gameObject, 0.15f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Earth"))
        {
            AsteroidsGameManager.Instance._earthHealth -= 15;
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
            gameObject.GetComponent<AudioSource>().Play();
            Destroy(gameObject, 0.5f);
        }
        else if (other.gameObject.CompareTag("Bullet"))
        {
            AsteroidsGameManager.Instance._score += 10;
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
            gameObject.GetComponent<AudioSource>().Play();
            Destroy(gameObject, 0.15f);
            Destroy(other.gameObject);
        }
    }
}
