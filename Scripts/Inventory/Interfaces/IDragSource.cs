using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDragSource 
{
    Item GetItem();
    int GetNumber();
    void RemoveItem(int number);
}
