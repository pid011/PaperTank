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

            var elapsedTime = 0f;

            var gravity = Mathf.Abs(Physics.gravity.y);
            var dh = end.y - start.y;
            var mh = height - start.y;
            var ty = Mathf.Sqrt(2 * gravity * mh);

            var a = -2 * ty;
            var b = 2 * dh;
            var duration = (-a + Mathf.Sqrt((a * a) - (4 * gravity * b))) / (2 * gravity);

            var tx = -(start.x - end.x) / duration;
            var tz = -(start.z - end.z) / duration;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                var x = start.x + (tx * elapsedTime);
                var y = start.y + (ty * elapsedTime) - (0.5f * gravity * elapsedTime * elapsedTime);
                var z = start.z + (tz * elapsedTime);

                transform.position = new Vector3(x, y, z);

                yield return waitForFixedUpdate;
            }
        }

        public static IReadOnlyList<Vector3> SimulateParabolaMove(Vector3 start, Vector3 end, float height)
        {
            var points = new List<Vector3>();

            height += start.y < end.y ? end.y : start.y;

            var elapsedTime = 0f;

            var gravity = Mathf.Abs(Physics.gravity.y);
            var dh = end.y - start.y;
            var mh = height - start.y;
            var ty = Mathf.Sqrt(2 * gravity * mh);

            var a = -2 * ty;
            var b = 2 * dh;
            var duration = (-a + Mathf.Sqrt((a * a) - (4 * gravity * b))) / (2 * gravity);

            var tx = -(start.x - end.x) / duration;
            var tz = -(start.z - end.z) / duration;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                var x = start.x + (tx * elapsedTime);
                var y = start.y + (ty * elapsedTime) - (0.5f * gravity * elapsedTime * elapsedTime);
                var z = start.z + (tz * elapsedTime);

                points.Add(new Vector3(x, y, z));
            }

            return points;
        }
    }
}
