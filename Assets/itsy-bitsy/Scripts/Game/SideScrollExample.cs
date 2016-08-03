using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using itsybitsy;
using itsybitsy.Utilities;

public class SideScrollExample : LevelGenerator
{
    [SerializeField]
    private Transform platformPrefab;
    [SerializeField]
    public Transform prefabParent;
    [SerializeField]
    private bool clearOnPlay;
    
    private List<char> binaryList = new List<char>();
 
    // Use this for initialization
    public override void Start()
    {
        if (clearOnPlay)
        {
            base.ClearPlatform(prefabParent);
            binaryList.Clear();
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
        string[] platformData = new string[bits.Length];

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
        ///initializes parsed data string 0's and 1's
        ///0 = 'space'; 1 = '_'               
        Transform prevPlatform = null;
        Vector2 prevPos = Vector2.zero;
        string output = "";

        foreach (char c in binaryList)
        {
            if (c == '0' ? true : false)
            {
                output += "_";

                Transform newPlatform = Instantiate(platformPrefab) as Transform;
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
                output += " ";

                if (prevPlatform)
                    prevPos.x = prevPos.x + platformPrefab.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
            }
        }

        Debug.Log(output);
    }        
}
