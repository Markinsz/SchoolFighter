using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        string CenaAtual = SceneManager.GetActiveScene().name;

        //PlayerController playerDeath = GetComponent<PlayerController>();

        ////Se pressionar qualquer tecla
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (CenaAtual == "TelaInicial")
            {
                //Mudar de Cena
                StartCoroutine(CarregarFase("Fase1"));
            }
            else if (CenaAtual == "GameOver")
            {
                StartCoroutine(CarregarFase("TelaInicial"));
            }
            //else if (playerDeath != null)
            //{
            //    if (playerDeath.isDead == true)
            //    {
            //        StartCoroutine(CarregarFase("GameOver"));
            //    }
            //}
            //else
            //{
            //    Debug.LogError("playerDeath não foi encontrado ou atribuído!");
            //}
        }
       
    }

    //Corrotina - Coroutine
    public IEnumerator CarregarFase(string nomeFase)
    {
        transition.SetTrigger("Start");
        
        //Esperar o tempo de animação
        yield return new WaitForSeconds(1f);

        //Carregar a cena
        SceneManager.LoadScene(nomeFase);
    }

}
