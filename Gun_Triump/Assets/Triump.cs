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

    void Start() { }

    void Update() { }

    void Fire()
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


    void Type_Shoot(ShootType now, )
    {
        if(now == normal)
        {
            GameObject go = Instantiate(normal_bullet, pos.position, Quaternion.identity);
        }
        else if(now == jack)
        {
            GameObject go = Instantiate(jack_bullet, pos.position, Quaternion.identity);
            if(joker_stack<3) joker_stack++;           
        }
        else if(now == queen)
        {
            GameObject go = Instantiate(queen_bullet, pos.position, Quaternion.identity);
            if(joker_stack<3) joker_stack++;           
        }
        else if(now == king)
        {
            GameObject go = Instantiate(king_bullet, pos.position, Quaternion.identity);         
            if(joker_stack<3) joker_stack++;           
        }

        go.transform.Translate(Vector3.Up * bullet_speed * Time.deltaTime);
    }

    IEnumerator Shoot_Delay()
    {
        yield return new WaitForSeconds(shoot_delay);
        isReady = true;
    }
}
