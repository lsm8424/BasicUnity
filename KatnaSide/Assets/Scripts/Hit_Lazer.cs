using UnityEngine;

public class Hit_Lazer : MonoBehaviour
{
    float Speed = 50f;
    Vector2 MousePos;
    Transform tr;
    Vector3 dir;

    float angle;
    Vector3 dirNo;

    void Start()
    {
        tr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        MousePos = Input.mousePosition;
        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 Pos = new Vector3(MousePos.x, MousePos.y, 0);
        dir = Pos - tr.position;

        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        dirNo = new Vector3(dir.x, dir.y, 0).normalized;

        Destroy(gameObject, 4f);
    }

    void Update()
    {
        //이동
        transform.rotation = Quaternion.Euler(0, 0, angle);

        transform.position += dirNo * Speed * Time.deltaTime;
    }
}
