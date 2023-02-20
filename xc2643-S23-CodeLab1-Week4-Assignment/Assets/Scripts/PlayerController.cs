using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    
    private Rigidbody2D m_Rigidbody2D;
    private Vector2 m_Movement;
    private Collider2D m_Collider2D;
    public float moveSpeed = 5f;
    
    public GameObject bulletPrefab;
    public float launchPower = 300.0f;
    Vector2 lookDirection = new Vector2(-1,0);
    
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Collider2D = GetComponent<Collider2D>();
    }
    
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        m_Movement.Set(0, vertical);
        
        if(Input.GetButtonDown("Fire1"))
        {
            Launch();
        }
    }

    private void FixedUpdate()
    {
        m_Rigidbody2D.AddForce(m_Movement * moveSpeed);
        m_Rigidbody2D.velocity *= 0.99f;
    }
    
    void Launch()
    {
        GameObject projectileObject = Instantiate(
            bulletPrefab, m_Rigidbody2D.position + Vector2.left * 0.5f, Quaternion.identity
        );
        BulletController bc = projectileObject.GetComponent<BulletController>();
        bc.Launch(lookDirection, launchPower);
    }
    
}
