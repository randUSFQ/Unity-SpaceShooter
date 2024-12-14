using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Método para cargar la escena del juego
    public void LoadGame()
    {
        SceneManager.LoadScene("Game_Scene");
    }

    // Método para salir del juego
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("El juego ha salido."); // Esto solo se verá en el editor.
    }
}
