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

    public bool isReady = true;
    public bool draw = false;
    public float shoot_delay = 0.5f;

    private enum ShootType
    {
        Normal,
        Jack,
        Queen,
        King
    }

    private enum DrawType
    {
        Jack = 0,
        Queen = 1,
        King = 2
    }

    private ShootType currentShootType = ShootType.Normal;
    private DrawType currentDrawType = DrawType.Jack;

    private int spadeStack = 0;
    private int jokerStack = 0;
    public float bulletSpeed = 10f;

    void Update()
    {
        HandleShooting();
        HandleSpadeDrawing();
    }

    // 발사를 처리하는 함수
    void HandleShooting()
    {
        if (isReady && Input.GetMouseButtonDown(0))
        {
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0;

            FireBullet(currentShootType, targetPosition);
            isReady = false;
            StartCoroutine(ShootDelay());
        }
    }

    // 현재 발사 타입에 맞는 총알을 발사하는 함수
    void FireBullet(ShootType shootType, Vector3 targetPosition)
    {
        GameObject bulletPrefab = GetBulletPrefab(shootType);

        if (bulletPrefab != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, pos.position, Quaternion.identity);
            Vector3 direction = (targetPosition - pos.position).normalized;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.linearVelocity = direction * bulletSpeed;
        }

        UpdateStack(shootType);
    }

    // 발사 타입에 맞는 총알 프리팹을 반환하는 함수
    GameObject GetBulletPrefab(ShootType shootType)
    {
        switch (shootType)
        {
            case ShootType.Normal:
                return normal_bullet;
            case ShootType.Jack:
                return jack_bullet;
            case ShootType.Queen:
                return queen_bullet;
            case ShootType.King:
                return king_bullet;
            default:
                return null;
        }
    }

    // 스페이드나 조커 스택을 업데이트하는 함수
    void UpdateStack(ShootType shootType)
    {
        switch (shootType)
        {
            case ShootType.Normal:
                if (spadeStack < 10 && !draw)
                    spadeStack++;
                break;
            case ShootType.Jack:
            case ShootType.Queen:
            case ShootType.King:
                if (jokerStack < 3)
                    jokerStack++;
                break;
        }
    }

    // 스페이드 드로우 관련 처리 함수
    void HandleSpadeDrawing()
    {
        if (spadeStack == 10)
        {
            draw = true;
            spadeStack = 0;
        }

        if (draw)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CycleDrawType();
            }

            if (Input.GetMouseButtonDown(1))
            {
                SetShootTypeBasedOnDraw();
                draw = false;
            }
        }
    }

    // 드로우 타입을 순차적으로 변경하는 함수
    void CycleDrawType()
    {
        currentDrawType = (DrawType)(((int)currentDrawType + 1) % 3);
    }

    // 드로우 타입에 맞는 발사 타입을 설정하는 함수
    void SetShootTypeBasedOnDraw()
    {
        currentShootType = (ShootType)currentDrawType;
    }

    // 발사 후 대기 시간 처리
    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(shoot_delay);
        isReady = true;
    }
}
