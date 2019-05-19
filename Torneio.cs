using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class Torneio : SelectionMethod
{
    public override List<Individual> selectIndividuals(List<Individual> oldpop, int num)
    {
        List<Individual> new_population = new List<Individual>();
        while (oldpop.Count > new_population.Count)
        { 
            float maximotour = float.MinValue;
            List<Individual> selection = new List<Individual>();
            
            Individual escolhagrupo = null;
            int rodada = 0;
            while (rodada < num)
            {
                selection.Add(oldpop[(int)Random.Range(0.0f, (float)oldpop.Count)]);
                rodada++;
            }
            rodada = 0;
            while (rodada < num)
            {
                maximotour = Mathf.Max(selection[rodada].Fitness, maximotour);
                rodada++;
            }
            for (int i = 0; i < num; i++)
            {
                if (selection[i].Fitness == maximotour)
                {
                    escolhagrupo = selection[i];
                }
            }
            new_population.Add(escolhagrupo);
        }
        return new_population;
    }

}
