using UnityEngine;

public class VariableExample : MonoBehaviour
{
    public int playerScore = 0;
    public float speed = 5.5f;
    public string playername = "Hero";
    public bool isGameOver = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log($"Player Name : {playername} ");
    }
}
