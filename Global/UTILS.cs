using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UTILS
{
    public static IEnumerator WaitForFrames(int frameCount)
    {
        while (frameCount > 0)
        {
            frameCount--;
            yield return null;
        }
    }
}
