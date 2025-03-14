using UnityEngine;

public class Boss_Head : MonoBehaviour
{
    [SerializeField]
    GameObject bossbullet;

    public void LeftDownLaunch()
    {
        GameObject go = Instantiate(bossbullet, transform.position, Quaternion.identity);

        go.GetComponent<Boss_Bullet>().Move(new Vector2(-1, -1));
    }

    public void RightDownLaunch()
    {
        GameObject go = Instantiate(bossbullet, transform.position, Quaternion.identity);

        go.GetComponent<Boss_Bullet>().Move(new Vector2(1, -1));
    }

    public void DownLaunch()
    {
        GameObject go = Instantiate(bossbullet, transform.position, Quaternion.identity);

        go.GetComponent<Boss_Bullet>().Move(new Vector2(0, -1));
    }
}
