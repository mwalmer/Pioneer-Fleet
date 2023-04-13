using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchThree.Utilities
{
    public static class Action2D
    {
        public static IEnumerator MoveTo(Transform target, Vector3 to, float duration, bool bSelfRemove = false)
        {
            Vector2 begPosition = target.transform.position;

            float elapsed = 0.0f;
            while (elapsed < duration)
            {
                elapsed += Time.smoothDeltaTime;
                target.transform.position = Vector2.Lerp(begPosition, to, elapsed / duration);

                yield return null;
            }

            target.transform.position = to;

            if (bSelfRemove)
                Object.Destroy(target.gameObject, 0.1f);

            yield break;
        }

        public static IEnumerator Scale(Transform target, float toScale, float speed)
        {
            //decide direction
            bool decideDir = target.localScale.x < toScale;
            float fDirection = decideDir ? 1 : -1;

            float factor;
            while (true)
            {
                factor = Time.deltaTime * speed * fDirection;
                target.localScale = new Vector3(target.localScale.x + factor, target.localScale.y + factor, target.localScale.z);

                if ((!decideDir && target.localScale.x <= toScale) || (decideDir && target.localScale.x >= toScale))
                    break;

                yield return null;
            }

            yield break;
        }

    }
}