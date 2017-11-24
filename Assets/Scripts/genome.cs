using System;
using System.Collections;
using System.Collections.Generic;


public class Genome {
    private int id { get; set; }
    private double fitness { get; set; }
    private List<float> weights { get; set; }

    //constructeurs 
    public Genome(int i, double fit, List<float> w)
    {
        this.weights = new List<float>(w.Count);
        this.id = i;
        this.fitness = fit;
        foreach (float s in w)
        {
            this.weights.Add(s);
        }


    }
    public List<float[,]> transform_weights()
    {
        List<float> a = this.weights;

    }
        
        
      


}
