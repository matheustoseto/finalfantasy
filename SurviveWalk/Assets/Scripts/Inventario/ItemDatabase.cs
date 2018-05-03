using UnityEngine;
using LitJson;
using System.Collections.Generic;
using System.IO;

public class ItemDatabase : MonoBehaviour {

    // Items.json
    private List<Item> database = new List<Item>();
	private JsonData itemData;

    // Crafts.json
    private List<CraftItem> craftbase = new List<CraftItem>();
    private JsonData craftData;

    // HouseCrafts.json
    private List<CraftHouse> housebase = new List<CraftHouse>();
    private JsonData houseData;

    void Start()
	{
		itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
        craftData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Crafts.json"));
        houseData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/HouseCrafts.json"));
        ConstructItemDatabase();	
	}

	public Item FetchItemById(int id)
	{
		for (int i = 0; i < database.Count; i++)
		{
			if (database[i].Id.Equals(id))
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

    public List<CraftHouse> GetHouseList()
    {
        return housebase;
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
            newItem.Durability = (int)itemData[i]["durability"];
            newItem.DurabilityCount = (int)itemData[i]["durability"];
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

        for (int i = 0; i < houseData.Count; i++)
        {
            CraftHouse craftHouse = new CraftHouse();
            craftHouse.Id = (int)houseData[i]["id"];
            craftHouse.Title = houseData[i]["title"].ToString();

            for (int j = 0; j < houseData[i]["levels"].Count; j++)
            {
                HouseLevel houseLevel = new HouseLevel();
                houseLevel.IdLevel = (int)houseData[i]["levels"][j]["idLevel"];
                houseLevel.Title = houseData[i]["levels"][j]["title"].ToString();

                for (int k = 0; k < houseData[i]["levels"][j]["combination"].Count; k++)
                {
                    Combination combination = new Combination();
                    combination.Id = (int)houseData[i]["levels"][j]["combination"][k]["id"];
                    combination.Qt = (int)houseData[i]["levels"][j]["combination"][k]["qt"];

                    houseLevel.Combination.Add(combination);
                }

                craftHouse.houseLevel.Add(houseLevel);
            }

            housebase.Add(craftHouse);
        }
    }
}

public class Item
{
	public int Id { get; set; }
    public int Slot { get; set; }
    public string Title { get; set; }
	public int Value { get; set; }
    public string Type { get; set; }
    public int Power { get; set; }
	public int Defense { get; set; }
	public int Vitality { get; set; }
	public string Description { get; set; }
	public bool Stackable { get; set; }
	public int Rarity { get; set; }
    public int Durability { get; set; }
    public int DurabilityCount { get; set; }
    public string Slug { get; set; }
	public Sprite Sprite { get; set; }

	public Item()
	{
		this.Id = -1;
	}

    public Item(Item item)
    {
        this.Id = item.Id;
        this.Slot = item.Slot;
        this.Title = item.Title;
        this.Value = item.Value;
        this.Type = item.Type;
        this.Power = item.Power;
        this.Defense = item.Defense;
        this.Vitality = item.Vitality;
        this.Description = item.Description;
        this.Stackable = item.Stackable;
        this.Rarity = item.Rarity;
        this.Durability = item.Durability;
        this.DurabilityCount = item.DurabilityCount;
        this.Slug = item.Slug;
        this.Sprite = item.Sprite;
    }

    public override bool Equals(object obj)
    {
        var item = obj as Item;
        return item != null &&
               Id == item.Id &&
               Slot == item.Slot &&
               Title == item.Title &&
               Value == item.Value &&
               Type == item.Type &&
               Power == item.Power &&
               Defense == item.Defense &&
               Vitality == item.Vitality &&
               Description == item.Description &&
               Stackable == item.Stackable &&
               Rarity == item.Rarity &&
               Durability == item.Durability &&
               DurabilityCount == item.DurabilityCount &&
               Slug == item.Slug &&
               EqualityComparer<Sprite>.Default.Equals(Sprite, item.Sprite);
    }

    public override int GetHashCode()
    {
        var hashCode = 745842380;
        hashCode = hashCode * -1521134295 + Id.GetHashCode();
        hashCode = hashCode * -1521134295 + Slot.GetHashCode();
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Title);
        hashCode = hashCode * -1521134295 + Value.GetHashCode();
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Type);
        hashCode = hashCode * -1521134295 + Power.GetHashCode();
        hashCode = hashCode * -1521134295 + Defense.GetHashCode();
        hashCode = hashCode * -1521134295 + Vitality.GetHashCode();
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
        hashCode = hashCode * -1521134295 + Stackable.GetHashCode();
        hashCode = hashCode * -1521134295 + Rarity.GetHashCode();
        hashCode = hashCode * -1521134295 + Durability.GetHashCode();
        hashCode = hashCode * -1521134295 + DurabilityCount.GetHashCode();
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Slug);
        hashCode = hashCode * -1521134295 + EqualityComparer<Sprite>.Default.GetHashCode(Sprite);
        return hashCode;
    }
}

public class CraftHouse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public List<HouseLevel> houseLevel = new List<HouseLevel>();
}

public class HouseLevel
{
    public int IdLevel { get; set; }
    public string Title { get; set; }
    public List<Combination> Combination = new List<Combination>();
}

public class Combination
{
    public int Id { get; set; }
    public int Qt { get; set; }
}