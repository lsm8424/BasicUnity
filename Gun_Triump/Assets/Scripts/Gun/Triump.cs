using System.Collections;
using UnityEngine;

public class Triump : MonoBehaviour
{
    public static Triump Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //public GameObject gun;
    public GameObject normal_bullet;
    public GameObject jack_bullet;
    public GameObject queen_bullet;
    public GameObject king_bullet;

    public Transform pos = null;
    public Vector3 pot;

    public bool isReady = true;
    public bool draw = false;
    public bool loyalty = false;
    private int loyalty_remain = 3;
    public bool fatal = false;
    private int fatal_remain = 1;
    public float shoot_delay = 0.5f;

    public enum ShootType
    {
        Normal,
        Jack,
        Queen,
        King
    }

    public enum DrawType
    {
        Jack = 0,
        Queen = 1,
        King = 2
    }

    public ShootType currentShootType = ShootType.Normal;
    public DrawType currentDrawType = DrawType.Jack;

    public int spadeStack = 0;
    private int jokerStack = 0;
    private float bulletSpeed = 20f;

    void Start()
    {
        pot = transform.position;
    }

    void Update()
    {
        HandleShooting();
        HandleSpadeDrawing();
        HandleJoker();
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

            currentShootType = ShootType.Normal;
        }
    }

    // 현재 발사 타입에 맞는 총알을 발사하는 함수
    void FireBullet(ShootType shootType, Vector3 targetPosition)
    {
        if (shootType == ShootType.Queen)
        {
            FireQueenBullets();
            return;
        }

        GameObject bulletPrefab = GetBulletPrefab(shootType);

        if (bulletPrefab != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, pos.position, Quaternion.identity);
            Vector3 direction = (targetPosition - pos.position).normalized;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.linearVelocity = direction * bulletSpeed;
        }

        UpdateStack(shootType);
        CheckFatalState();
        CheckLoyaltyState();
    }

    // Queen 상태에서 두 개의 탄환 생성
    void FireQueenBullets()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Monster");
        GameObject firstTarget = null;
        GameObject secondTarget = null;
        float minDist1 = Mathf.Infinity;
        float minDist2 = Mathf.Infinity;
        Vector3 currentPos = transform.position;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(enemy.transform.position, currentPos);
            if (dist < minDist1 && dist <= 10f)
            {
                minDist2 = minDist1;
                secondTarget = firstTarget;
                minDist1 = dist;
                firstTarget = enemy;
            }
            else if (dist < minDist2 && dist <= 10f)
            {
                minDist2 = dist;
                secondTarget = enemy;
            }
        }

        for (int i = 0; i < 2; i++)
        {
            GameObject bullet = Instantiate(queen_bullet, pos.position, Quaternion.identity);
            Queen_bullet queenScript = bullet.GetComponent<Queen_bullet>();
            queenScript.SetTarget(i == 0 ? firstTarget : secondTarget ?? firstTarget);
        }
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
        switch (currentDrawType)
        {
            case DrawType.Jack:
                currentShootType = ShootType.Jack;
                break;
            case DrawType.Queen:
                currentShootType = ShootType.Queen;
                break;
            case DrawType.King:
                currentShootType = ShootType.King;
                break;
        }
    }

    //충정 상태를 갱신하는 함수
    void CheckLoyaltyState()
    {
        if (loyalty)
        {
            loyalty_remain--;

            if (loyalty_remain == 0)
            {
                StartCoroutine(ResetLoyalty());
            }
        }
    }

    IEnumerator ResetLoyalty()
    {
        yield return new WaitForEndOfFrame();
        loyalty = false;
        loyalty_remain = 3;
    }

    // 조커 드로우 관련 함수
    void HandleJoker()
    {
        if (jokerStack == 3)
        {
            FateBlessing();
            jokerStack = 0;
        }
    }

    // 운명실현 관련 함수
    void FateBlessing()
    {
        int selecting = Random.Range(0, 3);
        switch (selecting)
        {
            case 0:
                Blessing_Speed();
                break;
            case 1:
                Blessing_Protect();
                break;
            case 2:
                Blessing_Power();
                break;
        }
    }

    // 운명실현 - 신속
    void Blessing_Speed()
    {
        Debug.Log("신속실현");
    }

    // 운명실현 - 보호
    void Blessing_Protect()
    {
        Debug.Log("보호실현");
    }

    // 운명실현 - 치명
    void Blessing_Power()
    {
        fatal = true;
        Debug.Log("치명실현");
    }

    //치명 상태를 갱신 하는 함수
    void CheckFatalState()
    {
        if (fatal)
        {
            fatal_remain--;
            if (fatal_remain <= 0)
            {
                fatal = false;
                fatal_remain = 1; // 다시 초기화
            }
        }
    }

    // 발사 후 대기 시간 처리
    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(shoot_delay);
        isReady = true;
    }
}
