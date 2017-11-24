using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Math;


public class Neural_network  {
    private int[] layer_tab { get; set; }
    private List<float> imput_layer { get; set; }
    private List<float[][]> weigts { get; set; }
    public void Neural_network(int[] a,List<float> b, List<float[][]> w )
    {
        
        for(int i = 0; i < a.Length; i++)
        {
            layer_tab[i] = a[i];
        }
        foreach(float s in b)
        {
            imput_layer.Add(s);
        }
        foreach(float[][] p in w)
        {
            weigts.Add(p);
        }
    }
    public List<float> create_layer(List<float>  previous_layer )
    {
        List<float> layer_i;
        float t;
        for(int i = 0; i<this.layer_tab[i]; i++)
        {
            t = scale(previous_layer, this.weigts, i);
            layer_i.Add(t);
        }
        return layer_i;
    }
    public float scale(List<float> a , List<float[][]> w , int j )
    {
        float ws=0;
        foreach(float[][] s in w)
        {
            for(int i=0; i < a.Count; i++)
            {
                ws = ws + s[i][j] * a[i];
            }
        }

        return ws; 
    }
    public List<float> get_output_layer()
    {
        List<float> previous_layer = this.imput_layer;
        List<float> output_layer;
        for (int i = 0; i < this.layer_tab.Length; i++)
        {
            output_layer = create_layer(previous_layer);
            previous_layer = output_layer;

        }
        return output_layer;
    }
    public List<float> get_layeri(int i)
    {
        List<float> previous_layer = this.imput_layer;
        List<float> output_layer;
        for (int j = 0; j<i; j++)
        {
            output_layer = create_layer(previous_layer);
            previous_layer = output_layer;

        }
        return output_layer;
    }
    public static double SigmoidFunction(double xValue)
    {
        if (xValue > 10) return 1.0;
        else if (xValue < -10) return 0.0;
        else return 1.0 / (1.0 + Math.Exp(-xValue));
    }

    /// <summary>
    /// The standard TanH function.
    /// </summary>
    /// <param name="xValue">The input value.</param>
    /// <returns>The calculated output.</returns>
    public static double TanHFunction(double xValue)
    {
        if (xValue > 10) return 1.0;
        else if (xValue < -10) return -1.0;
        else return Math.Tanh(xValue);
    }

    /// <summary>
    /// The SoftSign function as proposed by Xavier Glorot and Yoshua Bengio (2010): 
    /// "Understanding the difficulty of training deep feedforward neural networks".
    /// </summary>
    /// <param name="xValue">The input value.</param>
    /// <returns>The calculated output.</returns>
    public static double SoftSignFunction(double xValue)
    {
        return xValue / (1 + Math.Abs(xValue));
    }
}