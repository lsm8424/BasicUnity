using System.Collections;
using UnityEngine;

public class CoroutineStudy : MonoBehaviour
{
    void Start()
    {
        //StartCoroutine("ExampleCoroutine");
        StartCoroutine(ExampleCoroutine());
    }

    IEnumerator ExampleCoroutine()
    {
        Debug.Log("코루틴 시작");
        yield return new WaitForSeconds(2f); //2초대기
        Debug.Log("2초 후 실행");
    }
}
