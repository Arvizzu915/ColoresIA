using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Brain : MonoBehaviour
{
    [SerializeField] private Color[] colors;
    private List<Color> elegibles = new();
    private List<Color> selected = new();
    private List<Color> currentSelected = new();

    private int correct = 0, tries = 0;

    public void AsignColors()
    {
        selected.Clear();

        for (int i = 0; i < 4; i++)
        {
            selected.Add(colors[Random.Range(0, colors.Length)]);
        }

        for (int i = 0;i < colors.Length; i++)
        {
            elegibles.Add(colors[i]);
        }
    }

    public void Try()
    {
        currentSelected.Clear();

        for (int i = 0; i < 4; i++)
        {
            currentSelected.Add(elegibles[Random.Range(0, elegibles.Count)]);

        }
    }

    private void SelectColors()
    {

    }
}
