using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    void Start() { }

    void FixedUpdate()
    {
        float distanceX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        if (GameManager.instance.isStart != false)
        {
            transform.Translate(distanceX, 0, 0);
        }
    }
}
