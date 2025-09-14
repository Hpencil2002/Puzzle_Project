using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField] string nextScene;
    [SerializeField] Player player;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.SetisClear(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.SetisClear(false);
        }
    }

    public void GoToNextScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
