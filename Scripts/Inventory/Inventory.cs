﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void OnButtonPressed()
    {
        gameObject.SetActive(false);

    }
}
