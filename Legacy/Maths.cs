using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

namespace Curookie.Util {
    
    [System.Serializable]
    public class FloatMinMax
    {
        public float min;
        public float max;

        public float GetValue()
        {
            return UnityEngine.Random.Range(min, max);
        }
    }

    [System.Serializable]
    public class IntMinMax
    {
        public int min;
        public int max;

        public int GetValue()
        {
            return UnityEngine.Random.Range(min, max + 1);
        }
    }

    public static class Maths {
        public static Vector3 EulerTo180Degree(Vector3 eulerDegree) {
            var resultDegree = eulerDegree;
            if (eulerDegree.x > 180f) {
                resultDegree.x -= 360f;
            }
            if (eulerDegree.y > 180f) {
                resultDegree.y -= 360f;
            }
            if (eulerDegree.z > 180f) {
                resultDegree.z -= 360f;
            }
            return resultDegree;
        }

        public static Vector3 RoundPosition(Vector3 position) {
            position.x = Mathf.Round(position.x * 10.0f) / 10.0f;
            position.y = Mathf.Round(position.y * 10.0f) / 10.0f;
            position.z = Mathf.Round(position.z * 10.0f) / 10.0f;
            return position;
        }

        public static int Mod(int x, int m)
        {
            return (x % m + m) % m;
        }

        public static float Mod(float x, int m)
        {
            return (x % m + m) % m;
        }

        public static bool IsInRange(float x, float a, float b)
        {
            return x >= a && x <= b;
        }

        public static bool IsOutRange(float x, float a, float b)
        {
            return x < a || x > b;
        }

        /// <summary>
        /// Random List Elements - Fisher-Yates Algorithm
        /// </summary>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Int32 seed) {
            if (source == null) throw new ArgumentNullException("source");

            List<T> buffer = source.ToList();

            System.Random random = seed!=-1 ? new System.Random(seed) : new System.Random();

            Int32 count = buffer.Count;

            for (Int32 i = 0; i < count; i++) {          
                Int32 j = random.Next(i, count);
                yield return buffer[j];          
                buffer[j] = buffer[i];
            }
        }

        /// <summary>
        /// Random List Elements - Fisher-Yates Algorithm
        /// </summary>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, System.Random rng) {
            if (source == null) throw new ArgumentNullException("source");

            List<T> buffer = source.ToList();

            System.Random random = rng ?? new System.Random();

            Int32 count = buffer.Count;

            for (Int32 i = 0; i < count; i++) {          
                Int32 j = random.Next(i, count);
                yield return buffer[j];          
                buffer[j] = buffer[i];
            }
        }

        public static int RandomRange(int inclusiveMin, int exclusiveMax, System.Random rng = null) {
            rng??=new System.Random();
            return rng.Next(inclusiveMin, exclusiveMax);
        }

        public static float RandomRange(float inclusiveMin, float exclusiveMax, System.Random rng = null) {
            rng??=new System.Random();
            return Mathf.Lerp(inclusiveMin, exclusiveMax, Convert.ToSingle(rng.NextDouble()));
        }
    }
}
