using UnityEngine;

public class Queen_bullet : MonoBehaviour
{
    private float speed = 15f; // 총알의 속도를 더 빠르게 설정
    private float rotateSpeed = 5f;
    private Transform target;
    private bool orbitPlayer = false;
    private float lifetime = 0f;
    public float maxLifetime = 10f;
    public float searchInterval = 0.5f;
    private float searchTimer = 0f;
    public Transform player;
    public float targetChangeDistance = 10f; // 거리 변경을 위한 변수
    private float orbitRadius = 1.5f; // 맴도는 원의 크기

    private float angleOffset;
    private bool isSecondBullet = false; // 두 번째 발인지 구분하는 변수
    private float secondBulletDelay = 1f; // 두 번째 발이 대기하는 시간 (1초 예시)
    private float secondBulletTimer = 0f; // 대기 시간을 측정하는 타이머

    public void SetTarget(GameObject enemy, bool isSecond = false)
    {
        isSecondBullet = isSecond;

        if (enemy != null)
        {
            target = enemy.transform;
            orbitPlayer = false;
        }
        else
        {
            if (Player.Instance != null)
            {
                player = Player.Instance.transform;
                orbitPlayer = true;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    void Start()
    {
        angleOffset = Random.Range(0f, 360f);
    }

    void Update()
    {
        lifetime += Time.deltaTime;
        searchTimer += Time.deltaTime;

        // 첫 번째 발은 목표를 계속 추적, 두 번째 발은 대기 후 새로운 타겟을 찾도록
        if (isSecondBullet)
        {
            secondBulletTimer += Time.deltaTime;

            if (secondBulletTimer >= secondBulletDelay)
            {
                // 두 번째 발이 대기 시간을 마쳤다면 새 목표를 찾는다
                FindNewTarget();

                // 새 목표를 찾지 못했다면 첫 번째 발과 같은 타겟을 추적
                if (target == null)
                {
                    target = FindObjectOfType<Queen_bullet>().target;
                }
            }
        }
        else
        {
            // 첫 번째 발은 바로 타겟을 추적
            if (target == null)
            {
                FindNewTarget();
            }
        }

        if (target != null)
        {
            TrackEnemy();
        }
        else if (orbitPlayer)
        {
            OrbitAroundPlayer();
            FindNewTarget(); // 회전하면서도 주변 적을 찾음
        }

        if (lifetime >= maxLifetime)
        {
            Destroy(gameObject);
        }
    }

    void TrackEnemy()
    {
        if (target == null)
            return;

        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        transform.right = direction;
    }

    void OrbitAroundPlayer()
    {
        if (player == null)
        {
            Destroy(gameObject);
            return;
        }

        float angle = Time.time * rotateSpeed + angleOffset;
        Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * orbitRadius;
        transform.position = player.position + offset;
        transform.right = offset.normalized;
    }

    void FindNewTarget()
    {
        Collider2D[] monstersInRange = Physics2D.OverlapCircleAll(
            player.position,
            targetChangeDistance
        );

        foreach (var monster in monstersInRange)
        {
            if (monster.CompareTag("Monster"))
            {
                SetTarget(monster.gameObject, isSecondBullet); // 두 번째 발이면 isSecond 매개변수를 true로 설정
                break;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            int damage = 10;
            if (Triump.Instance.fatal)
            {
                damage += 30;
            }
            collision.gameObject.GetComponent<Monster>().Damage(damage);
            Destroy(gameObject);
        }
    }
}
