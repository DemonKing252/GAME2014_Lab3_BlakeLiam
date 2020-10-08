using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletManagerController : MonoBehaviour
{
    public GameObject bullet;
    public int maxBullets;

    private Queue<GameObject> m_bulletPool;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Bullet manager init!");
        BuildBulletPool();
    }
    public void Update()
    {
        Debug.Log(m_bulletPool.Count.ToString());
    }

    private void BuildBulletPool()
    {
        m_bulletPool = new Queue<GameObject>();

        for (int i = 0; i < maxBullets; i++)
        {
            GameObject tempBullet = Instantiate(bullet);
            tempBullet.SetActive(false);
            tempBullet.transform.parent = transform;
            m_bulletPool.Enqueue(tempBullet);
        }

    }
    public GameObject GetBullet(Vector3 position)
    {
        var newBullet = m_bulletPool.Dequeue(); // Return a referece to the first bullet g.o in bulletPool
        newBullet.SetActive(true);
        newBullet.transform.position = position;
        return newBullet;
    }
    public void ReturnBullet(GameObject returnBullet)
    {
        returnBullet.SetActive(false);
        m_bulletPool.Enqueue(returnBullet); // Puts the bullet back into the pool
    }

}
