using UnityEngine;
using UnityEngine.Rendering;

public class PlayerAnimEvents : MonoBehaviour
{
    private Player player;

    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    void Update() { }

    public void AnimationTrigger()
    {
        player.AttackOver();
    }
}
