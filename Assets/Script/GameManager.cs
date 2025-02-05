using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public PWKeyManager keyManager;
    public ItemManager itemManager;
    public Inventory Inv;

    public GameObject talkPanel;
    public GameObject PcPanel;

    public Text TalkText;

    private GameObject scanObject;

    public Image PortraitImg;

    public static bool isAction;
    public static bool stopKeyInput;

    private bool isTalk;
    private bool isTyping;
    private bool isPcOn;

    public int TalkIndex;

    
    public void Action(GameObject scanObject)
    {
        ObjData objData = scanObject.GetComponent<ObjData>();

        if (objData.Dialog_id != 0 && objData.canTalk)
            TalkAction(scanObject);

        if (objData.Pc_id != 0)
            PcAction(scanObject);

        if (objData.isItem == true && objData.Item_id != 0 && !isTalk)
            ItemAction(scanObject);

    }
    
    void ItemAction(GameObject scanObject)
    {
        ObjData objData = scanObject.GetComponent<ObjData>();

        int Item_Id = objData.Item_id;

        ItemManager item = ItemManager.GetItem(Item_Id);

        if (item != null)
            if (Inventory.inventoryItemList.Contains(item))
            {
                item.item_Count++;
            }
            else
                Inventory.inventoryItemList.Add(item);

        scanObject.SetActive(false);
    }

    void PcAction(GameObject scanObject)
    {
        ObjData objData = scanObject.GetComponent<ObjData>();

        if (isPcOn)
        {
            PcNumber(scanObject);
        }
        else if (!isTalk)
        {
            keyManager.PW.text = "";
            isAction = true;
            isPcOn = true;
        }

        PcPanel.SetActive(isPcOn);
    }

    void PcNumber(GameObject scanObject)
    {
        isPcOn = false;

        ObjData objData = scanObject.GetComponent<ObjData>();

        int Dialog_id = objData.Dialog_id;
        int Pc_id = objData.Pc_id;

        if (keyManager.GetPc(Pc_id))
            objData.Dialog_id = Pc_id + 80;

        else
            objData.Dialog_id = Pc_id + 90;

        TalkAction(scanObject);

        
    }

    void TalkAction(GameObject scanObject)
    {
        if (isTalk && isTyping)
            isTyping = false;
        else
        {
            isAction = true;
            
            Talk(scanObject);
        }

        talkPanel.SetActive(isTalk);
    }

    void Talk(GameObject scanObject)
    {
        ObjData objData = scanObject.GetComponent<ObjData>();

        int id = objData.Dialog_id;
        int Interact_Num = objData.Interact_Number;

        bool isNPC = objData.isNPC;

        string talkData = talkManager.GetTalk(id, TalkIndex, Interact_Num);

        if (talkData == null)
        {
            isAction = false;
            TalkIndex = 0;
            isTalk = false;

            TalkType(scanObject);

            return;
        }
        if (isNPC)
        {
            StartCoroutine(TextOutput(talkData.Split(":")[0]));

            PortraitImg.sprite = talkManager.GetPortrait(int.Parse(talkData.Split(":")[1]));
            PortraitImg.color = new Color(1, 1, 1, 1);
        }
        else
        {
            StartCoroutine(TextOutput(talkData));

            PortraitImg.color = new Color(1, 1, 1, 0);

        }
        isTalk = true;
        TalkIndex++;
    }
    void TalkType(GameObject scanObject)
    {
        ObjData objData = scanObject.GetComponent<ObjData>();

        int Dialog_id = objData.Dialog_id;
        int Pc_id = objData.Pc_id;
        int Item_id = objData.Item_id;
        int Interact_Num = objData.Interact_Number;

        string talkData = "";

        if (Dialog_id % 10 == 1 || Pc_id % 10 == 1)
        {
            if (Dialog_id == 311)
            {
                objData.Pc_id = 301;
                objData.Dialog_id = 0;
                return;
            }
            else if (Pc_id % 10 == 1)
            {
                objData.Dialog_id = Pc_id;
                objData.Pc_id = 0;
                return;
            }
        }

        else if (Dialog_id % 10 == 3)
        {
            if (Dialog_id == 403)
            {
                ItemManager item = ItemManager.GetItem(10001);
                if (item != null)
                {
                    if (Inventory.inventoryItemList.Contains(item))
                    {
                        Debug.Log(item);
                        item.item_Count--;
                        if (item.item_Count <= 0)
                            Inventory.inventoryItemList.Remove(item);

                        objData.Dialog_id = objData.Dialog_id + 10;
                    }
                }
            }
        }

        talkData = talkManager.GetTalk(Dialog_id + 10, 0, objData.Interact_Number);
        if (talkData != null)
            objData.Dialog_id = objData.Dialog_id + 10;
    }

    void CanTalk(GameObject scanObject)
    {
        ObjData ObjData = scanObject.GetComponent<ObjData>();
        ObjData.canTalk = !ObjData.canTalk;
    }

    IEnumerator TextOutput(string TalkInput)
    {
        TalkText.text = string.Empty;

        StringBuilder sb = new StringBuilder();

        isTyping = true;

        for (int i = 0; i < TalkInput.Length; i++)
        {
            if (!isTyping)
            {
                TalkText.text = TalkInput;
                break;
            }

            sb.Append(TalkInput[i]);
            TalkText.text = sb.ToString();
            yield return new WaitForSeconds(0.03f);

        }
        isTyping = false;
    }

}