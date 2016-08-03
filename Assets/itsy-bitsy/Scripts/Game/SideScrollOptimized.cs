using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using itsybitsy;
using itsybitsy.Utilities;

public class SideScrollOptimized : LevelGenerator
{
    [SerializeField]
    private Transform platformPrefab;
    [SerializeField]
    public Transform prefabParent;
    [SerializeField]
    private bool clearOnPlay;
    [SerializeField]
    private bool optimize;
    [SerializeField]
    private BoxCollider2D boxCollider;
    [SerializeField]
    private int initialLength;

    private float offsetValue;
    private float maxOffsetValue;

    private int platFormIndex = -1;
    private string[] platformData;

    private List<char> binaryList = new List<char>();
    private int currentIndex = -1;

    private TransformPool platformPool;
    private Transform prevPlatform = null;
    private Vector2 prevPos = Vector2.zero;

    // Use this for initialization
    public override void Start()
    {
        offsetValue = platformPrefab.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
        maxOffsetValue = offsetValue * 7;
        prevPlatform = null;
        prevPos = Vector2.zero;

        if (clearOnPlay)
        {
            base.ClearPlatform(prefabParent);
            base.Start();
        }
        else
        {
            if (prefabParent.childCount < 1)
                base.Start();
        }       
    }

    public override void ParseData(byte[] bytes)
    {
        BitArray[] bits = BitUtility.GetBitArray(bytes);
        platformData = new string[bits.Length];

        for (int x = 0; x < bits.Length; x++)
        {
            platformData[x] = BitUtility.BitsToString(bits[x]);
        }

        //always make sure first data has platform
        platformData[0].ToCharArray()[0] = '0';

        foreach (string pData in platformData)
        {
            foreach (char value in pData.ToCharArray())
            {
                binaryList.Add(value);
            }
        }

        base.ParseData(bytes);
    }
    
    public override void Initialize()
    {
        /// create new pool for prefab      
        if (optimize)
        {
            //TransformPool platformPool = new TransformPool(platformPrefab);        
            platformPool = gameObject.AddComponent<TransformPool>();
        }
        else initialLength = binaryList.Count;

        ///initializes parsed data string 0's and 1's
        ///0 = 'space'; 1 = '_'
               
        for (int x = 0; x < initialLength; x++)
        {
            CreatePrefab();
        }
    }        

    private void CreatePrefab()
    {
        currentIndex++;

        if (binaryList[currentIndex] == '0' ? true : false)
        {
            Transform newPlatform = null;

            if (optimize)
            {
                newPlatform = platformPool.getTransform(platformPrefab.name);
            }

            if (newPlatform == null)
            {
                newPlatform = Instantiate(platformPrefab) as Transform;
            }

            newPlatform.SetParent(prefabParent);
            newPlatform.localScale = Vector2.one;
            newPlatform.gameObject.SetActive(true);

            Vector2 targetPos = prevPos;

            if (prevPlatform)
            {
                targetPos.x = prevPos.x + (float)platformPrefab.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
            }

            newPlatform.localPosition = targetPos;
            prevPlatform = newPlatform;
            prevPos = targetPos;
        }
        else
        {
            if (prevPlatform)
                prevPos.x = prevPos.x + platformPrefab.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (optimize && platformPool != null)
        {
            platformPool.returnTransform(collider.transform);
            if (platFormIndex < binaryList.Count)
                CreatePrefab();
        }
    }
}
