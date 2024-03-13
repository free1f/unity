using System;
using UnityEngine;

namespace Freelf.Audio
{
  public class AudioHandler: MonoBehaviour
  {
    public static AudioHandler instance { get; private set; }
    public Sound[] sounds;
    public AudioChannel[] channels;

    private void Awake()
    {
      if (instance == null) instance = this;
      else {
        Destroy(gameObject);
        return;
      }
      DontDestroyOnLoad(gameObject);
    }

    public void Play(string soundName, bool once)
    {
      var foundSound = Array.Find(sounds, x => x.name == soundName);
      if (foundSound == null) {
        Debug.LogError($"Sound {soundName} not found");
        return;
      }
      GetChannel(foundSound.category, foundChannel => {
        if (once) foundChannel.PlayOnce(foundSound.clip, foundSound.volume);
        else foundChannel.Play(foundSound);
      });
    }

    public void Play(AudioClip clip, SoundCategory category, float volume = 1f)
    {
      GetChannel(category, foundChannel => foundChannel.PlayOnce(clip, volume));
    }

    public void Play(AudioClip clip, Vector3 position, float volume = 1f)
    {
      AudioSource.PlayClipAtPoint(clip, position, volume);
    }

    public void Stop(SoundCategory category)
    {
      GetChannel(category, foundChannel => foundChannel.Stop());
    }

    private void GetChannel(SoundCategory category, Action<AudioChannel> onFoundChannel)
    {
      var foundChannel = Array.Find(channels, x => x.category == category);
      if (foundChannel == null) {
        Debug.LogError($"Channel for {category} not found");
        return;
      }
      onFoundChannel?.Invoke(foundChannel);
    }
  }
}