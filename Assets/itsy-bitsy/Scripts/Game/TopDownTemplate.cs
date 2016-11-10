  using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using itsybitsy;
using itsybitsy.Utilities;

public class TopDownTemplate : LevelGenerator {

    private enum HTBits
    {
        PlatformFlag = 1,
        Shift1 = 4,
        Shift2 = 15,
    }
    

	[SerializeField]
	private Transform platformPrefab;
	[SerializeField]
	public Transform prefabParent;
	[SerializeField]
	private bool clearOnPlay;

    //private List<char> binaryList = new List<char>();
    private List<int> heightList = new List<int>();
    private List<int> platformList = new List<int>();

    // Use this for initialization
    public override void Start()
	{
		if (clearOnPlay)
		{
			base.ClearPlatform(prefabParent);
            heightList.Clear();
            platformList.Clear();
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

        for (int x = 0; x < bytes.Length; x++)
        {
            int half1 = bytes[x] & (int)HTBits.Shift2;
            int half2 = bytes[x] >> (int)HTBits.Shift1;

            int platform1 = (half1 | (int)HTBits.PlatformFlag) & ~half1;
            int platform2 = (half2 | (int)HTBits.PlatformFlag) & ~half2; 

            heightList.Add(half1);
            heightList.Add(half2);

            platformList.Add(platform1);
            platformList.Add(platform2);

            //Debug.Log(BitUtility.BitsToString(bits[x]) + " " + half1 +  " " + platform1);
            //Debug.Log(BitUtility.BitsToString(bits[x]) + " " + half2 +  " " + platform2);
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

        //overide first data to generate platform
        platformList[0] = 1;

		for (int x = 0; x < platformList.Count; x++) 
		{
			if (platformList[x] == 0 ? true : false)
			{
                output += "_";

				Transform newPlatform = Instantiate(platformPrefab) as Transform;
				newPlatform.SetParent(prefabParent);
				newPlatform.localScale = new Vector2(1, (float)heightList[x] * 0.5f);
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

		prevPos = new Vector2(0, 40);
        prevPlatform = null;

        for (int x = platformList.Count - 1; x >= 0; x--)
        {
            if (platformList[x] == 0 ? true : false)
            {
                output += "_";

                Transform newPlatform = Instantiate(platformPrefab) as Transform;
                newPlatform.SetParent(prefabParent);
                newPlatform.localScale = new Vector2(1, (float)heightList[x] * -0.5f);
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
        Debug.Log("BOTTOM: " + output);
    }        
}
