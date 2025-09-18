using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Menu Manager")]

    [SerializeField] private string NomeDaCena;
    [SerializeField] private GameObject Menu;
    [SerializeField] private GameObject Configuracoes;

    

    public void Jogar()
    {
        SceneManager.LoadScene(NomeDaCena);
    }


    public void AbrirConfiguracoes()
    {
        Menu.SetActive(false);
        Configuracoes.SetActive(true);
    }


    public void FecharConfiguracoes()
    {
        Configuracoes.SetActive(false);
        Menu.SetActive(true);
    }


    public void Sair()
    {
        Application.Quit();
        Debug.Log("Jogo fechado.");
    }

}
