using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticAlgorithm : MetaHeuristic {
	public float mutationProbability;
	public float crossoverProbability;
	public int tournamentSize;
	public bool elitist;
    public int num_elit;
    

	public override void InitPopulation () {
        //You should implement the code to initialize the population herez
        selection = new Torneio();
        population = new List<Individual>();
        // jncor 
        while (population.Count < populationSize)
        {
            GeneticIndividual new_ind = new GeneticIndividual(topology);
            new_ind.Initialize();
            population.Add(new_ind);
        }
	}

    //The Step function assumes that the fitness values of all the individuals in the population have been calculated.
    public override void Step() {
        //You should implement the code which runs in each generation here
        List<Individual> new_pop = new List<Individual>();
        List<Individual> temporary = new List<Individual>();
        //fazer elitismo
        updateReport();
        if (elitist)
        {
            
            for (int i = 0; i < num_elit && i < populationSize; i++)
            {
                
                Individual tmp = population[i];
                new_pop.Add(tmp.Clone());
            }
        }
        //fazer seleção por torneio
        temporary = selection.selectIndividuals(population, tournamentSize);

        //fazer crossover
        if (populationSize > 1)
        {
            int con = 0;
            Individual primeiro = temporary[0].Clone();
            int indice = new_pop.Count;
            while (indice < populationSize)//faz cross over dois a dois, faz a mutação dos descendentes
            {                                                     //e preenche a nova população
                temporary[con].Crossover(temporary[con + 1], crossoverProbability);
                temporary[con].Mutate(mutationProbability);
                if (new_pop.Count < populationSize)
                {
                    new_pop.Add(temporary[con]);
                }
                else
                {
                    break;
                }
                temporary[con + 1].Mutate(mutationProbability);
                if (new_pop.Count < populationSize)
                {
                    new_pop.Add(temporary[con + 1]);
                }
                else
                {
                    break;
                }
                con += 2;
                indice = new_pop.Count;
            }
            if (new_pop.Count != populationSize) //se a nova população tiver um numero impar de individuos
            {                                    //a recombinação da a volta no array porem só gera o descendente do ultimo individuo
                primeiro.Crossover(temporary[temporary.Count], crossoverProbability);
                temporary[temporary.Count].Mutate(mutationProbability);
                new_pop.Add(temporary[temporary.Count - 1]);
            }
        }
        else
        {
            population[0].Mutate(mutationProbability);
            new_pop.Add(population[0]);
        }
        //termino do passo

        population = new_pop;
        generation++;
    }

}
