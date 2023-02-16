using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGen : MonoBehaviour
{
    public float distanceBetween;
    public float distanceBetweenMin;
    public float distanceBetweenMax;
    public float maxHeightChange;

    //Random Point Threshold
    public float RPTsmallPoints;
    public float RPTsmallHealth;
    public float RPTfastPowerUP;
    public float RPTgroundObstacle;

    public GameObject thePlatform;
    //public GameObject[] thePlatforms;
    public ObjectPooler[] theObjectPools;
    public Transform generationPoint;
    public Transform maxHeightPoint;
    public PointGen thePointGenerator;

    private float minHeight;
    private float maxHeight; 
    private float heightChange;
    private float platformWidth;
    private float[] platformWidths;
    
    private int platformSelector;

    // Start is called before the first frame update
    void Start()
    {
        //platformWidth = thePlatform.GetComponent<BoxCollider2D>().size.x;
        platformWidths = new float[theObjectPools.Length];
        
        for(int i=0; i<theObjectPools.Length; i++)
        {
            platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }

        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;

        thePointGenerator = FindObjectOfType<PointGen>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < generationPoint.position.x)
        {
            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);

            heightChange = transform.position.y + Random.Range(maxHeightChange, -maxHeightChange);

            if(heightChange > maxHeight)
            {
                heightChange = maxHeight;
            }
            else if(heightChange < minHeight)
            {
                heightChange = minHeight;
            }

            platformSelector = Random.Range(0, theObjectPools.Length);

            //go in front of the platform
            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector]/2) + distanceBetween, heightChange, transform.position.z);

            //Instantiate(theObjectPools[platformSelector], transform.position, transform.rotation);
            GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject();

            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);

            if(Random.Range(0f, 100f) < RPTsmallPoints)
            {
                thePointGenerator.SpawnSmallPoints(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));
            }
            else if(Random.Range(0f, 100f) < RPTgroundObstacle)
            {
                    thePointGenerator.SpawnGroundObstacle(new Vector3(transform.position.x, transform.position.y+0.5f, transform.position.z));
            }
           else
           {
                if(Random.Range(0f, 100f) < RPTsmallHealth)
                {
                    thePointGenerator.SpawnSmallHealth(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));
                }
                else if(Random.Range(0f, 100f) < RPTfastPowerUP)
                {
                    thePointGenerator.SpawnGoFastPowerUp(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));
                }
           }

            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector]/2), transform.position.y, transform.position.z);
            
        }
    }
}
