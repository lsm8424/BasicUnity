using UnityEngine;

public class Jack_bullet : MonoBehaviour
{
    public int damage = 10;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            Triump.Instance.loyalty = true;
            Debug.Log("충정");

            if (Triump.Instance.fatal == true)
            {
                damage += 30;
            }
            collision.gameObject.GetComponent<Monster>().Damage(damage);
            Debug.Log("데미지: " + damage);

            Destroy(gameObject);
        }
    }
}
