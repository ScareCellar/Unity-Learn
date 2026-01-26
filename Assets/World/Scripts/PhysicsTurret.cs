using System.Runtime.CompilerServices;
using UnityEngine;

public class PhysicsTurret : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 45.0f;
    [SerializeField] float fireRateMin = 1.0f;
    [SerializeField] float fireRateMax = 2.0f;
    [SerializeField] float recoil = 200.0f;
    [SerializeField] Ammo ammo;
    [SerializeField] Transform muzzle;

    private Rigidbody rb;
    private float fireRate;
    float fireTimer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fireRate = Random.Range(fireRateMin, fireRateMax);
        fireTimer = fireRate;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0)
        {
            fireTimer = fireRate;
            Instantiate(ammo, muzzle.position, muzzle.rotation);
            rb.AddRelativeForce(Vector3.back * recoil);
        }

        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
