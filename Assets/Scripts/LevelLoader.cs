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
        ////Se pressionar qualquer tecla
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //Mudar de Cena
            StartCoroutine(CarregarFase("Fase1"));
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
