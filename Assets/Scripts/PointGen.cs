using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointGen : MonoBehaviour
{

    public ObjectPooler smallPointPool;
    public ObjectPooler smallHealthPool;
    public ObjectPooler goFastPowerUpPool;

    public float distanceBetweenPoints;

    public void SpawnSmallPoints(Vector3 startPosition)
    {
        //middle of platform
        GameObject smallPoint1 = smallPointPool.GetPooledObject();
        smallPoint1.transform.position = startPosition;
        smallPoint1.SetActive(true);

        //left side of platform
        // GameObject smallPoint2 = smallPointPool.GetPooledObject();
        // smallPoint2.transform.position = new Vector3(startPosition.x - distanceBetweenPoints, startPosition.y, startPosition.z);
        // smallPoint2.SetActive(true);

        //right side of platform
        // GameObject smallPoint3 = smallPointPool.GetPooledObject();
        // smallPoint3.transform.position = new Vector3(startPosition.x + distanceBetweenPoints, startPosition.y, startPosition.z);
        // smallPoint3.SetActive(true);
    }

    public void SpawnSmallHealth(Vector3 startPosition)
    {
        GameObject smallPoint1 = smallHealthPool.GetPooledObject();
        smallPoint1.transform.position = startPosition;
        smallPoint1.SetActive(true);

    }

    public void SpawnGoFastPowerUp(Vector3 startPosition)
    {
        GameObject smallPoint1 = goFastPowerUpPool.GetPooledObject();
        smallPoint1.transform.position = startPosition;
        smallPoint1.SetActive(true);

    }
}
