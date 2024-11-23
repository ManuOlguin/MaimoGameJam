using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }
    // Carga una escena por nombre
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Carga una escena por índice
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // Cargar escena de manera asíncrona (ideal para tiempos de carga o transiciones)
    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(LoadSceneAsyncCoroutine(sceneName));
    }

    private IEnumerator LoadSceneAsyncCoroutine(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            // Puedes mostrar un progreso de carga aquí, por ejemplo:
            Debug.Log($"Progreso de carga: {asyncLoad.progress * 100}%");
            yield return null;
        }
    }

    // Reinicia la escena actual
    public void ReloadCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Obtiene el índice de la escena actual
        int nextSceneIndex = currentSceneIndex + 1; // Suma 1 para obtener el índice de la siguiente escena

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) // Verifica que la siguiente escena existe
        {
            SceneManager.LoadScene(nextSceneIndex); // Carga la siguiente escena
        }
        else
        {
            Debug.LogWarning("No hay más escenas para cargar.");
        }
    }

    // Salir del juego
    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}
