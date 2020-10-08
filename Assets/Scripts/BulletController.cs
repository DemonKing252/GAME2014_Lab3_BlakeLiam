using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float verticalSpeed;
    public BulletManagerController bulletManager;
    public float verticalBoundary;
    // Start is called before the first frame update
    void Start()
    {
        bulletManager = FindObjectOfType<BulletManagerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        _Reset();
    }
    private void Move()
    {
        gameObject.transform.position += new Vector3(0.0f, verticalSpeed * Time.deltaTime, 0.0f);
    }
    private void _Reset()
    {
        if (gameObject.transform.position.y > verticalBoundary)
        {
            // Put the bullet back into the queue, and set its visibility to be false until we are ready to use it
            bulletManager.ReturnBullet(gameObject);
        }
    }
}
