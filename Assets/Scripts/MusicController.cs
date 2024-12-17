using UnityEngine;

public class MusicController : MonoBehaviour
{
    //Classe responsável por controlar qualquer tipo de áudiop
    private AudioSource AudioSource;

    // Arquivo de áudio
    public AudioClip levelMusic;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();

        PlayMusic(levelMusic);
    }

    public void PlayMusic(AudioClip music)
    {
        //Define o som que irá ser reproduzido
        AudioSource.clip = music;

        //Executa o som
        AudioSource.Play();
    }
}
