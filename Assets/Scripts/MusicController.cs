using UnityEngine;

public class MusicController : MonoBehaviour
{
    //Classe respons�vel por controlar qualquer tipo de �udiop
    private AudioSource AudioSource;

    // Arquivo de �udio
    public AudioClip levelMusic;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();

        PlayMusic(levelMusic);
    }

    public void PlayMusic(AudioClip music)
    {
        //Define o som que ir� ser reproduzido
        AudioSource.clip = music;

        //Executa o som
        AudioSource.Play();
    }
}
