using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour, IItem, IEquipable, IUseable
{
    [SerializeField] Ammo ammo;
    [SerializeField] Transform muzzle;
    [SerializeField] float fireRate = 1.0f;

    [SerializeField] int maxAmmoCount = 20;

    private int ammoCount;
    public int AmmoCount { 
        get { 
            return ammoCount; 
        } 
        set {
            ammoCount = Mathf.Clamp(value, 0, maxAmmoCount);
        } 
    }

    public bool IsReadyToFire { get; set; } = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void OnFire()
    {
        if (IsReadyToFire && AmmoCount > 0)
        {
            AmmoCount--;
            Instantiate(ammo, muzzle.position, muzzle.rotation);
            IsReadyToFire = false;
            StartCoroutine(ResetFireCR());
        }
    }

    IEnumerator ResetFireCR()
    {
        yield return new WaitForSeconds(fireRate);
        IsReadyToFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPickup(Actor owner)
    {
        throw new System.NotImplementedException();
    }

    public void OnDrop()
    {
        throw new System.NotImplementedException();
    }

    public void OnEquip(Actor owner)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnequip(Actor owner)
    {
        throw new System.NotImplementedException();
    }

    public void OnUse(Actor owner)
    {
        throw new System.NotImplementedException();
    }
}
