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
    
    /// <summary>
    /// Play a sound once or not by its name
    /// </summary>
    /// <param name="soundName">Exact name of the database</param>
    /// <param name="once">To play once or in loop</param>
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
    
    /// <summary>
    /// Play a sound according to the clip and category
    /// </summary>
    /// <param name="clip">Audio to be played</param>
    /// <param name="category">Tag for the type of the sound</param>
    /// <param name="volume">Optional volume of the clip</param>
    public void Play(AudioClip clip, SoundCategory category, float volume = 1f)
    {
      GetChannel(category, foundChannel => foundChannel.PlayOnce(clip, volume));
    }

    /// <summary>
    /// Play a sound according to the clip in a specific position
    /// </summary>
    /// <param name="clip">Audio to be played</param>
    /// <param name="position">Location of the sound in the scene</param>
    /// <param name="volume">Optional volume of the clip</param>
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