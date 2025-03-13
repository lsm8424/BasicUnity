using UnityEngine;

public class M_Homing : MonoBehaviour
{
    public GameObject target;
    public float Speed = 3f;
    Vector2 dir;
    Vector2 dirNo;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        dir = target.transform.position - transform.position;
        dirNo = dir.normalized;
    }

    void Update()
    {
        transform.Translate(dirNo * Speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
