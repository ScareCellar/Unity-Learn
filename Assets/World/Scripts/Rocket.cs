using UnityEngine;
using UnityEngine.InputSystem;

public class Rocket : Ammo
{
    Rigidbody rb;
    [SerializeField] GameObject effect;
    [SerializeField] int speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.forward * speed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //checking if object has health component
        Health health = collision.gameObject.GetComponentInChildren<Health>();
        
        if (health != null)
        {
            //deal damage to hit object
            health.OnDamage(1);
        }
        else
        {
            Debug.Log($"No health found on {collision.gameObject}");
        }

            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(effect, 2.5f);
            Destroy(gameObject);
    }
}
