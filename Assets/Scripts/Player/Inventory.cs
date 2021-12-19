using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    public DataBase data;
    public int maxStackItems = 999;

    public List<ItemInventory> items = new List<ItemInventory>();
    public List<ItemInventory> imagesShow = new List<ItemInventory>();

    public GameObject gameObjShow;
    public GameObject gameImgShow;
    public GameObject lootItem;
    public GameObject player;
    public GameObject world;

    public GameObject InventoryMainObject;
    public GameObject InventoryQuickAccessCells;
    public GameObject ImagesQuickAccessCells;
    public int maxCount;

    public Camera cam;
    public EventSystem es;

    public int currentID;
    public int currentUseID = 1;
    public ItemInventory currentItem;

    public RectTransform movingObject;
    public Vector3 offset;

    public GameObject backGround;
    public GameObject backGroundImages;
    public void Start()
    {
        if (items.Count == 0)
        {
            AddGraphics();
        }

        for (int i = 0; i < maxCount; i++) // тест, заполнить рандомные €чейки
        {
            Item tempItem = data.items[Random.Range(1, data.items.Count)];
            if (tempItem.id != 0)
            {
                AddItem(i, tempItem, Random.Range(999, maxStackItems));
            }
            else
            {
                AddItem(i, tempItem, 0);
            }
        }
        UpdateInventory();
    }

    public void Update()
    {
        if (currentID != -1)
        {
            MoveObject();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            backGround.SetActive(!backGround.activeSelf);
            backGroundImages.SetActive(!backGroundImages.activeSelf);
            UpdateInventory();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentUseID = 1;
            UpdateInventory();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentUseID = 2;
            UpdateInventory();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentUseID = 3;
            UpdateInventory();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentUseID = 4;
            UpdateInventory();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            currentUseID = 5;
            UpdateInventory();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            currentUseID = 6;
            UpdateInventory();
        }
    }
    public void AddItem(int id, Item item, int count)
    {
        items[id].id = item.id;
        items[id].count = count;
        items[id].itemGameObj.GetComponent<Image>().sprite = item.img;

        if (count >= 1 && item.id != 0)
        {
            items[id].itemGameObj.GetComponentInChildren<Text>().text = count.ToString();

        }
        else
        {
            items[id].itemGameObj.GetComponentInChildren<Text>().text = "";
        }
    }

    public void AddInventoryItem(int id, ItemInventory invItem)
    {
        items[id].id = invItem.id;
        items[id].count = invItem.count;
        items[id].itemGameObj.GetComponent<Image>().sprite = data.items[invItem.id].img;

        if (invItem.count > 1 && invItem.id != 0)
        {
            items[id].itemGameObj.GetComponentInChildren<Text>().text = invItem.count.ToString();

        }
        else
        {
            items[id].itemGameObj.GetComponentInChildren<Text>().text = "";
        }
    }

    public void AddGraphics()
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject newItem = Instantiate(gameImgShow, ImagesQuickAccessCells.transform) as GameObject;
            newItem.name = i.ToString();

            ItemInventory ii = new ItemInventory();
            ii.itemGameObj = newItem;

            RectTransform rt = newItem.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(0, 0, 0);
            rt.localScale = new Vector3(1, 1, 1);
            newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

            imagesShow.Add(ii);
        }
        for (int i = 0; i < 6; i++)
        {
            GameObject newItem = Instantiate(gameObjShow, InventoryQuickAccessCells.transform) as GameObject;

            newItem.name = i.ToString();

            ItemInventory ii = new ItemInventory();
            ii.itemGameObj = newItem;

            RectTransform rt = newItem.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(0, 0, 0);
            rt.localScale = new Vector3(1, 1, 1);
            newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

            Button tempButton = newItem.GetComponent<Button>();

            tempButton.onClick.AddListener(delegate { SelectObject(); });

            items.Add(ii);
        }
        for (int i = 6; i < maxCount; i++)
        {
            GameObject newItem = Instantiate(gameObjShow, InventoryMainObject.transform) as GameObject;

            newItem.name = i.ToString();

            ItemInventory ii = new ItemInventory();
            ii.itemGameObj = newItem;

            RectTransform rt = newItem.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(0, 0, 0);
            rt.localScale = new Vector3(1, 1, 1);
            newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

            Button tempButton = newItem.GetComponent<Button>();

            tempButton.onClick.AddListener(delegate { SelectObject(); });

            items.Add(ii);
        }
    }

    public void UpdateInventory()
    {
        for (int i = 0; i < 6; i++)
        {
            if (items[i].id != 0 && items[i].count > 1)
            {
                items[i].itemGameObj.GetComponentInChildren<Text>().text = items[i].count.ToString();
            }
            else
            {
                items[i].itemGameObj.GetComponentInChildren<Text>().text = "";
            }
            if(currentUseID == i + 1)
            {
                items[i].itemGameObj.gameObject.transform.Find("Number").GetComponent<Text>().text = (i + 1).ToString();
                items[i].itemGameObj.gameObject.transform.Find("Number").GetComponent<Text>().color = new Color(255, 255, 0, 255);
            }
            else
            {
                items[i].itemGameObj.gameObject.transform.Find("Number").GetComponent<Text>().text = (i + 1).ToString();
                items[i].itemGameObj.gameObject.transform.Find("Number").GetComponent<Text>().color = new Color(255, 255, 255);
            }
            items[i].itemGameObj.GetComponent<Image>().sprite = data.items[items[i].id].img;
        }
        for (int i = 6; i < maxCount; i++)
        {
            if (items[i].id != 0 && items[i].count > 1)
            {
                items[i].itemGameObj.GetComponentInChildren<Text>().text = items[i].count.ToString();
            }
            else
            {
                items[i].itemGameObj.GetComponentInChildren<Text>().text = "";
            }

            items[i].itemGameObj.GetComponent<Image>().sprite = data.items[items[i].id].img;
        }

        for(int i = 0; i < 6; i++)
        {
            imagesShow[i].itemGameObj.GetComponentInChildren<Text>().text = items[i].itemGameObj.GetComponentInChildren<Text>().text;
            imagesShow[i].itemGameObj.GetComponent<Image>().sprite = items[i].itemGameObj.GetComponent<Image>().sprite;
            imagesShow[i].itemGameObj.gameObject.transform.Find("Number").GetComponent<Text>().text = items[i].itemGameObj.gameObject.transform.Find("Number").GetComponent<Text>().text;
            imagesShow[i].itemGameObj.gameObject.transform.Find("Number").GetComponent<Text>().color = items[i].itemGameObj.gameObject.transform.Find("Number").GetComponent<Text>().color;
        }
    }
    public void SelectObject()
    {
        if (data.items[CopyInventoryItem(items[int.Parse(es.currentSelectedGameObject.name)]).id].id == 0 && !movingObject.gameObject.activeSelf)
            return;
        if (currentID == -1)
        {
            currentID = int.Parse(es.currentSelectedGameObject.name);
            currentItem = CopyInventoryItem(items[currentID]);
            movingObject.gameObject.SetActive(true);
            movingObject.GetComponent<Image>().sprite = data.items[currentItem.id].img;

            AddItem(currentID, data.items[0], 0);
        }
        else
        {
            ItemInventory II = items[int.Parse(es.currentSelectedGameObject.name)];

            if (currentItem.id != II.id)
            {

                AddInventoryItem(currentID, II);

                AddInventoryItem(int.Parse(es.currentSelectedGameObject.name), currentItem);
            }
            else
            {
                if (II.count + currentItem.count <= maxStackItems)
                {
                    II.count += currentItem.count;
                }
                else
                {
                    AddItem(currentID, data.items[II.id], II.count + currentItem.count - maxStackItems);

                    II.count = maxStackItems;
                }

                if (II.count != 0)
                {
                    II.itemGameObj.GetComponentInChildren<Text>().text = II.count.ToString();
                }
            }
            currentID = -1;

            movingObject.gameObject.SetActive(false);
        }
    }
    public void MoveObject()
    {
        Vector3 pos = Input.mousePosition + offset;
        pos.z = InventoryMainObject.GetComponent<RectTransform>().position.z;
        movingObject.position = cam.ScreenToWorldPoint(pos);
    }

    public ItemInventory CopyInventoryItem(ItemInventory old)
    {
        ItemInventory New = new ItemInventory();

        New.id = old.id;
        New.itemGameObj = old.itemGameObj;
        New.count = old.count;

        return New;

    }

    public void InventoryDropout()
    {
        GameObject obj;
        for(int i = 0; i < maxCount; i++)
        {
            if(items[i].id != 0)
            {
                obj = LootItem.CreateLootItem(player.transform, items[i].id, items[i].count, player, this, data, world, lootItem);
                obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-250f, 250f), Random.Range(0, 3000f)));

                items[i].id = 0;
                items[i].count = 0;
            }
        }
        UpdateInventory();
    }
}

[System.Serializable]

public class ItemInventory
{
    public int id;
    public GameObject itemGameObj;

    public int count;
}