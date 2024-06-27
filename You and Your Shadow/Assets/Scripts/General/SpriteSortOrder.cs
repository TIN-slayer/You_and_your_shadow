using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General
{
    public class SpriteSortOrder : MonoBehaviour
    {
        private static int order = 0;
        // Start is called before the first frame update
        void Start()
        {
            GetComponent<SpriteRenderer>().sortingOrder = order++;
        }
    }
}
