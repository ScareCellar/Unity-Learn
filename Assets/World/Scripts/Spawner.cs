using UnityEngine;
using UnityEngine.InputSystem;

public class Spawner : MonoBehaviour
{
    [SerializeField] float spawnTime = 1.0f;
    [SerializeField] GameObject spawnObject;
    private void Awake()
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(spawnObject, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime -= Time.deltaTime;
        if (Keyboard.current.spaceKey.isPressed)
        {
            var go = Instantiate(spawnObject, transform.position, transform.rotation);
            spawnTime = 1.0f;

            Destroy(go, 4);
        }
    }
}
