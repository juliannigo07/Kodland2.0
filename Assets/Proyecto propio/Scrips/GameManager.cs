using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score = 0;
    public int shotsLeft = 3;
    public bool gameOver = false;
    public TMP_Text shotsText;
    public TMP_Text resultText;
    public AudioSource Ambiente;
    public AudioSource SonidoPersona;
    public AudioSource SonidoManzana;

    void Start()
    {
        resultText.text = "";
        UpdateUI();
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (gameOver && Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }

    public void AddScore()
    {
        if (gameOver) return;

        score++;
        Debug.Log("Score: " + score);

        if (score >= 3)
        {
            Win();
        }
    }

    public void UseShot()
    {
        if (gameOver) return;

        shotsLeft--;

        if (shotsLeft < 0)
            shotsLeft = 0;

        UpdateUI();
        StartCoroutine(PopText(shotsText));
    }

    public void CheckLoseAfterShot()
    {
        if (gameOver) return;

        if (shotsLeft <= 0)
        {
            Lose();
        }
    }

    public void Win()
    {
        gameOver = true;
        resultText.text = "GANASTE - R para reiniciar";
        resultText.color = Color.green;
        StartCoroutine(PopText(resultText));
        Time.timeScale = 0f;
    }

    public void Lose()
    {
        gameOver = true;
        resultText.text = "PERDISTE - R para reiniciar";
        resultText.color = Color.red;
        StartCoroutine(PopText(resultText));
        Time.timeScale = 0f;
    }

    void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void HitApple()
    {
        if (gameOver) return;

        Win();
    }

    public void HitPerson()
    {
        if (gameOver) return;

        Lose();
    }

    public void MissShot()
    {
        if (gameOver) return;

       
        if (shotsLeft <= 0)
        {
            Lose();
        }
    }

    void UpdateUI()
    {
        shotsText.text = "Balas: " + shotsLeft;

        if (shotsLeft == 1)
            shotsText.color = Color.red;
        else if (shotsLeft == 2)
            shotsText.color = Color.yellow;
        else
            shotsText.color = Color.white;
    }

    IEnumerator PopText(TMP_Text text)
    {
        Vector3 originalScale = text.transform.localScale;
        text.transform.localScale = originalScale * 1.3f;

        yield return new WaitForSecondsRealtime(0.1f);

        text.transform.localScale = originalScale;
    }
}
