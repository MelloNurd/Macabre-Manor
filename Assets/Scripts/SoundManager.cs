using System;
using System.Collections;
using UnityEngine;

[System.Serializable]
public class Sound {
    [HideInInspector]
    public AudioSource source;

    public AudioClip clip;

    public new string name;

    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(0.1f, 3f)]
    public float pitch = 1f;
    [Range(0f, 15f)]
    public float delay = 0f;

    public bool loop;
    public bool playOnAwake;
}

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;

    public string clipPlayOnStartForTesting;

    public bool isWaitingToPlay;
    private Sound clipWaitingOn;
    private Sound clipToPlay;

    private void Awake() {
        foreach (Sound sound in sounds) {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.playOnAwake = sound.playOnAwake;
            if (sound.source.playOnAwake) sound.source.Play();
        }
    }

    public void TransitionPlay(string transitionSongPlusSongToPlay) {
        string transitionFrom = transitionSongPlusSongToPlay.Split('|')[0];
        string transitionTo = transitionSongPlusSongToPlay.Split('|')[1];

        clipWaitingOn = Array.Find(sounds, sound => sound.name == transitionFrom);
        clipToPlay = Array.Find(sounds, sound => sound.name == transitionTo);

        if (!clipWaitingOn.source.isPlaying) return;

        clipWaitingOn.source.loop = false;

        StartCoroutine(PlayAfterCurrent(clipWaitingOn.source.clip.length - clipWaitingOn.source.time - 0.01f));
    }

    public void Play(string name) {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        //if(sound.delay == 0) sound.source?.PlayDelayed(sound.delay);
        /*else */sound.source?.Play();
    }

    public void Stop(string name) {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        sound.source?.Stop();
    }

    private void Start() {
        if(!string.IsNullOrEmpty(clipPlayOnStartForTesting)) { Play(clipPlayOnStartForTesting); }
    }

    IEnumerator PlayAfterCurrent(float seconds) {
        yield return new WaitForSeconds(seconds);
        clipToPlay.source.Play();
    }
}
