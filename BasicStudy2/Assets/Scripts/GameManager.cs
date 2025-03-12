using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Text scoreText;
    public Text StartText;

    int score = 0;

    public bool isStart = false;

    void Awake()
    {
        if (GameManager.instance == null)
        {
            GameManager.instance = this;
        }
    }

    void Start()
    {
        StartCoroutine("StartGame");
    }

    IEnumerator StartGame()
    {
        int i = 3;

        while (i > 0)
        {
            StartText.text = i.ToString();

            yield return new WaitForSeconds(1);

            i--;

            if (i == 0)
            {
                StartText.text = "Start!";
                yield return new WaitForSeconds(1);
                StartText.gameObject.SetActive(false); //UI감추기
                isStart = true;
            }
        }
    }

    public void AddScore(int num)
    {
        score += num;
        scoreText.text = "Score : " + score;
    }
}
