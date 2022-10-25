using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public static class CustomExtension
    {
        public static Vector3 RandomPosition(float generationRadius, float generationHeight)
        {
            Vector3 randomPos = Random.insideUnitCircle * generationRadius;
            randomPos.z = randomPos.y;
            randomPos.y = generationHeight;
            return randomPos;
        }
        
        public static void NormalizeToOne(Vector3 inVec3, out Vector3 outVec3, out float totalValue)
        {
        totalValue = Mathf.Abs(inVec3.x) + Mathf.Abs(inVec3.y) + Mathf.Abs(inVec3.z);
        outVec3 = inVec3 / totalValue;
        }
    }

