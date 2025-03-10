using UnityEngine;

public class ConditionExample : MonoBehaviour
{
    //연산자와 조건문
    public int health = 100;

    void Update()
    {
        health -= 1; //체력 감소
        Debug.Log("Health: " + health);

        //조건문
        if (health <= 0)
        {
            Debug.Log("Game Over!");
        }
    }
}
