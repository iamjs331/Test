using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static ItemManager;

public class Inventory : MonoBehaviour
{
    private InventorySlot[] slots;

    public static List<ItemManager> inventoryItemList = new List<ItemManager>();
    private List<ItemManager> inventoryTabList;

    public Text Description_Text;
    public string[] tabDescription;

    public Transform tf; //slot�� �θ�ü.

    public GameObject InventoryPanel;
    public GameObject[] selectedTabImages;

    private int selectedItem;
    private int selectedTab;

    private bool isInventoryOn;
    private bool isTabActivated;
    private bool isItemActivated;
    private bool preventExecute; //�ߺ����� ����

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    void Awake()
    {
        inventoryTabList = new List<ItemManager>();
        slots = tf.GetComponentsInChildren<InventorySlot>();
    }

    public void RemoveSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveItem();
            slots[i].gameObject.SetActive(false);
        }
    } //�κ��丮 ���� �ʱ�ȭ

    public void ShowTab()
    {
        RemoveSlot();
        SelectedTab();
        
    } //�� Ȱ��ȭ

    public void SelectedTab()
    {
        StopAllCoroutines();

        Color color = selectedTabImages[selectedTab].GetComponent<Image>().color;
        color.a = 0f;

        for (int i = 0; i < selectedTabImages.Length; i++)
            selectedTabImages[i].GetComponent<Image>().color = color;

        Description_Text.text = tabDescription[selectedTab];

        StartCoroutine(SelectedTabEffectCoroutine());
    } //���õ� ���� ������ ��� �� �÷��� ����

    IEnumerator SelectedTabEffectCoroutine()
    {
        while (isTabActivated)
        {
            Color color = selectedTabImages[0].GetComponent<Image>().color;
            while (color.a < 0.5f)
            {
                color.a = color.a + 0.03f;
                selectedTabImages[selectedTab].GetComponent<Image>().color = color;
                yield return waitTime;
            }
            while (color.a > 0f)
            {
                color.a = color.a - 0.03f;
                selectedTabImages[selectedTab].GetComponent<Image>().color = color;
                yield return waitTime;
            }

            yield return new WaitForSeconds(0.3f);
        }
    } //���õ� �� ��¦�̰�

    public void ShowItem()
    {
        inventoryTabList.Clear();
        RemoveSlot();
        selectedItem = 0;

        switch (selectedTab)
        {
            case 0:
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (ItemManager.ItemType.Item == inventoryItemList[i].item_Type)
                        inventoryTabList.Add(inventoryItemList[i]);
                }
                break;

            case 1:
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (ItemManager.ItemType.Key_Item == inventoryItemList[i].item_Type)
                        inventoryTabList.Add(inventoryItemList[i]);
                }
                break;

            case 2:
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (ItemManager.ItemType.Post_it == inventoryItemList[i].item_Type)
                        inventoryTabList.Add(inventoryItemList[i]);
                }
                break;
        }

        for (int i = 0; i < inventoryTabList.Count; i++)
        {
            slots[i].gameObject.SetActive(true);
            slots[i].Additem(inventoryTabList[i]);
        }

        SelectedItem();
    } // ������ Ȱ��ȭ ( ���õ� �ǿ� �ش�Ǵ� ������ ��� )

    public void SelectedItem()
    {
        StopAllCoroutines();

        if (inventoryTabList.Count > 0)
        {
            Color color = slots[0].selected_Item.GetComponent<Image>().color;
            color.a = 0f;

            for (int i = 0; i < inventoryTabList.Count; i++)
                slots[i].selected_Item.GetComponent<Image>().color = color;

            Description_Text.text = inventoryTabList[selectedItem].item_Description;

            StartCoroutine(SelectedItemEffectCoroutine());
        }
        else
            Description_Text.text = "";
    } //���õ� �������� ������ ��� �� �÷��� ����

    IEnumerator SelectedItemEffectCoroutine()
    {
        while (isItemActivated)
        {
            Color color = slots[0].GetComponent<Image>().color;
            while (color.a < 0.5f)
            {
                color.a = color.a + 0.03f;
                slots[selectedItem].selected_Item.GetComponent<Image>().color = color;
                yield return waitTime;
            }
            while (color.a > 0f)
            {
                color.a = color.a - 0.03f;
                slots[selectedItem].selected_Item.GetComponent<Image>().color = color;
                yield return waitTime;
            }

            yield return new WaitForSeconds(0.3f);
        }
    } //���õ� ������ ��¦�̰�

    public void InventoryOnOff()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {

            isInventoryOn = !isInventoryOn;

            if (isInventoryOn)
            {
                GameManager.isAction = true;
                InventoryPanel.SetActive(true);
                selectedTab = 0;
                isTabActivated = true;
                isItemActivated = false;

                ShowTab();
            }
            else
            {
                StopAllCoroutines();

                GameManager.isAction = false;
                GameManager.stopKeyInput = false;

                InventoryPanel.SetActive(false);
                isTabActivated = false;
                isItemActivated = false;
            }
        }

        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isInventoryOn)
            {
                StopAllCoroutines();

                isInventoryOn = false;

                GameManager.isAction = false;
                GameManager.stopKeyInput = false;
                InventoryPanel.SetActive(false);
                isTabActivated = false;
                isItemActivated = false;
            }
        }
    } //�κ��丮 ���� �ݱ�

    public void InventoryAction()
    {
        if (isInventoryOn)
        {
            if (isTabActivated)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (selectedTab < selectedTabImages.Length - 1)
                        selectedTab++;
                    else
                        selectedTab = 0;

                    SelectedTab();
                }

                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if (selectedTab > 0)
                        selectedTab--;
                    else
                        selectedTab = selectedTabImages.Length - 1;

                    SelectedTab();
                }

                else if (Input.GetKeyDown(KeyCode.Z))
                {
                    Color color = selectedTabImages[selectedTab].GetComponent<Image>().color;
                    color.a = 0.25f;
                    selectedTabImages[selectedTab].GetComponent<Image>().color = color;

                    isItemActivated = true;
                    isTabActivated = false;
                    preventExecute = true;

                    ShowItem();
                }
            } // �� Ȱ��ȭ�� Ű�Է� ó��.

            else if (isItemActivated)
            {
                
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if (selectedItem < inventoryTabList.Count - 2)
                        selectedItem = selectedItem + 2;
                    else
                        selectedItem = selectedItem % 2;

                    SelectedItem();
                }

                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (selectedItem > 1)
                        selectedItem = selectedItem - 2;
                    else if (inventoryTabList.Count != 0)
                        if (inventoryTabList.Count % 2 == 0)
                            selectedItem = selectedItem + inventoryTabList.Count - 2;
                        else
                            selectedItem = inventoryTabList.Count - ( 1 + selectedItem );

                    else
                        selectedItem = 0;

                    SelectedItem();
                }

                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (selectedItem < inventoryTabList.Count - 1)
                        selectedItem++;
                    else
                        selectedItem = 0;

                    SelectedItem();
                }

                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if (selectedItem > 0)
                        selectedItem--;
                    else if (inventoryTabList.Count != 0)
                        selectedItem = inventoryTabList.Count - 1;
                    else
                        selectedItem = 0;

                    SelectedItem();
                }

                else if (Input.GetKeyDown(KeyCode.Z) && !preventExecute)
                {
                    if (selectedTab == 0) //��Ÿ ������
                    {
                        GameManager.stopKeyInput = true;
                        isTabActivated = false;
                    }

                    else if (selectedTab == 1) //�ٽ� ������
                    {
                        GameManager.stopKeyInput = true;
                        isTabActivated = false;
                    }

                    else if (selectedTab == 2) //����Ʈ��
                    {
                        GameManager.stopKeyInput = true;
                        isTabActivated = false;
                    }
                }

                else if (Input.GetKeyDown(KeyCode.X))
                {
                    StopAllCoroutines();

                    isItemActivated = false;
                    isTabActivated = true;

                    ShowTab();
                }

            } //������ Ȱ��ȭ�� Ű�Է� ó��.

            if (Input.GetKeyUp(KeyCode.Z))
            {
                preventExecute = false;
            } //�ߺ� ���� ����.
        }
    } //�κ��丮 ����
}


