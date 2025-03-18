using System.Collections;
using UnityEngine;

public class Triump : MonoBehaviour
{
    public GameObject gun;
    public GameObject normal_bullet;
    public GameObject jack_bullet;
    public GameObject queen_bullet;
    public GameObject king_bullet;

    public Transform pos = null;

    private bool isReady = true;
    public float shoot_delay = 0.5f;

    private enum ShootType
    {
        normal,
        jack,
        queen,
        king
    }

    private ShootType now_shoot = ShootType.normal;

    private int bullet_count = 0;
    public float bullet_speed = 3f;
    private float bullet_power = 1;
    private int joker_stack = 0;

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isReady && Input.GetMouseButtonDown(0))
        {
            Type_Shoot(now_shoot);
            isReady = false;
            StartCoroutine(Shoot_Delay());
        }
    }

    void Type_Shoot(ShootType now)
    {
        GameObject go = null; // 발사될 총알을 담을 변수

        switch (now)
        {
            case ShootType.normal:
                go = Instantiate(normal_bullet, pos.position, Quaternion.identity);
                break;
            case ShootType.jack:
                go = Instantiate(jack_bullet, pos.position, Quaternion.identity);
                if (joker_stack < 3)
                    joker_stack++; // ✅ 조커 스택 증가
                break;
            case ShootType.queen:
                go = Instantiate(queen_bullet, pos.position, Quaternion.identity);
                if (joker_stack < 3)
                    joker_stack++; // ✅ 조커 스택 증가
                break;
            case ShootType.king:
                go = Instantiate(king_bullet, pos.position, Quaternion.identity);
                if (joker_stack < 3)
                    joker_stack++; // ✅ 조커 스택 증가
                break;
        }

        if (go != null)
        {
            go.transform.Translate(Vector3.up * bullet_speed * Time.deltaTime);
        }
    }

    IEnumerator Shoot_Delay()
    {
        yield return new WaitForSeconds(shoot_delay);
        isReady = true;
    }
}
