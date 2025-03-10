using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float speed = 5.0f;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        transform.position += move * speed * Time.deltaTime;
    }
}
