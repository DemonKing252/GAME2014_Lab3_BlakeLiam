using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
File: BackgroundController.cs
Author: Liam Blake
Created: 2020-10-07
Modified: 2020-10-07
Desc:
    Demonstrates the use of object pooling (Lab #3).

*/
public class BackgroundController : MonoBehaviour
{
    [SerializeField]
    float maxSpeed = 5.0f;

    [SerializeField]
    float verticalBoundary = 10.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void Move()
    {
        Vector3 pos = gameObject.transform.position;

        pos.y -= maxSpeed * Time.deltaTime;
        gameObject.transform.position = pos;

        CheckBounds();
    }
    private void CheckBounds()
    {
        if (gameObject.transform.position.y <= -verticalBoundary)
        {
            _Reset();
        }
    }
    private void _Reset()
    {
        gameObject.transform.position += new Vector3(0.0f, +verticalBoundary * 2.0f, 0.0f);
    }
}
