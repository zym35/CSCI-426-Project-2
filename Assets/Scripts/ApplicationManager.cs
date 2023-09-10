namespace TorcheyeUtility
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    
    public class ApplicationManager : MonoBehaviour
    {
        [Header("Application Logic")]
        [SerializeField] private KeyCode restartGame = KeyCode.R;
        [SerializeField] private KeyCode exitGame = KeyCode.Escape;
        
        [Header("Scene Control")]
        [SerializeField] private KeyCode nextScene = KeyCode.RightBracket;
        [SerializeField] private KeyCode previousScene = KeyCode.LeftBracket;

        [Header("Audio Volume Control")] 
        [SerializeField] private KeyCode increaseVolume = KeyCode.Equals;
        [SerializeField] private KeyCode decreaseVolume = KeyCode.Minus;
        [SerializeField] private List<AudioSource> audioSources;
        
        private void Update()
        {
            int activeScene = SceneManager.GetActiveScene().buildIndex;
            int totalScene = SceneManager.sceneCountInBuildSettings;

            if (Input.GetKeyDown(restartGame))
                SceneManager.LoadScene(activeScene);
            if (Input.GetKeyDown(exitGame))
                Application.Quit();
            if (Input.GetKeyDown(nextScene))
                SceneManager.LoadScene((activeScene + 1) % totalScene);
            if (Input.GetKeyDown(previousScene))
                SceneManager.LoadScene((activeScene - 1) % totalScene);
            if (Input.GetKeyDown(increaseVolume))
                foreach (AudioSource audioSource in audioSources)
                {
                    audioSource.volume *= 1.5f;
                }
            if (Input.GetKeyDown(decreaseVolume))
                foreach (AudioSource audioSource in audioSources)
                {
                    audioSource.volume *= 0.75f;
                }
        }
    }
}
