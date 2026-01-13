using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 45.0f;
    [SerializeField] float fireRate = 2.0f;
    [SerializeField] Ammo ammo;
    [SerializeField] Transform muzzle;

    float fireTimer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fireTimer = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        fireTimer -= Time.deltaTime;
        if(fireTimer <= 0)
        {
            fireTimer = fireRate;
            Instantiate(ammo, muzzle.position, muzzle.rotation);
        }

        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
