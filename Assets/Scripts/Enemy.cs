using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody rb;
    private GameObject player;
    private bool isStun = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isStun) return;

        Vector3 dir = player.transform.position - transform.position;
        dir.Normalize();
        rb.AddForce(dir * speed);
    }

    public void StartStun(float duration)
    {
        StartCoroutine(Stun(duration));
    }

    private IEnumerator Stun(float duration)
    {
        isStun = true;
        rb.linearVelocity = Vector3.zero;

        yield return new WaitForSeconds(duration);

        isStun = false;
    }


}
