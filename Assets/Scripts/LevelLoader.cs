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
        }
       
    }

    //Corrotina - Coroutine
    IEnumerator CarregarFase(string nomeFase)
    {
        transition.SetTrigger("Start");
        
        //Esperar o tempo de animação
        yield return new WaitForSeconds(1f);

        //Carregar a cena
        SceneManager.LoadScene(nomeFase);
    }

}
