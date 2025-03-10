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
        Vector2 newoffset = thisMaterial.mainTextureOffset;
        newoffset.Set(0, newoffset.y + (scrollSpeed * Time.deltaTime));
        thisMaterial.mainTextureOffset = newoffset;
    }
}
