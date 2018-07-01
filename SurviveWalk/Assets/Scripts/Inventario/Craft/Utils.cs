using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils {

    public enum ResourceType { Comida, Madeira, Ferro, Machado , Picareta, Pedra, Galho };
    public enum HouseType { House1 = 0, House2 = 1, House3 = 2, House4 = 3 };
    public enum EnemyType { Skeleton = 0 };
    public enum NpcType { Npc0 = 0, Npc1 = 1, Npc2 = 2, Npc3 = 3, Npc4= 4, Npc5 = 5, Npc6 = 6 };

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
        }

        return PodeCraftarSemMaterial(resourceType);
    }

    public static bool PodeCraftarSemMaterial(ResourceType resourceType)
    {
        if (ResourceType.Comida == resourceType)
        {
            return true;
        }
        else if (ResourceType.Galho == resourceType)
        {
            return true;
        }
        else if (ResourceType.Pedra == resourceType)
        {
            return true;
        }
        return false;
    }

    public static string PodeCraftarDS(ResourceType resourceType)
    {
        if (ResourceType.Madeira == resourceType)
            return "Você precisa equipar um Machado para poder coletar esse item.";
        if (ResourceType.Ferro == resourceType)
            return "Você precisa equipar uma Picareta para poder coletar esse item.";
        return "Item não encontrado.";
    }
}
