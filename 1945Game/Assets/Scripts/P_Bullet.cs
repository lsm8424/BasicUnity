using UnityEngine;

public class P_Bullet : MonoBehaviour
{
    public float Speed = 4.0f;

    public GameObject effect;

    void Start() { }

    void Update()
    {
        transform.Translate(Vector2.up * Speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            GameObject go = Instantiate(
                effect,
                Monster.Instance.transform.position,
                Quaternion.identity
            );

            Destroy(go, 1);
            //몬스터
            Destroy(collision.gameObject);

            //미사일 삭제
            Destroy(gameObject);
        }
    }
}
