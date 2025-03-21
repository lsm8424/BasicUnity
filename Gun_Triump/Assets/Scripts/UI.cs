using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    public TMP_Text spade_stack;
    public TMP_Text joker_stack;
    public TMP_Text loyalty;
    public TMP_Text fatal;

    public static UI Instance { get; private set; }

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

    void Start() { }

    void Update()
    {
        if (Triump.Instance.loyalty == true)
        {
            loyalty.gameObject.SetActive(true);
        }
        else if (Triump.Instance.loyalty == false)
        {
            loyalty.gameObject.SetActive(false);
        }

        if (Triump.Instance.fatal == true)
        {
            loyalty.gameObject.SetActive(true);
        }
        else if (Triump.Instance.fatal == false)
        {
            loyalty.gameObject.SetActive(false);
        }
    }
}
