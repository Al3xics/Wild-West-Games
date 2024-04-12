using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject games;

    // L'objet en haut de la pile est l'objet actuel sur lequel on se situe
    private Stack<GameObject> objectStack = new();

    void Start()
    {
        objectStack.Push(menu);
    }

    public void PlayGameButton()
    {
        GameManager.Instance.LoadNextMiniGame();
        //SceneManager.LoadScene("......");
    }

    public void SettingsButton()
    {
        objectStack.Push(settings);
        menu.SetActive(false);
        settings.SetActive(true);
    }

    public void GamesButton()
    {
        objectStack.Push(games);
        menu.SetActive(false);
        games.SetActive(true);
    }

    public void MusicButton()
    {

    }

    public void SoundEffectsButton()
    {

    }

    public void LanguagesButton()
    {

    }

    public void ProgressionButton()
    {

    }

    public void AboutUsButton()
    {

    }

    public void BackButton()
    {
        if (objectStack.Count > 0)
        {
            objectStack.Pop().SetActive(false);
            objectStack.Peek().SetActive(true);
        }
        else
        {
            Debug.LogWarning("Il n'y a pas d'objet précédent à activer.");
        }
    }
}
