// Assets/Scripts/BuzzAudioController.cs
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class BuzzAudioController : MonoBehaviour
{
    public AudioSource source;
    public bool autoPlay = false;

    void Reset() { source = GetComponent<AudioSource>(); }
    void Awake()
    {
        if (!source) source = GetComponent<AudioSource>();
        if (source) { source.loop = true; if (autoPlay && !source.isPlaying) source.Play(); }
    }
    public void PlayIfStopped() { if (source && !source.isPlaying) source.Play(); }
    public void FadeTo(float target, float seconds)
    {
        if (!source) return; StopAllCoroutines(); StartCoroutine(FadeRoutine(target, seconds));
    }
    IEnumerator FadeRoutine(float target, float sec)
    {
        float start = source.volume, t = 0f;
        while (t < sec) { t += Time.unscaledDeltaTime; source.volume = Mathf.Lerp(start, target, t / sec); yield return null; }
        source.volume = target;
    }
}

