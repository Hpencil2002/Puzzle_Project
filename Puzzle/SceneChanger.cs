using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene()
    {
        ObjData obj = GameManager.instance.scanObject.GetComponent<ObjData>();
        int id = obj.id; //rock, warp, key

        switch (id)
        {
            case 100:
                GameManager.instance.isAction = false;
                SceneManager.LoadScene("Rock1");
                break;
            case 200:
                GameManager.instance.isAction = false;
                SceneManager.LoadScene("Warp1");
                break;
            case 300:
                GameManager.instance.isAction = false;
                SceneManager.LoadScene("Key1");
                break;
        }
    }
}
