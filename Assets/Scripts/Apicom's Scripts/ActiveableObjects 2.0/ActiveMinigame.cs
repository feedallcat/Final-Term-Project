﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class ActiveMinigame : ActiveableObject20 {

    public int S_need = 0;
    public int T_need = 0;
    public int E_need = 0;
    public int M_need = 0;
    public int Energy_Need = 0;

    public int Gain_S = 0;
    public int Gain_T = 0;
    public int Gain_E = 0;
    public int Gain_M = 0;
    public int Gain_Energy = 0;

    public int Battery_need = 0;
    public int Wrench_need = 0;

    public GameObject MinigameCanvas;
    public GameObject MinigameCamera;

    public UIFloating uiFloating;
    
    private UIFps uiFPS;
    private bool inRangeMinigame = false;

    private void Start()
    {
        MinigameCanvas.SetActive(false);
        MinigameCamera.SetActive(false);
        uiFPS = GameObject.FindGameObjectWithTag("Player").GetComponent<UIFps>();
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Player"))
        {
            inRangeMinigame = true;
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if (other.tag.Contains("Player"))
        {
            inRangeMinigame = false;
        }
    }

    void DoProcess()
    {
        if (uiFPS != null)
        {
            if (IsLock)
            {
                uiFPS.MidText.text = "";
            }
            else
            {
                if (EnoughStuff())
                {
                    uiFPS.MidText.text = "Press " + Press.ToString();
                }
                else
                {
                    string whatweneed = "Needs ";
                    if (S_need > GameManager.GM.S)
                    {
                        whatweneed += " [S x " + S_need + "]";
                    }
                    if (T_need > GameManager.GM.T)
                    {
                        whatweneed += " [T x " + T_need + "]";
                    }
                    if (E_need > GameManager.GM.E)
                    {
                        whatweneed += " [E x " + E_need + "]";
                    }
                    if (M_need > GameManager.GM.M)
                    {
                        whatweneed += " [M x " + M_need + "]";
                    }
                    if (Energy_Need > GameManager.GM.Energy)
                    {
                        whatweneed += " [Energy x " + Energy_Need + "]";
                    }

                    if (Battery_need > GameManager.GM.Battery)
                    {
                        whatweneed += " [Battery x " + Battery_need + "]";
                    }

                    if (Wrench_need > GameManager.GM.Wrench)
                    {
                        whatweneed += " [Wrench x " + Wrench_need + "]";
                    }
                    uiFPS.MidText.text = whatweneed;
                }

            }

        }
        if (Input.GetKeyDown(Press) &&
            !IsLock &&
            EnoughStuff())
        {

            Interact();
        }
    }

    bool EnoughStuff()
    {
        return (GameManager.GM.S >= S_need &&
                    GameManager.GM.T >= T_need &&
                    GameManager.GM.E >= E_need &&
                    GameManager.GM.M >= M_need &&
                    GameManager.GM.Energy >= Energy_Need &&
                    GameManager.GM.Battery >= Battery_need &&
                    GameManager.GM.Wrench >= Wrench_need);
    }

    public override void Interact()
    {
        uiFPS.FreezePlayer();
        uiFPS.ShowCursor();
        MinigameCamera.SetActive(true);
        MinigameCanvas.SetActive(true);
    }

    public void OnCorrect()
    {
        uiFPS.UnfreezePlayer();
        uiFPS.HideCursor();
        MinigameCamera.SetActive(false);
        MinigameCanvas.SetActive(false);

        Activated = true;
        IsLock = true;

        GameManager.GM.S += Gain_S;
        GameManager.GM.T += Gain_T;
        GameManager.GM.E += Gain_E;
        GameManager.GM.M += Gain_M;
        GameManager.GM.Energy += Gain_Energy;
        GameManager.GM.Battery -= Battery_need;
        GameManager.GM.Wrench -= Wrench_need;
        GameManager.GM.RefreshUI();

        if (TitleQuest != null && DescriptQuest != null)
        {
            TitleQuest.text = TitleQuestString;
            DescriptQuest.text = DescriptionQuestString;
        }
    }
    public void OnIncorrect()
    {
        uiFPS.UnfreezePlayer();
        uiFPS.HideCursor();
        MinigameCamera.SetActive(false);
        MinigameCanvas.SetActive(false);
    }

    public override void Update()
    {
        base.Update();

        if (uiFloating != null)
        {
            if (Activated || IsLock)
            {
                uiFloating.hide();
            }
            else
            {
                uiFloating.show();
            }
        }
        if (inRangeMinigame)
        {
            DoProcess();
        }
    }
}

