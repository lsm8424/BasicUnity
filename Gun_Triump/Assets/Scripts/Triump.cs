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

    void Start() { }

    void Update() { }

    void Shoot()
    {
        if (isReady != false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                switch (now_shoot)
                {
                    case ShootType.normal:
                        Normal_Shoot();
                        break;
                    case ShootType.jack:
                        Jack_Shoot();
                        break;
                    case ShootType.queen:
                        Queen_Shoot();
                        break;
                    case ShootType.king:
                        King_Shoot();
                        break;
                }
                isReady = false;
                StartCoroutine(Shoot_Delay());
            }
        }
    }

    void Normal_Shoot()
    {
        GameObject go = Instantiate(normal_bullet, pos.position, Quaternion.identity);
    }

    void Jack_Shoot() { }

    void Queen_Shoot() { }

    void King_Shoot() { }

    IEnumerator Shoot_Delay()
    {
        yield return new WaitForSeconds(shoot_delay);
    }
}
