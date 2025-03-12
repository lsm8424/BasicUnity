using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 1.3f;

    void Start() { }

    void Update()
    {
        float distanceY = moveSpeed * Time.deltaTime;
        if (GameManager.instance.isStart != false)
        {
            transform.Translate(0, -distanceY, 0);
        }
    }

    private void OeBecameInvisible()
    {
        Destroy(gameObject);
    }
}
