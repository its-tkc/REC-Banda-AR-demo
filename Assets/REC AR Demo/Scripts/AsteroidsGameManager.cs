using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidsGameManager : MonoBehaviour
{
    public GameObject _startGameCanvas, _scoreCanvas,_gameOverCanvas,_bullet, _reticle, _earth, _asteroid, earthHealth;
    public static AsteroidsGameManager Instance;
    public int _score, _earthHealth, initialHealth;
    public bool isGameStarted;
    public float rotationSpeed;
    public float spawnIntervel;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        _startGameCanvas.SetActive(true);
        _scoreCanvas.SetActive(false);
        _reticle.SetActive(false);
        _earthHealth = initialHealth;
        earthHealth.GetComponent<Slider>().maxValue = initialHealth;
    }

    void Update()
    {
        if (isGameStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Fire();
            }
            _earth.transform.Rotate(new Vector3(rotationSpeed * Time.deltaTime, 0f, rotationSpeed * Time.deltaTime));
            _gameOverCanvas.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = "Score: " + _score.ToString();
            _scoreCanvas.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "Score: " + _score.ToString();
            earthHealth.GetComponent<Slider>().value = _earthHealth;
        }

        if(_earthHealth <= 0)
        {
            StopCoroutine(SpawnAsteroids());
            isGameStarted = false;
            _gameOverCanvas.SetActive(true);
            _reticle.SetActive(false);
        }
    }

    public void Fire()
    {
        if (_bullet != null) 
        { 
            Instantiate(_bullet, Camera.main.transform.position, Camera.main.transform.rotation);
        }
    }

    public IEnumerator SpawnAsteroids()
    {
        var pos = new Vector3(Random.Range(-10f, 10f), Random.Range(-0.5f, 0.5f), Random.Range(5f, 10f));
        Instantiate(_asteroid, pos, Quaternion.identity);
        yield return new WaitForSeconds(spawnIntervel);
        if (isGameStarted)
        {
            StartCoroutine(SpawnAsteroids());
        }
    }

    public void StartGame()
    {
        _score = 0;
        _earthHealth = initialHealth;
        isGameStarted = true;
        _startGameCanvas.SetActive(false);
        _reticle.SetActive(true);
        _gameOverCanvas.SetActive(false);
        _scoreCanvas.SetActive(true);
        earthHealth.GetComponent<Slider>().maxValue = initialHealth;
        StartCoroutine(SpawnAsteroids());
    }

    public void Quit()
    {
        print("Quited");
        Application.Quit();
    }
}
