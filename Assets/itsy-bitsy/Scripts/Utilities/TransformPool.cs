using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace itsybitsy.Utilities
{
    public class TransformPool : MonoBehaviour {
       
        private Dictionary<string, List<Transform>> poolDictionary = new Dictionary<string, List<Transform>>();
        	   
	    public void Deactivate(Transform transform, bool includeChildren)
	    {
            if (includeChildren)
            {
                foreach (Transform child in transform)
                Deactivate(child, true);
            }

            transform.gameObject.SetActive(false);
		    if (transform.GetComponent<Renderer>() != null)
			    transform.GetComponent<Renderer>().enabled = false;
	    }
	
	    private void Activate(Transform transform, bool includeChildren)
	    {
            if (includeChildren)
            {
                foreach (Transform child in transform)
                Activate(child, true);
            }

            transform.gameObject.SetActive(true);
		    if (transform.GetComponent<Renderer>() != null)
			    transform.GetComponent<Renderer>().enabled = true;
	    }
	
	    public Transform getTransform(string key, bool includeChildren = default(bool))
	    {	
            if (poolDictionary.ContainsKey(key))
            {
                if (poolDictionary[key].Count > 0)
                {
                    Transform item = poolDictionary[key][0];
                    Activate(item, includeChildren);
                    poolDictionary[key].RemoveAt(0);
                    return item;
                }          
            }

            //Transform New = Instantiate(transform) as Transform;
            //return New;
            Debug.LogWarning("Not enough " + key + "s in pool");
            return null;
	    }
	
	    public void returnTransform(Transform transform, bool includeChildren = default(bool))
	    {
            Deactivate(transform, includeChildren);

            if (poolDictionary.ContainsKey(transform.name))
            {
                poolDictionary[transform.name].Add(transform);
            }
            else
            {
                List<Transform> newList = new List<Transform>(){transform};
                poolDictionary.Add(transform.name, newList);
            }
            
            Debug.LogWarning("Returned to" + transform.name + " pool - current count: " + poolDictionary[transform.name].Count);            
	    }	  
    }
}

