using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    Vector3 dir;
    void Start()
    {
        dir = AsteroidsGameManager.Instance._reticle.transform.position - Camera.main.transform.position;
        Destroy(gameObject.transform.parent.gameObject, 3f);
    }
    void Update()
    {
        transform.parent.position += speed *Time.deltaTime * dir;
        if(AsteroidsGameManager.Instance._earthHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
