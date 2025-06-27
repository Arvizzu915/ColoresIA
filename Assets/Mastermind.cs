using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Analytics;
using UnityEditor.Search;
using UnityEngine;

public class Mastermind : MonoBehaviour
{
    public ColorBall[] colors;
    private List<ColorBall> assignableColors = new();
    private List<ColorBall> selectableColors = new();
    public ColorBall[] answer;
    public ColorBall[] selected;

    public int correct;
    public int answered;
    public int tries;
    public ColorBall[] knownColors;
    public int KnownColorsIndex;

    public int wins = 0;

    public void AssignColors()
    {
        assignableColors.Clear();
        selectableColors.Clear();

        for (int i = 0; i < colors.Length; i++)
        {
            assignableColors.Add(colors[i]);
            selectableColors.Add(colors[i]);
        }

        for (int i = 0; i < 4; i++)
        {
            int pick = Random.Range(0, colors.Length);
            answer[i].color = colors[pick].color;
            answer[i].place = i;
            assignableColors.Remove(assignableColors[pick]);
        }

        for (int i = 0; i < 4; i++)
        {
            selected[i] = null;
        }
    }


    public void Play()
    {
        for (int i = 0; i < 100; i++)
        {
            Try();
        }



        Debug.Log(wins);
    }
    private void Try()
    {
        for (int i = 0; i < 4; i++)
        {
            if (selected[i] == null)
            {
                selected[i].color = colors[i].color;
                selected[i].place = i;
            }
        }

        Check(selected, answer);
        

        if (correct + answered >= 2)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    selected[j].color = colors[i].color;
                    selected[j].place = j;
                }

                Check(selected, answer);
                if (answered >= 1)
                {
                    knownColors[KnownColorsIndex].color = selected[i].color;
                    KnownColorsIndex++;
                    assignableColors.Remove(selected[i]);
                }
                else
                {
                    assignableColors.Remove(selected[i]);
                }
            }

            if (KnownColorsIndex == 2)
            {
                for (int i = 0; i < 3; i++)
                {
                    selected[0].color = knownColors[0].color;
                    selected[1].color = knownColors[1].color;
                    selected[2].color = assignableColors[i].color;
                    selected[3].color = assignableColors[i+1].color;

                    Check(selected, answer);

                    if (correct + answered >= 3)
                    {
                        knownColors[2].color = selected[2].color;
                        knownColors[3].color = selected[3].color;
                        KnownColorsIndex++;
                        break;
                    }
                }
                
            }
            
            if (KnownColorsIndex == 3)
            {
                selected[3].color = assignableColors[assignableColors.Count].color;
                Check(selected, answer);

                if (correct + answered >= 3)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        selected[0].color = knownColors[0].color;
                        selected[1].color = knownColors[1].color;
                        selected[2].color = knownColors[0].color;
                        selected[3].color = assignableColors[i].color;

                        Check(selected, answer);

                        if (correct + answered == 4)
                        {
                            KnownColorsIndex++;
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        selected[0].color = knownColors[0].color;
                        selected[1].color = knownColors[1].color;
                        selected[2].color = assignableColors[i].color;
                        selected[3].color = knownColors[0].color;

                        Check(selected, answer);

                        if (correct + answered == 4)
                        {
                            KnownColorsIndex++;
                            break;
                        }
                    }
                }
            }

            if (KnownColorsIndex == 4)
            {
                for (int i = 0; i < 4; i++)
                {

                }
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 4; j < 7; j++)
                {
                    selected[j].color = colors[i].color;
                    selected[j].place = j;
                }

                Check(selected, answer);
                if (answered >= 1)
                {
                    knownColors[KnownColorsIndex].color = selected[i].color;
                    KnownColorsIndex++;
                    assignableColors.Remove(selected[i]);
                }
                else
                {
                    assignableColors.Remove(selected[i]);
                }
            }

            if (KnownColorsIndex == 2)
            {
                for (int i = 0; i < 3; i++)
                {
                    selected[0].color = knownColors[0].color;
                    selected[1].color = knownColors[1].color;
                    selected[2].color = assignableColors[i].color;
                    selected[3].color = assignableColors[i + 1].color;

                    Check(selected, answer);

                    if (correct + answered >= 3)
                    {
                        knownColors[2].color = selected[2].color;
                        knownColors[3].color = selected[3].color;
                        KnownColorsIndex++;
                        break;
                    }
                }

            }

            if (KnownColorsIndex == 3)
            {
                selected[3].color = assignableColors[assignableColors.Count].color;
                Check(selected, answer);

                if (correct + answered >= 3)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        selected[0].color = knownColors[0].color;
                        selected[1].color = knownColors[1].color;
                        selected[2].color = knownColors[0].color;
                        selected[3].color = assignableColors[i].color;

                        Check(selected, answer);

                        if (correct + answered == 4)
                        {
                            KnownColorsIndex++;
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        selected[0].color = knownColors[0].color;
                        selected[1].color = knownColors[1].color;
                        selected[2].color = assignableColors[i].color;
                        selected[3].color = knownColors[0].color;

                        Check(selected, answer);

                        if (correct + answered == 4)
                        {
                            KnownColorsIndex++;
                            break;
                        }
                    }
                }
            }

            if (KnownColorsIndex == 4)
            {
                for (int i = 0; i < 4; i++)
                {

                }
            }
        }
    }

    private void Check(ColorBall[] selectedC, ColorBall[] answerC)
    {
        correct = 0;
        answered = 0;

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (selectedC[i].color == answer[j].color)
                {
                    if (selectedC[i].place == answerC[j].place)
                    {
                        answered++;
                    }
                    else
                    {
                        correct++;
                    }
                }
            }
        }

        tries++;
    }
}


/*
 usar los primeros 4
si tienen al menos 2 buenas, usar los primero 3 colores. si no, usar los primeros 3 colores empezando por el 5to
usar los que ya sabes y agregar los restantes hasta encontrar los 4 que son
si ya tenias 1 acomodada, primero dejar la mas baja en donde estaba en el primer intento.
si se queda, ahí dejarla he ir cambiando de lugar la otra que ya sabes (seguir añadiendo colores)

si en el primer intento no tenias acomodados, cambiar todos de lugar
ya que sepas los 4, rotarlos hasta que tengas 2 acomodados
deja los primeros 2 y cambia los otros 2
luego deja los segundos 2 y cambia los primero 2
luego deja el primero y el 3ro y cambia el 2do y el 4to y luego al reves

 */