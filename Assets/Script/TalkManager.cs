using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> PortraitData;

    public Sprite[] PortraitArr;

    void Start()
    {
        PortraitData = new Dictionary<int, Sprite>();
        talkData = new Dictionary<int, string[]>();
        GenerateTalkDate();
        GeneratePortraitDate();
        

    }
    public string GetTalk(int id, int TalkIndex, int Interact_Num)
    {
        if (!talkData.ContainsKey(id))
            return null;

        else if (TalkIndex == talkData[id].Length)
            return null;

        else if (Interact_Num == 1)
        {
            if (id == 202)
            {
                id = id + 20;
                return talkData[id][TalkIndex];
            }

            else
            {
                return talkData[id][TalkIndex];
            }
        }

        else
            return talkData[id][TalkIndex];
    }

    public Sprite GetPortrait(int portraitIndex)
    {
        return PortraitData[portraitIndex];
    }

    void GeneratePortraitDate()
    {
        PortraitData.Add(0, PortraitArr[0]);
        PortraitData.Add(1, PortraitArr[1]);
    }


    void GenerateTalkDate()
    {
        talkData.Add(0, new string[] { "" });
        talkData.Add(100, new string[] { "이건 돌이다.:0", "개쩌는 돌이다.:0", "아무튼 돌이다.:1", "왜 이것이 그냥 돌이라고 생각하는가.:1" });

        talkData.Add(202, new string[] { "평범한 대걸레다.:0" });
        talkData.Add(222, new string[] { "이걸로 싸울 수 있을지도...:0" });


        talkData.Add(301, new string[] { "이 컴퓨터는 뭘까", "한 번 조사해보자",});
        talkData.Add(311, new string[] { "비밀번호를 입력할 수 있을 것 같다.."});
        talkData.Add(381, new string[] { "비밀번호를 맞췄나보다."});
        talkData.Add(391, new string[] { "이게 아닌가..."});

        talkData.Add(403, new string[] { "잠겨있다." });
        talkData.Add(423, new string[] { "문이 열렸다." });

        talkData.Add(10001, new string[] { "돌멩이를 획득했다." });
    }
}
