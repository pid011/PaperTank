using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaperTank.Game.BehaviourMovement
{
    public static class Movement
    {
        public static IEnumerator ParabolaMove(Transform transform, Vector3 start, Vector3 end, float height)
        {
            var waitForFixedUpdate = new WaitForFixedUpdate();

            height += start.y < end.y ? end.y : start.y;

            float elapsedTime = 0f;

            float gravity = Mathf.Abs(Physics.gravity.y);
            float dh = end.y - start.y;
            float mh = height - start.y;
            float ty = Mathf.Sqrt(2 * gravity * mh);

            float a = -2 * ty;
            float b = 2 * dh;
            float duration = (-a + Mathf.Sqrt((a * a) - (4 * gravity * b))) / (2 * gravity);

            float tx = -(start.x - end.x) / duration;
            float tz = -(start.z - end.z) / duration;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float x = start.x + (tx * elapsedTime);
                float y = start.y + (ty * elapsedTime) - (0.5f * gravity * elapsedTime * elapsedTime);
                float z = start.z + (tz * elapsedTime);

                transform.position = new Vector3(x, y, z);

                yield return waitForFixedUpdate;
            }

            yield break;
        }

        public static IReadOnlyList<Vector3> SimulateParabolaMove(Vector3 start, Vector3 end, float height)
        {
            var points = new List<Vector3>();

            height += start.y < end.y ? end.y : start.y;

            float elapsedTime = 0f;

            float gravity = Mathf.Abs(Physics.gravity.y);
            float dh = end.y - start.y;
            float mh = height - start.y;
            float ty = Mathf.Sqrt(2 * gravity * mh);

            float a = -2 * ty;
            float b = 2 * dh;
            float duration = (-a + Mathf.Sqrt((a * a) - (4 * gravity * b))) / (2 * gravity);

            float tx = -(start.x - end.x) / duration;
            float tz = -(start.z - end.z) / duration;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float x = start.x + (tx * elapsedTime);
                float y = start.y + (ty * elapsedTime) - (0.5f * gravity * elapsedTime * elapsedTime);
                float z = start.z + (tz * elapsedTime);

                points.Add(new Vector3(x, y, z));
            }

            return points;
        }
    }
}
