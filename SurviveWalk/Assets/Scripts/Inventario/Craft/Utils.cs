using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils {

    public enum ResourceType { Comida, Madeira, Ferro, Machado };

    [System.Serializable]
    public class CraftItem
    {
        public int idItem;
        public List<CraftCombination> craftCombination;
    }

    [System.Serializable]
    public class CraftCombination
    {
        public int idItem;
        public int qnt;
    }
}
