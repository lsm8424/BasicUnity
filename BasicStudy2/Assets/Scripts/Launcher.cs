using System.Collections;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject bullet;

    void Start()
    {
        //InvokeRepeating(함수이름, 초기지연시간, 지연할 시간)
        InvokeRepeating("Shoot", 0.5f, 0.5f);
    }

    void Shoot()
    {
        if (GameManager.instance.isStart != false)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            SoundManager.instance.PlayBulletSound();
        }
    }
}
