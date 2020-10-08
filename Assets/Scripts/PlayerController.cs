using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{
    public BulletManagerController bulletManager;

    private Rigidbody2D m_rigidBody;
    private Vector3 finalTouch;
    public float lerpTValue;

    public float horizontalBoundary;
    public float maxSpeed;
    public float horizontalSpeed;

    // Start is called before the first frame update
    void Start()
    {
        finalTouch = new Vector3();

        if (GetComponent<Rigidbody2D>() != null)
            m_rigidBody = GetComponent<Rigidbody2D>();

    }
    private void Move()
    {
        float direction = 0.0f;
        foreach (var touch in Input.touches)
        {
            var worldTouch = Camera.main.ScreenToWorldPoint(touch.position);

            if (worldTouch.x >= gameObject.transform.position.x)
            {
                direction = 1.0f;
            }
            else if (worldTouch.x <= gameObject.transform.position.x)
            {
                direction = -1.0f;
            }

            finalTouch = worldTouch;
        }


        if (Input.GetAxis("Horizontal") >= 0.1f)
        {
            // Move right
            direction = 1.0f;
        }
        if (Input.GetAxis("Horizontal") <= -0.1f)
        {
            // Move left
            direction = -1.0f;
        }

        Vector2 newVelocity = m_rigidBody.velocity + new Vector2(direction * horizontalSpeed, 0.0f);

        // Prevent the player from moving past max speed:
        m_rigidBody.velocity = Vector2.ClampMagnitude(newVelocity, maxSpeed);
        m_rigidBody.velocity *= 0.99f;

        if (finalTouch.x != 0.0f)
        {
            gameObject.transform.position = new Vector2(Mathf.Lerp(gameObject.transform.position.x, finalTouch.x, lerpTValue), gameObject.transform.position.y);
        }

    }
    private void CheckBounds()
    {
        if (gameObject.transform.position.x >= horizontalBoundary)
        {
            gameObject.transform.position = new Vector2(horizontalBoundary, gameObject.transform.position.y);
        }
        else if (gameObject.transform.position.x <= -horizontalBoundary)
        {
            gameObject.transform.position = new Vector2(-horizontalBoundary, gameObject.transform.position.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move(); 
        CheckBounds();
        _FireBullet();
        //Debug.Log("hello");
    }
     
    private void _FireBullet()
    {
        if (Time.frameCount % 40 == 0)
        {
            bulletManager.GetBullet(gameObject.transform.position);
        }
    }
}
