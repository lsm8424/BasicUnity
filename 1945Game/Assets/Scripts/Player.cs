using TreeEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    //스피드
    public float moveSpeed = 5f;

    Animator ani; //애니메이터를 가져올 변수

    public GameObject[] bullet; //총알 추후 4개 배열로 만들예정
    public Transform pos = null;

    public int power = 0;

    [SerializeField]
    private GameObject powerup; //private 인스펙터에서 사용하는방법

    //레이져
    public GameObject lazer;
    public float gValue = 0;

    void Start()
    {
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        //방향키에따른 움직임
        float moveX = moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        float moveY = moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");

        // -1   0   1
        if (Input.GetAxis("Horizontal") <= -0.5f)
            ani.SetBool("left", true);
        else
            ani.SetBool("left", false);

        if (Input.GetAxis("Horizontal") >= 0.5f)
            ani.SetBool("right", true);
        else
            ani.SetBool("right", false);

        if (Input.GetAxis("Vertical") >= 0.5f)
        {
            ani.SetBool("up", true);
        }
        else
        {
            ani.SetBool("up", false);
        }

        //스페이스
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //프리팹 위치 방향 넣고 생성
            Instantiate(bullet[power], pos.position, Quaternion.identity);
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            gValue += Time.deltaTime;

            if (gValue >= 1)
            {
                GameObject go = Instantiate(lazer, pos.position, Quaternion.identity);
                Destroy(go, 3);
                gValue = 0;
            }
        }
        else
        {
            gValue -= Time.deltaTime;

            if (gValue <= 0)
            {
                gValue = 0;
            }
        }

        transform.Translate(moveX, moveY, 0);

        //캐릭터의 월드 좌표를 뷰포트 좌표계로 변환해준다.
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        viewPos.x = Mathf.Clamp01(viewPos.x); //x값을 0이상, 1이하로 제한한다.
        viewPos.y = Mathf.Clamp01(viewPos.y); //y값을 0이상, 1이하로 제한한다.
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos); //다시월드좌표로 변환
        transform.position = worldPos; //좌표를 적용한다.
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            power += 1;

            if (power >= 3)
                power = 3;
            else
            {
                //파워업
                GameObject go = Instantiate(powerup, transform.position, Quaternion.identity);
                Destroy(go, 1);
            }

            //아이템 먹은 처리
            Destroy(collision.gameObject);
        }
    }
}
