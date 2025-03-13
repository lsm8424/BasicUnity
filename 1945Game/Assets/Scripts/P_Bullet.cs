using UnityEngine;

public class P_Bullet : MonoBehaviour
{
    public float Speed = 4.0f;

    void Start() { }

    void Update()
    {
        transform.Translate(Vector2.up * Speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
