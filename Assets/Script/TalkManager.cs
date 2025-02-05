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
        talkData.Add(100, new string[] { "�̰� ���̴�.:0", "��¼�� ���̴�.:0", "�ƹ�ư ���̴�.:1", "�� �̰��� �׳� ���̶�� �����ϴ°�.:1" });

        talkData.Add(202, new string[] { "����� ��ɷ���.:0" });
        talkData.Add(222, new string[] { "�̰ɷ� �ο� �� ��������...:0" });


        talkData.Add(301, new string[] { "�� ��ǻ�ʹ� ����", "�� �� �����غ���",});
        talkData.Add(311, new string[] { "��й�ȣ�� �Է��� �� ���� �� ����.."});
        talkData.Add(381, new string[] { "��й�ȣ�� ���質����."});
        talkData.Add(391, new string[] { "�̰� �ƴѰ�..."});

        talkData.Add(403, new string[] { "����ִ�." });
        talkData.Add(423, new string[] { "���� ���ȴ�." });

        talkData.Add(10001, new string[] { "�����̸� ȹ���ߴ�." });
    }
}
