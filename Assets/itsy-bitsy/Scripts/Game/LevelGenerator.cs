using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using itsybitsy.Utilities;

namespace itsybitsy
{
    [System.Serializable]
    public enum Difficulty
    {
        EASY,
        MEDIUM,
        HARD
    }
    
    public class LevelGenerator : MonoBehaviour
    {
        /* DYNAMIC LEVEL GENERATOR */
        /* Step 1: Input from user 
         * Step 2: Generate hash encryption of Input string then convert to binary
         * Step 3: Parse Binary with desired level design
         * Step 4: Initialize Game Asset with object pooling */

        #region step 1

        [SerializeField]
        private string input;

        [SerializeField]
        private Difficulty difficulty;

        [SerializeField]
        private bool generateHash;
                     
        #endregion       
        
        // Use this for initialization
        public virtual void Start()
        {
            if (string.IsNullOrEmpty(input)) return;         
            ParseData(PrepareParseData());
        }

        #region step 2

        public virtual byte[] PrepareParseData()
        {
            string newInput = input;

            if (generateHash)
            {
                string hash = BitUtility.GenerateHash(newInput);
                newInput = hash;
            }
            
            switch (this.difficulty)
            {
                case Difficulty.EASY: newInput = newInput.Substring(0, newInput.Length / 3); break;
                case Difficulty.MEDIUM: newInput = newInput.Substring(0, (newInput.Length / 3) * 2); break;
            }

            byte[] bytes = BitUtility.GetBytes(newInput);            

            return bytes;
        }

        #endregion

        #region step 3

        public virtual void ParseData(byte[] bytes)
        {
            Initialize();
        }            

        #endregion

        #region step 4

        public virtual void Initialize()
        {
            // put event on finish initialization here
        }

        #endregion
        
        public void ClearPlatform(Transform parent)
        {
            foreach (Transform child in parent.GetComponentsInChildren<Transform>())
            {
                if (child != parent)
                    DestroyImmediate(child.gameObject);
            }

        }
    }
}
