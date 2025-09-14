using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isAction;
    public int talkIndex;
    public TalkManager talkManager;
    public GameObject talkPanel;
    public Text talkText;
    public GameObject talkButton;
    public GameObject scanObject;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        talkButton.SetActive(false);
        talkPanel.SetActive(false);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (talkButton != null)
        {
            talkButton.SetActive(false);
        }
        if (talkPanel != null)
        {
            talkPanel.SetActive(false);
        }
    }

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;

        ObjData objData = scanObj.GetComponent<ObjData>();
        Talk(objData.id);

        talkPanel.SetActive(isAction);
    }

    public void Talk(int id)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);
        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            return;
        }

        talkText.text = talkData;

        isAction = true;
        talkIndex += 1;
    }

    public void SetTalkButton(bool flag)
    {
        talkButton.SetActive(flag);
    }

    public void ClosePanel()
    {
        isAction = false;
        talkIndex = 0;
        talkButton.SetActive(false);
        talkPanel.SetActive(false);
    }
}
