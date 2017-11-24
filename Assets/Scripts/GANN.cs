using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NNet;
using genotype;
using MathNN;

namespace evolution
{
    public class GANN
    {
        public static float MAXPERMUT=0.3f ;
        public static float Mutation_rate = 0.15f;
        private int population_size { get; set; }
        private int generation { get; set; }
        private List<Genome> population { get; set; }
        private int genomeid { get; set; }
        private int current_genome { get; set; }
        
        //Constructors 
        public GANN()
        {
            current_genome = -1;
            genomeid = 0;
            generation = 1;
            population_size = 0;
            this.population = new List<Genome>();
        }
        public void clearpopulation()
        {
            population.Clear();
        }
        //Elle genere une population dont les poids sont random
        public void generateNewPopulation(int totalpop , int totalweights)
        {
            this.generation = 1;
            this.population_size = totalpop;
            clearpopulation();
            this.current_genome = -1;
            this.population = new List<Genome>();
            for(int i=0; i < population_size; i++)
            {
                Genome genome1 = new Genome();
                genome1.id = genomeid;
                genome1.fitness = 0;
                genome1.weights = new List<float>(totalweights);
                for (int j = 0; j < totalweights; j++)
                {
                    // on va ajouter les poids dans la liste des poids du genome 
                    genome1.weights.Add(MathHelper.Randomsigmoidvalue());
                }
                genomeid++;
                this.population.Add(genome1);
            }
        }
        // elle permet de mettre a jour les fitness pour un genome a un index particulier
        public void setgenomefitness(float fit , int index)
        {
            if (index<0 || index > this.population_size)
            {
                return;
            }
            this.population[index].fitness = fit; 
        }
        // return the next genome in a given population 
        public Genome getNextgenome()
        {
            this.current_genome++;
            if (this.current_genome > this.population_size)
            {
                return null;
            }
            else
            {
                return this.population[this.current_genome];
            }
        }
      
        private List<Genome> getBestgenomes(int nbre_genome)
        {
            
            List<Genome> Best_genome = new List<Genome>(nbre_genome);
            List<Genome> population2 = new List<Genome>(this.population_size);
            population2 = this.population;
            population2.Sort((a, b) => a.fitness.CompareTo(b.fitness)); 
            for(int i=0; i < nbre_genome; i++)
            {
                
                Best_genome.Add(population2[population2.Count() - i]);
            }
            return Best_genome;
                      
        }

        /* Cross over
        */
        private List<Genome> crossover(Genome g_parent1 , Genome g_parent2)
        {
            Genome g_child1 = new Genome();
            Genome g_child2 = new Genome();
            List<Genome> g_child = new List<Genome>();
            g_child1.id = this.genomeid;
            g_child1.weights = new List<float>();

            genomeid++;
            g_child2.id = this.genomeid;
            g_child2.weights = new List<float>();
            genomeid++;
            // on va choisir maintenant le point ou on fera le crossover (ce point sera aléatoire )
            Random ran1 = new Random();
            int cross_point = ran1.Next(g_parent1.weights.Count()); //génére un nombre entre 0 et le nombre total de poids du parent 
            for(int i = 0; i < cross_point; i++)
            {
                g_child1.weights.Add(g_parent1.weights[i]);
                g_child2.weights.Add(g_parent2.weights[i]);
            }
            for(int j = cross_point; j < g_parent1.weights.Count(); j++)
            {
                g_child1.weights.Add(g_parent2.weights[j]);
                g_child2.weights.Add(g_parent1.weights[j]);
            }
            g_child.Add(g_child1);
            g_child.Add(g_child2);

            return g_child;
        }
       /* on va créer une fonction de mutation qui permet génerer des mutations aléatoire du genotype 
        ceci ce fera d'une facon aléatoire en fait si randomvalue<Mutation rate alors ...*/
        private void mutate(Genome genome)
        {
            for (int i = 0; i < genome.weights.Count(); i++)
            {
                float rand = MathHelper.Randomsigmoidvalue();
                if (rand < Mutation_rate)
                {
                    float a = genome.weights[i];
                    genome.weights[i] = a + rand * MAXPERMUT;
                }
            }
        }

      
         public void create_generation_child ()
        {
            List<Genome> Bestgenomes = new List<Genome>();
            List<Genome> children = new List<Genome>();
            Bestgenomes = getBestgenomes(4);
            Genome Bestgenome = new Genome();
            Bestgenome.fitness = 0;
            Bestgenome.weights = Bestgenomes[0].weights;
            Bestgenome.id = Bestgenomes[0].id;
            mutate(Bestgenome);
            children.Add(Bestgenome);
            // on va créer maintenant les enfants issues du cross over 
            List<Genome> crossed_over = new List<Genome>();
            crossed_over = crossover(Bestgenomes[0], Bestgenomes[1]);
            mutate(crossed_over[0]);
            mutate(crossed_over[1]);
            children.Add(crossed_over[0]);
            children.Add(crossed_over[1]);
            crossed_over = crossover(Bestgenomes[0], Bestgenomes[2]);
            mutate(crossed_over[0]);
            mutate(crossed_over[1]);
            children.Add(crossed_over[0]);
            children.Add(crossed_over[1]);
            crossed_over = crossover(Bestgenomes[0], Bestgenomes[3]);
            mutate(crossed_over[0]);
            mutate(crossed_over[1]);
            children.Add(crossed_over[0]);
            children.Add(crossed_over[1]);
            crossed_over = crossover(Bestgenomes[1], Bestgenomes[2]);
            mutate(crossed_over[0]);
            mutate(crossed_over[1]);
            children.Add(crossed_over[0]);
            children.Add(crossed_over[1]);
            crossed_over = crossover(Bestgenomes[1], Bestgenomes[3]);
            mutate(crossed_over[0]);
            mutate(crossed_over[1]);
            children.Add(crossed_over[0]);
            children.Add(crossed_over[1]);
            // maintenant on va ajouter a la population restante des genomes randomly 
            int pop_restante = this.population_size - children.Count();
            for (int i = 0; i < pop_restante; i++)
            {
                children.Add(this.create_new_genome(Bestgenome.weights.Count()));
            }

            // maintenant qu'on a créer les enfants on va créer la nouvelles population (donc une nouvelle genenration ) 
            //ainsi on remplacer l'ancienne population par cette nouvelle (children ) et on clear l'ancienne avant et on ajoute une generation 
            clearpopulation();
            this.population = children;
            this.current_genome = -1;
            this.generation++;
                

        }
        private Genome create_new_genome (int totalweights)
        {
            Genome genome1 = new Genome();
            genome1.id = genomeid;
            genome1.fitness = 0;
            genome1.weights = new List<float>(totalweights);
            for (int j = 0; j < totalweights; j++)
            {
                // on va ajouter les poids dans la liste des poids du genome 
                genome1.weights.Add(MathHelper.Randomsigmoidvalue());
            }
            genomeid++;
            return genome1;
        }
       
        

    }
    // on doit ajouter une méthode maintenant dans le réseau de neurones qui permet de changer les poids du réseau a chaque generation 
     
}
