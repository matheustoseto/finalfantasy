using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils {

    public enum ResourceType { Comida, Madeira, Ferro, Machado , Picareta };
    public enum HouseType { House1 = 0, House2 = 1, House3 = 2, House4 = 3 };

    public static bool PodeCraftar(ResourceType resourceType, Item item)
    {
        if (ResourceType.Madeira == resourceType)
        {
            if (item != null && ResourceType.Machado.ToString().Equals(item.Type))
            {
                return true;
            }
        } else if (ResourceType.Ferro == resourceType)
        {
            if (item != null &&  ResourceType.Picareta.ToString().Equals(item.Type))
            {
                return true;
            }
        } else if (ResourceType.Comida == resourceType)
        {
            return true;
        }
        return false;
    }

    public static string PodeCraftarDS(ResourceType resourceType)
    {
        if (ResourceType.Madeira == resourceType)
            return "Você precisa equipar um Machado para poder craftar esse item.";
        if (ResourceType.Ferro == resourceType)
            return "Você precisa equipar uma Picareta para poder craftar esse item.";
        return "Item não encontrado.";
    }
}
