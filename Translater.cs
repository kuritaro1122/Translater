using System.Collections;
using UnityEngine;

public static class Translater {
    public static IEnumerator TranslatePosAndRot(Transform transform, Vector3 toPos, Quaternion toRot, float duration, bool speedBase = false, bool local = false, AnimationCurve curve = null) {
        Vector3 fromPos = !local ? transform.position : transform.localPosition;
        Quaternion fromRot = !local ? transform.rotation : transform.localRotation;
        float t = 0f;
        float time = speedBase ? Vector3.Distance(fromPos, toPos) / duration : duration;
        AnimationCurve _curve = curve != null ? curve : AnimationCurve.Linear(0f, 0f, 1f, 1f);
        while (t <= time) {
            float T = _curve.Evaluate(t / time);
            Vector3 pos = Vector3.Lerp(fromPos, toPos, T);
            Quaternion rot = Quaternion.Slerp(fromRot, toRot, T);
            if (!local) (transform.position, transform.rotation) = (pos, rot);
            else (transform.localPosition, transform.localRotation) = (pos, rot);
            if (t < time) t = Mathf.Clamp(t + Time.deltaTime, 0f, time);
            else break;
            yield return null;
        }
    }
    public static IEnumerator TranslatePos(Transform transform, Vector3 toPos, float duration, bool speedBase = false, bool local = false, AnimationCurve curve = null) {
        Vector3 fromPos = !local ? transform.position : transform.localPosition;
        float t = 0f;
        float time = speedBase ? Vector3.Distance(fromPos, toPos) / duration : duration;
        if (time <= 0f) {
            Debug.Log("Translater/time is zero.", transform);
            yield break;
        }
        AnimationCurve _curve = curve != null ? curve : AnimationCurve.Linear(0f, 0f, 1f, 1f);
        while (t <= time) {
            float T = _curve.Evaluate(t / time);
            Vector3 pos = Vector3.Lerp(fromPos, toPos, T);
            if (!local) transform.position = pos;
            else transform.localPosition = pos;
            if (t < time) t = Mathf.Clamp(t + Time.deltaTime, 0f, time);
            else break;
            yield return null;
        }
    }
}
