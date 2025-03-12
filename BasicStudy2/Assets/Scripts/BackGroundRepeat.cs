using UnityEngine;

public class BackGroundRepeat : MonoBehaviour
{
    public float scrollSpeed = 1.2f;
    private Material thisMaterial;

    void Start()
    {
        thisMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    {
        Vector2 newOffset = thisMaterial.mainTextureOffset;
        newOffset.Set(0, newOffset.y + (scrollSpeed * Time.deltaTime));
        thisMaterial.mainTextureOffset = newOffset;
    }
}
