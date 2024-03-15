using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Game.GameLogic;

class AudioHandler : IDisposable
{
    static AudioHandler instance;
    public AudioHandler()
    {
        instance = this;
        InitAudioDevice();
    }
    public static AudioHandler GetAudioHandler()
    {
        return instance;
    }
    Sound?[] soundBuffer = new Sound?[10];
    int c = 0;
    public void PlaySoundM(string sound)
    {
        if (!loadedSounds.ContainsKey(sound))
            LoadSoundM(sound);
        if (soundBuffer[c] != null)
            UnloadSoundAlias((Sound)soundBuffer[c]);
        soundBuffer[c] = LoadSoundAlias(loadedSounds[sound]);
        PlaySound((Sound)soundBuffer[c]);
    }

    Dictionary<string, Sound> loadedSounds = new Dictionary<string, Sound>();
    public void LoadSoundM(string sound)
    {
        loadedSounds.Add(sound, LoadSound($"assets/Audio/{sound}.ogg"));
    }

    public void Dispose()
    {
        foreach (Sound? s in soundBuffer)
            if (s != null)
                UnloadSoundAlias((Sound)s);
        foreach (var ds in loadedSounds)
            UnloadSound(ds.Value);

        CloseAudioDevice();
    }
}
