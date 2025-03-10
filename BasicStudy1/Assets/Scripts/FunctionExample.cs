using UnityEngine;

public class FunctionExample : MonoBehaviour
{
    //함수 정의
    void SayHello()
    {
        Debug.Log("Hello , Unity");
    }

    int AdDNumbers(int a, int b)
    {
        return a + b;
    }

    void Start()
    {
        SayHello(); //함수 호출
        int total = AdDNumbers(3, 5);
        Debug.Log($"Total : {total}");
    }

    void Update() { }
}
