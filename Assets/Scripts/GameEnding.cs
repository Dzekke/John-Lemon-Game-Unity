using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    private bool isPlayerAtExit,isPlayerCaught;
    public GameObject player;
    public CanvasGroup exitBackgroudImageCanvasGroup, caughtBackgroudImageCanvasGroup;
    //public CanvasGroup caughtBackgroudImageCanvasGroup;
    private float timer;
    public AudioSource exitAudio,caughtAudio;
    private bool hasAudioPlayed;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            isPlayerAtExit = true;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (isPlayerAtExit)
        {
            EndLevel(exitBackgroudImageCanvasGroup,false, exitAudio );
        }
        else if (isPlayerCaught)
        {
            EndLevel(caughtBackgroudImageCanvasGroup,true, caughtAudio);
        }
    }

    /// <summary>
    /// Muestra la imagen de fin de partida
    /// </summary>
    /// <param name="imageCanvasGroup">Imagen de fin d partida correspondiente</param>
    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        if (!hasAudioPlayed)
        {
            audioSource.Play();
            hasAudioPlayed = true;
        }

        timer += Time.deltaTime;
        imageCanvasGroup.alpha = Mathf.Clamp(timer / fadeDuration, 0, 1);

        if (timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                Application.Quit();

            }
        }
        
    }

    public void CatchPlayer()
    {
        isPlayerCaught=true;
    }

}
