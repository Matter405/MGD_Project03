using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private MusicPlayer _musicPlayer1;
    private MusicPlayer _musicPlayer2;

    private MusicPlayer _activeMusicPlayer = null;

    private float _volume = 0.3f;
    public float Volume
    {
        get => _volume;
        private set
        {
            value = Mathf.Clamp(value, 0, 0.3f);
            _volume = value;
        }
    }

    private AudioClip _activeMusicTrack = null;

    // singleton pattern
    private static MusicManager _instance;
    public static MusicManager Instance
    {
        get
        {
            //Lazy instantiation
            if(_instance == null)
            {
                _instance = FindAnyObjectByType<MusicManager>();
                if(_instance == null)
                {
                    GameObject singletonGO = new GameObject("MusicManager_Singleton");
                    _instance = singletonGO.AddComponent<MusicManager>();
                    DontDestroyOnLoad(singletonGO);
                }

            }
            return _instance;
        }
    }

    private void Awake()
    {
        //enforce singleton
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        //this is singleton
        SetupMusicPlayers();
    }

    public void Play(AudioClip musicTrack, float fadeTime)
    {
        //guard clauses
        if (musicTrack == null) return;
        if (musicTrack == _activeMusicTrack) return;

        //stop the current player
        if(_activeMusicTrack != null)
        {
            _activeMusicPlayer.Stop(fadeTime);
        }

        //switch to new player while previous fades
        SwitchActiveMusicPlayer();
        _activeMusicTrack = musicTrack;

        //play the new song (with fade up)
        _activeMusicPlayer.Play(musicTrack, fadeTime);
    }

    public void Stop(float fadeTime)
    {
        //don't stop if we don't have a track yet
        if (_activeMusicTrack == null) return;
        // clear out active track and stop
        _activeMusicTrack = null;
        _activeMusicPlayer.Stop(fadeTime);
    }

    private void SetupMusicPlayers()
    {
        _musicPlayer1 = gameObject.AddComponent<MusicPlayer>();
        _musicPlayer2 = gameObject.AddComponent<MusicPlayer>();

        //choose a starting 'active' music player
        _activeMusicPlayer = _musicPlayer1;
    }

    private void SwitchActiveMusicPlayer()
    {
        if (_activeMusicPlayer == _musicPlayer1)
            _activeMusicPlayer = _musicPlayer2;

        else if (_activeMusicPlayer == _musicPlayer2)
            _activeMusicPlayer = _musicPlayer1;
    }
}
