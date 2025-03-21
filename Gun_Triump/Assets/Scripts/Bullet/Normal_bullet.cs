using UnityEngine;

public class Normal_bullet : MonoBehaviour
{
    public int damage = 1;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            if (Triump.Instance.loyalty == true)
            {
                damage += 5;
                Debug.Log("충정총알");
            }
            if (Triump.Instance.fatal == true)
            {
                damage += 30;
                Debug.Log("치명총알");
            }
            collision.gameObject.GetComponent<Monster>().Damage(damage);
            Debug.Log("데미지: " + damage);

            Destroy(gameObject);
        }
    }
}
