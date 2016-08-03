using UnityEngine;
using System.Collections;

namespace itsybitsy
{
    public enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }


    public class Panning : MonoBehaviour
    {
        [SerializeField]
        private Direction direction;

        [SerializeField]
        private Transform target;

        [SerializeField]
        private float speed;
        
        void Start()
        {
            switch (direction)
            {
                case Direction.UP: StartCoroutine("StartPanning", Vector2.up); break;
                case Direction.DOWN: StartCoroutine("StartPanning", Vector2.down); break;
                case Direction.LEFT: StartCoroutine("StartPanning", Vector2.left); break;
                case Direction.RIGHT: StartCoroutine("StartPanning", Vector2.right); break;
                default: break;
            }           
        } 

        private IEnumerator StartPanning(Vector2 vectorDirection)
        {
            while (true)
            {
                Vector2 newPos = target.localPosition;
                newPos += vectorDirection * speed;
                target.localPosition = newPos;
                yield return null;
            }
        }
       
    }
}
