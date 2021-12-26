using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftPanel : MonoBehaviour
{
    private List<ItemInventory> items;
    private List<CraftItem> craftItems;
    public List<ItemInventory> imagesShow;

    public Inventory inv;
    public DataBaseCraft dataBaseCraft;
    public DataBase data;
    private int countCraftItem;

    public GameObject backGround;
    public GameObject cellArea;
    public GameObject showNeedItemsArea;
    public GameObject buttonCreateItemArea;
    public GameObject gameObjShow;
    public GameObject gameImgShow;
    public GameObject buttonCreateItem;
    private GameObject buttonCreate;
    public GameObject cellsContainer;
    public GameObject scrollBar;

    public EventSystem es;

    void Start()
    {
        imagesShow = new List<ItemInventory>();
        items = new List<ItemInventory>();
        craftItems = new List<CraftItem>();
        countCraftItem = dataBaseCraft.craftItems.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            backGround.SetActive(!backGround.activeSelf);
        }
    }
    
    public void UpdateCountItems()
    {
        if (items.Count != 0) items.Clear();
        items = new List<ItemInventory>();
        int maxCount = inv.maxCount;
        for (int i = 1; i < maxCount; i++)
        {
            items.Add(new ItemInventory { id = i, count = 0 });
        }
        for (int i = 0; i < items.Count; i++)
        {
            items[inv.items[i].id].count += inv.items[i].count;
        }
    }

    public void UpdateCraftPanel()
    {
        foreach(var craftItem in craftItems)
        {
            Destroy(craftItem.itemGameObj);
        }
        craftItems.Clear();
        for(int i = 0; i < countCraftItem; i++)
        {
            if (IsCanBeCrafted(dataBaseCraft.craftItems[i]))
            {
                craftItems.Add(dataBaseCraft.craftItems[i]);
                craftItems[craftItems.Count - 1].itemGameObj = CreateCraftItemButton(craftItems[craftItems.Count - 1].idCell, data.items[craftItems[craftItems.Count - 1].id].img);
            }
        }
        scrollBar.GetComponent<Scrollbar>().value = 1;
        cellsContainer.GetComponent<RectTransform>().offsetMin = new Vector2(cellsContainer.GetComponent<RectTransform>().offsetMin.x, -60 * (int)(craftItems.Count / 4));
    }

    private bool IsCanBeCrafted(CraftItem craftItem)
    {
        bool canCraftItem = true;
        int countNeedItems = craftItem.needItems.Count;
        for(int i = 0; i < countNeedItems; i++)
        {
            if (craftItem.needItems[i].count > items[craftItem.needItems[i].id].count) canCraftItem = false;
        }
        return canCraftItem;
    }

    private GameObject CreateCraftItemButton(int id, Sprite sprite)
    {
        GameObject newItem = Instantiate(gameObjShow, cellArea.transform) as GameObject;
        newItem.gameObject.transform.Find("CraftItem").GetComponent<Image>().sprite = sprite;
        newItem.gameObject.transform.Find("CraftItem").name = id.ToString();

        ItemInventory ii = new ItemInventory();
        ii.itemGameObj = newItem;

        RectTransform rt = newItem.GetComponent<RectTransform>();
        rt.localPosition = new Vector3(0, 0, 0);
        rt.localScale = new Vector3(1, 1, 1);
        newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

        Button tempButton = newItem.GetComponentInChildren<Button>();

        tempButton.onClick.AddListener(delegate { ChooseItem(); });

        return newItem;
    }

    private void CleanNeedItemsArea()
    {
        if (imagesShow.Count != 0)
        {
            foreach (var obj in imagesShow)
            {
                Destroy(obj.itemGameObj);
            }
            imagesShow.Clear();
        }
    }
    private void CleanButtonCreateItemArea()
    {
        if (buttonCreate != null)
        {
            Destroy(buttonCreate);
        }
    }

    private void ChooseItem()
    {
        CleanNeedItemsArea();
        CleanButtonCreateItemArea();

        CraftItem craftItem = dataBaseCraft.craftItems[int.Parse(es.currentSelectedGameObject.name)];
        int countItemsNeed = craftItem.needItems.Count;
        for(int i = 0; i < countItemsNeed; i++)
        {
            GameObject newItem = Instantiate(gameImgShow, showNeedItemsArea.transform) as GameObject;
            newItem.name = craftItem.needItems[i].id.ToString();

            ItemInventory ii = new ItemInventory();
            ii.itemGameObj = newItem;

            RectTransform rt = newItem.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(0, 0, 0);
            rt.localScale = new Vector3(1, 1, 1);
            newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);
            ii.itemGameObj.GetComponentInChildren<Text>().text = craftItem.needItems[i].count.ToString();
            ii.itemGameObj.GetComponent<Image>().sprite = data.items[craftItem.needItems[i].id].img;

            imagesShow.Add(ii);
        }

        buttonCreate = Instantiate(buttonCreateItem, buttonCreateItemArea.transform) as GameObject;
        buttonCreate.name = es.currentSelectedGameObject.name;
        Button tempButton = buttonCreate.GetComponent<Button>();
        tempButton.onClick.AddListener(delegate { SumbitCreateItem(); });

    }

    private void SumbitCreateItem()
    {
        CraftItem craftItem = dataBaseCraft.craftItems[int.Parse(es.currentSelectedGameObject.name)];
        int countItemsNeed = craftItem.needItems.Count;
        for (int i = 0; i < countItemsNeed; i++)
        {
            int count = craftItem.needItems[i].count;
            int id = craftItem.needItems[i].id;
            for(int j = 0; j < items.Count; j++)
            {
                if (count == 0) break;
                if (inv.items[j].id == id)
                {
                    if (inv.items[j].count < count)
                    {
                        count -= inv.items[j].count;
                        inv.items[j].count = 0;
                        inv.items[j].id = 0;
                    }
                    else
                    {
                        inv.items[j].count -= count;
                        count = 0;
                    }
                }
            }
        }
        LootItem.CreateLootItem(inv.player.transform, craftItem.id, craftItem.count, inv.player, inv, data, inv.world, inv.lootItem);
        inv.UpdateInventory();
        UpdateCountItems();
        UpdateCraftPanel();
        if (!IsCanBeCrafted(craftItem))
        {
            CleanNeedItemsArea();
            CleanButtonCreateItemArea();
        }
    }
}
