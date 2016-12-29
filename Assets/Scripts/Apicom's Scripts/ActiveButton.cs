﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveButton : ActiveableObject
{
    public string LockedText = "";
    public string ActiveableText = "";

    public override void Update()
    {
        base.Update();

        if (Locked)
        {
            StatusText = LockedText;
        }
        else
        {
            if(!Activated || Toggle)
            {
                StatusText = ActiveableText;
            }else
            {
                StatusText = "";
            }
        }

        StatusColor();
    }

    private void StatusColor()
    {
        if (Activated)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }else
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
    }
}