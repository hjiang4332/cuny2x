using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CoinCollect : MonoBehaviour
{
    // Start is called before the first frame update
    int score = 0;
    public Text scoreText;
    public Slider progressBar;

    void Start()
    {
        scoreText.text = "Progress: " + score.ToString() + "/10" ;
        progressBar.value = score;
    }

    public void NextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            score++;
            progressBar.value = score;
            if (score == 10)
            {
                SceneManager.LoadScene("Level2");
            }
            else
            {
                scoreText.text = "Progress: " + score.ToString() + "/10";
                Destroy(collision.gameObject);
            }
        }
    }
}
