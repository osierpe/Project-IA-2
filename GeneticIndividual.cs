using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticIndividual : Individual {


	public GeneticIndividual(int[] topology) : base(topology) {
	}

    public override void Initialize()
    {
        for (int i = 0; i < totalSize; i++)
        {
            genotype[i] = Random.Range(-1.0f, 1.0f);
        }
	}

    public override void Crossover(Individual partner, float probability)
    {

        if (Random.Range(0.0f, 1.0f) <= probability)
        {
            int cross1 = (int)Random.Range(0.0f, (float)(genotype.Length));
            int cross2 = (int)Random.Range(0.0f, (float)(genotype.Length));
            int scross = Mathf.Max(cross1, cross2);
            int pcross = Mathf.Min(cross1, cross2);
            for (int i = pcross; i < scross; i++)
            {
                float tmp1 = this.genotype[i];
                float tmp2 = partner.genotype[i];
                partner.genotype[i] = tmp1;
                this.genotype[i] = tmp2;
            }
        }
		
        
	}
 
	public override void Mutate (float probability)
	{
        for (int i = 0; i < totalSize; i++)
        {
            if (Random.Range(0.0f, 1.0f) < probability)
            {
                genotype[i] = Random.Range(-1.0f, 1.0f);
            }
        }
    }

	public override Individual Clone ()
	{
		GeneticIndividual new_ind = new GeneticIndividual(this.topology);

		genotype.CopyTo (new_ind.genotype, 0);
		new_ind.fitness = this.fitness;
		new_ind.evaluated = this.evaluated;

		return new_ind;
	}

}
