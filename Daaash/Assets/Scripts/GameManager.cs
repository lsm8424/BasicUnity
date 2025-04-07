using UnityEngine;

public class GameManager : MonoBehaviour
{
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jump");
        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            Debug.Log("키눌리고있습니다.");
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("키를 눌렀다가 놓을때");
        }

        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }
}
