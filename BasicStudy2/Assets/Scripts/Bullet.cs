using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 0.45f;
    public GameObject explosion;

    void Start() { }

    void Update()
    {
        if (GameManager.instance.isStart != false)
        {
            transform.Translate(0, moveSpeed * Time.deltaTime, 0);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Instantiate(explosion, transform.position, Quaternion.identity);

            SoundManager.instance.SoundDie();

            GameManager.instance.AddScore(10);

            Destroy(collision.gameObject);

            Destroy(gameObject);
        }
    }
}
