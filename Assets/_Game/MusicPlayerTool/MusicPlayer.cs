using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour
{
    private AudioSource _audioSource;
    private Coroutine _lerpVolumeRoutine = null;
    private Coroutine _stopRoutine = null;

    private void Awake()
    {
        //setup
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.loop = true;
        _audioSource.playOnAwake = false;
    }

    public void Play(AudioClip musicTrack, float fadeTime)
    {
        //start at 0 volume and fade up
        _audioSource.volume = 0;
        _audioSource.clip = musicTrack;
        _audioSource.Play();

        FadeVolume(MusicManager.Instance.Volume, fadeTime);
    }

    public void Stop(float fadeTime)
    {
        //reset it if it's already going
        if (_stopRoutine != null)
            StopCoroutine(_stopRoutine);
        _stopRoutine = StartCoroutine(StopRoutine(fadeTime));
    }

    public void FadeVolume(float targetVolume, float fadeTime)
    {
        targetVolume = Mathf.Clamp(targetVolume, 0, 0.3f);
        if (fadeTime < 0) fadeTime = 0;

        if (_lerpVolumeRoutine != null)
            StopCoroutine(_lerpVolumeRoutine);
        _lerpVolumeRoutine = StartCoroutine(LerpVolumeRoutine(targetVolume, fadeTime));
    }

    private IEnumerator LerpVolumeRoutine(float targetVolume, float fadeTime)
    {
        float newVolume;
        float startVolume = _audioSource.volume;

        for(float elapsedTime = 0; elapsedTime <= fadeTime; elapsedTime += Time.deltaTime)
        {
            newVolume = Mathf.Lerp(startVolume, targetVolume, elapsedTime / fadeTime);
            _audioSource.volume = newVolume;
            yield return null;
        }

        //if we've made it this far, set it to the target volume
        _audioSource.volume = targetVolume;
    }

    private IEnumerator StopRoutine(float fadeTime)
    {
        // cancel current running volume fade, stop prematurely
        if (_lerpVolumeRoutine != null)
            StopCoroutine(_lerpVolumeRoutine);
        _lerpVolumeRoutine = StartCoroutine(LerpVolumeRoutine(0, fadeTime));

        //wait for blend to finish
        yield return _lerpVolumeRoutine;
        _audioSource.Stop();
    }    
}
