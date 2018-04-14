using UnityEngine;
using LitJson;
using System.Collections.Generic;
using System.IO;

public class ItemDatabase : MonoBehaviour {
	private List<Item> database = new List<Item>();
	private JsonData itemData;

    private List<CraftItem> craftbase = new List<CraftItem>();
    private JsonData craftData;

    void Start()
	{
		itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
        craftData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Crafts.json"));
        ConstructItemDatabase();	
	}

	public Item FetchItemById(int id)
	{
		for (int i = 0; i < database.Count; i++)
		{
			if (id.Equals(database[i].Id))
			{
				return database[i];
			}
		}
		return null;
	}

    public List<CraftItem> GetCraftList()
    {
        return craftbase;
    }

    void ConstructItemDatabase()
	{
		for (int i = 0; i < itemData.Count; i++)
		{
			Item newItem = new Item();
			newItem.Id = (int)itemData[i]["id"];
			newItem.Title = itemData[i]["title"].ToString();
			newItem.Value = (int)itemData[i]["value"];
            newItem.Type = itemData[i]["type"].ToString();
            newItem.Power = (int)itemData[i]["stats"]["power"];
			newItem.Defense = (int)itemData[i]["stats"]["defense"];
			newItem.Vitality = (int)itemData[i]["stats"]["vitality"];
			newItem.Description = itemData[i]["description"].ToString();
			newItem.Stackable = (bool)itemData[i]["stackable"];
			newItem.Rarity = (int)itemData[i]["rarity"];
			newItem.Slug = itemData[i]["slug"].ToString();
			newItem.Sprite = Resources.Load<Sprite>("Sprites/Items/" + newItem.Slug);

			database.Add(newItem);
		}

        for (int k = 0; k < craftData.Count; k++)
        {
            CraftItem craftItem = new CraftItem();
            craftItem.Id = (int)craftData[k]["id"];

            for(int j = 0; j < craftData[k]["combination"].Count; j++)
            {
                Combination combination = new Combination();
                combination.Id = (int)craftData[k]["combination"][j]["id"];
                combination.Qt = (int)craftData[k]["combination"][j]["qt"];

                craftItem.Combination.Add(combination);
            }

            craftbase.Add(craftItem);
        }
    }
}

public class Item
{
	public int Id { get; set; }
	public string Title { get; set; }
	public int Value { get; set; }
    public string Type { get; set; }
    public int Power { get; set; }
	public int Defense { get; set; }
	public int Vitality { get; set; }
	public string Description { get; set; }
	public bool Stackable { get; set; }
	public int Rarity { get; set; }
	public string Slug { get; set; }
	public Sprite Sprite { get; set; }

	public Item()
	{
		this.Id = -1;
	}
}

public class Combination
{
    public int Id { get; set; }
    public int Qt { get; set; }
}