using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // M�todo para cargar la escena del juego
    public void LoadGame()
    {
        SceneManager.LoadScene("Game_Scene");
    }

    // M�todo para salir del juego
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("El juego ha salido."); // Esto solo se ver� en el editor.
    }
}
