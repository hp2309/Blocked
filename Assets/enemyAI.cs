using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour
{
    public Transform target;
    public PlayerController player;
    public float speed = 3f;
    public float forceFactor = 2f;

    Vector3 delta;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player = target.GetComponent<PlayerController>();
    }

    void Update()
    {
        delta = target.position - this.transform.position;
        Debug.Log(delta.magnitude);
        Vector3 direction = delta.normalized;
        direction.y = 0f;
        Quaternion rotation = Quaternion.Euler(0f, Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg, 0f);
        this.transform.rotation = rotation;

        if(delta.magnitude > 2.5f)
        {
            this.transform.position += direction * speed * Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if (delta.magnitude < 2.5f)
        {
            player.pushFactor += 1f;
            Vector3 push = delta.normalized * player.pushFactor * Time.fixedDeltaTime;
            push.y = 0f;
            target.position += push;
            player.pushFactor -= 1f;
        }
    }
}
