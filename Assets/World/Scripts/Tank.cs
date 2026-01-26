using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Tank : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;
    [SerializeField] float rotationSpeed = 90.0f; // rotation in degrees per second

    [SerializeField] GameObject ammo;
    [SerializeField] Transform muzzle;
    [SerializeField] float shootCooldown;
    [SerializeField] GameObject fireEffect;

    [SerializeField] Slider healthBar;

    private float shootTimer;

    InputAction moveAction;
    InputAction lookAction;
    InputAction attackAction;

    Health health;
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");
        attackAction = InputSystem.actions.FindAction("Attack");

        attackAction.started += ctx => OnAttack();

        health = GetComponent<Health>();

        shootTimer = shootCooldown;
    }

    void Update()
    {
        shootTimer -= Time.deltaTime;
        // direction (forward/backward movement)
        float direction = moveAction.ReadValue<Vector2>().y;
        //if (Keyboard.current.wKey.isPressed) direction = +1.0f;
        //if (Keyboard.current.sKey.isPressed) direction = -1.0f;

        // translate (move) the tank in the forward direction
        // moves the tank in the relative direction (direction tank is facing)
        transform.Translate(Vector3.forward * direction * speed * Time.deltaTime);

        // rotation (left/right)
        //if (Keyboard.current.aKey.isPressed) rotation = -1.0f;
        //if (Keyboard.current.dKey.isPressed) rotation = +1.0f;
        float rotation = moveAction.ReadValue<Vector2>().x;

        // rotate the tank, around the up axis (y-axis)
        transform.Rotate(Vector3.up * rotation * rotationSpeed * Time.deltaTime);

        // check if "Fire" key is pressed, if so instantiate the ammo (rocket)
        // ammo is instantiate at the muzzle position and rotation
        //if (attackAction.WasPerformedThisFrame())
        //{
        //    Instantiate(ammo, muzzle.transform.position, muzzle.transform.rotation);
        //}
        healthBar.value = health.CurrentHealthPercentage;
        
    }

    void OnAttack()
    {
        if (shootTimer <= 0)
        {
            Instantiate(ammo, muzzle.transform.position, muzzle.rotation);
            Destroy(ammo, 5);
            if (fireEffect != null) Instantiate(fireEffect, muzzle.transform.position, muzzle.rotation);
            shootTimer = shootCooldown;
        }
    }
}
