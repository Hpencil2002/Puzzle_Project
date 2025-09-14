using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(100, new string[] { "힘의 미궁", "장애물을 밀어 미궁을 탈출하라", "이동하시겠습니까?" });
        talkData.Add(200, new string[] { "공간의 미궁", "순간이동하는 발판을 이용해 미궁을 탈출하라", "이동하시겠습니까?" });
        talkData.Add(300, new string[] { "지혜의 미궁", "게이트에 맞는 열쇠를 찾아 미궁을 탈출하라", "이동하시겠습니까?" });
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
        {
            return null;
        }
        else if (talkIndex == talkData[id].Length - 1)
        {
            GameManager.instance.SetTalkButton(true);
            return talkData[id][talkIndex];
        }
        else
        {
            GameManager.instance.SetTalkButton(false);
            return talkData[id][talkIndex];
        }
    }
}
