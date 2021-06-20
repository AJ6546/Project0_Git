using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDragDestination 
{
    int MaxAcceptable(Item item);
    void AddItems(Item item, int number);
}
