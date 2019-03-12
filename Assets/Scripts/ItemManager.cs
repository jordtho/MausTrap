using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager> {

    protected ItemManager() { }

    //public ItemContainer m_ItemContainerPrefab;

    public List<Item> m_Items;

    public bool m_RandomizeAllChests = false;

    public List<Chest> m_Chests;

    void Awake() {

        //Collect all chests into list
        m_Chests.Clear();

        m_Chests = new List<Chest>(FindObjectsOfType<Chest>());

        //Randomize chests
        for(int i = 0; i < m_Chests.Count; i++) {

            if(m_RandomizeAllChests) { m_Chests[i].m_RandomizeContents = true; }

            m_Chests[i].InitializeItemContents();

            //if(m_Chests[i].m_RandomizeContents || m_RandomizeAllChests) {
            //    m_Chests[i].GenerateRandomContents();
            //}
        }
    }

    public Item RandomItem() {

        Item item;

        item = m_Items[Random.Range(0, m_Items.Count)];

        return item;
    }
}
