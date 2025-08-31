using UnityEngine;
using MidiaAdapt.Core;

public class MenuManager : MonoBehaviour
{
    [SerializeField] string nextSceneName;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        SceneLoader.Instance.LoadScene(nextSceneName);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
