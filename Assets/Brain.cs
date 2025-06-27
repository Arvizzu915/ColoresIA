using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Brain : MonoBehaviour
{
    [SerializeField] private ColorBall[] colors;
    public List<ColorBall> elegiblesForAssign = new();
    public List<ColorBall> elegibles = new();
    public ColorBall[] selected;
    public ColorBall[] currentSelected;

    private int index = 0;

    private int correct = 0, tries = 0;

    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            currentSelected[i] = null;
        }
    }

    public void AssignColors()
    {
        elegiblesForAssign.Clear();
        elegibles.Clear();

        for (int i = 0; i < colors.Length; i++)
        {
            elegiblesForAssign.Add(colors[i]);
        }

        for (int i = 0; i < 4; i++)
        {
            int assignPick = Random.Range(0, elegiblesForAssign.Count);
            selected[i].color = elegiblesForAssign[assignPick].color;
            selected[i].place = i;
            elegiblesForAssign.Remove(elegiblesForAssign[assignPick]);
        }

        for (int i = 0;i < colors.Length; i++)
        {
            elegibles.Add(colors[i]);
        }
    }

    public void Try()
    {
        correct = 0;
        index = 0;
        tries++;

        for (int i = 0; i < 4; i++)
        {
            if (currentSelected[i] == null)
            {
                int pick = Random.Range(0, elegibles.Count);
                currentSelected[i] = elegibles[pick];
                currentSelected[i].place = i;
                elegibles.Remove(elegibles[pick]);
            }
        }

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (currentSelected[i].color == selected[j].color)
                {
                    currentSelected[i].colorSelected = true;
                    if (currentSelected[i].place == selected[j].place)
                    {
                        currentSelected[i].isInPlace = true;
                        correct++;
                    }
                }
            }
        }

        if (correct == 4)
        {
            Win();
            return;
        }

        for (int i = 0; i < 4; i++)
        {
            if (!currentSelected[i].colorSelected && !currentSelected[i].isInPlace)
            {
                currentSelected[i] = null;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            if (currentSelected[i] != null)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (currentSelected[j] == null)
                    {
                        index = j;
                        break;
                    }
                }

                currentSelected[i].place = index;
                currentSelected[index] = currentSelected[i];
                currentSelected[i] = null;

                index = 0;
            }
        }

        if (tries == 10)
        {
            Lose();
        }
    }

    private void Win()
    {
        Debug.Log("won in " + tries.ToString());
    }

    private void Lose()
    {
        Debug.Log("10 tries, you lose womp womp");
    }
        
}
