using UnityEngine;

public class King_bullet : MonoBehaviour
{
    public int damage = 10;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            if (Triump.Instance.fatal == true)
            {
                damage += 30;
            }
            if (collision.gameObject.GetComponent<Monster>().invincibility == false)
            {
                collision.gameObject.GetComponent<Monster>().Damage(damage * 2);
                Debug.Log("데미지(방어무시 미적용): " + damage);
            }
            else
            {
                collision.gameObject.GetComponent<Monster>().Damage(damage);
                Debug.Log("데미지(방어무시 적용): " + damage);
            }

            Destroy(gameObject);
        }
    }
}
