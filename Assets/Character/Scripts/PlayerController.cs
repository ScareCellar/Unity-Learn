using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    IMovable movable;
    IAttackable attackable;
    IJumpable jumpable;
    ISprintable sprintable;

    private void Awake()
    {
        
    }
    void OnJump()
    {
        jumpable?.Jump();
    }

    void OnMove(InputValue value)
    {
        movable?.Move(value.Get<Vector2>());
    }

    void OnAttack()
    {
        attackable?.Attack();
    }

    void OnSprint()
    {
        sprintable?.StartSprint();
    }
}
