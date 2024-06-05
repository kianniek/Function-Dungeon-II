using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Table
{
    public static class TableHelper
    {
        /// <summary>
        /// Generates a list of random indices. With a given amount of indices to return and the length of the array.
        /// </summary>
        /// <param name="amountOfIndicesToReturn">Number of random indices to generate.</param>
        /// <param name="lengthOfArray">Length of the array to generate indices for.</param>
        /// <returns>A list of random indices.</returns>
        public static IEnumerable<int> GenerateRandomIndices(int amountOfIndicesToReturn, int lengthOfArray)
        {
            if (amountOfIndicesToReturn > lengthOfArray)
            {
                Debug.LogWarning("The amount of indices to return cannot be greater than the length of the array.");
            }
            
            return Enumerable.Range(0, lengthOfArray).OrderBy(x => Random.Range(0, lengthOfArray)).Take(amountOfIndicesToReturn);
        }
    }
}
