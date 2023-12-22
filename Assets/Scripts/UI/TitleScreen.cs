using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] CanvasGroup _canvas;
    [SerializeField] CanvasGroup _bgCanvas;

    [SerializeField] TutorialHandler _tutorialHandler;
    [SerializeField] AudioSource _audioSource;

    public void NewGame()
    {
        SaveHandler.DeleteSaveData();
        _canvas.alpha = 0;
        _canvas.interactable = false;
        _canvas.blocksRaycasts = false;
        _audioSource.Play();
        _bgCanvas.DOFade(0, 1.9f).onComplete += OnFinish;
    }
    private void OnFinish()
    {
        _tutorialHandler.StartGame();

    }
    public void Continue()
    {
        string path = Application.persistentDataPath + "/save.boogers";
        if (File.Exists(path))
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            NewGame();
        }
    }
}
