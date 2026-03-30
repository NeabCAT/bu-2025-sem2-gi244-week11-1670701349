using UnityEngine;
using UnityEngine.InputSystem;

public class StunPowerUp : MonoBehaviour
{
    public float stunDuration = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
            foreach (Enemy enemy in enemies)
            {
                enemy.StartStun(stunDuration);
            }

            Destroy(gameObject);
        }
    }
}
