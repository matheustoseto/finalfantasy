using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemResource : System.Object
{
    public int idItem   = -1;
    public int idQuest  = -1;
    public int idTask   = -1;

    public ItemResource()
    {
        this.idItem   = -1;
        this.idQuest  = -1;
        this.idTask   = -1;
    }

    public ItemResource(int idItem, int idQuest, int idTask)
    {
        this.idItem = idItem;
        this.idQuest = idQuest;
        this.idTask = idTask;
    }
}
