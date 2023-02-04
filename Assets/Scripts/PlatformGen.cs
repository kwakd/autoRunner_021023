using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGen : MonoBehaviour
{
    public float distanceBetween;
    public float distanceBetweenMin;
    public float distanceBetweenMax;

    public GameObject thePlatform;
    public GameObject[] thePlatforms;
    public Transform generationPoint;
    //public ObjectPooler theObjectPool;

    private float platformWidth;
    private float[] platformWidths;
    
    private int platformSelector;


    // Start is called before the first frame update
    void Start()
    {
        //platformWidth = thePlatform.GetComponent<BoxCollider2D>().size.x;
        platformWidths = new float[thePlatforms.Length];
        
        for(int i=0; i<thePlatforms.Length; i++)
        {
            platformWidths[i] = thePlatforms[i].GetComponent<BoxCollider2D>().size.x;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < generationPoint.position.x)
        {
            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);

            platformSelector = Random.Range(0, thePlatforms.Length);

            //go in front of the platform
            transform.position = new Vector3(transform.position.x + platformWidths[platformSelector] + distanceBetween, transform.position.y, transform.position.z);

            Instantiate(thePlatforms[platformSelector], transform.position, transform.rotation);
            /*
            GameObject newPlatform = theObjectPool.GetPooledObject();

            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);
            */
        }
    }
}
