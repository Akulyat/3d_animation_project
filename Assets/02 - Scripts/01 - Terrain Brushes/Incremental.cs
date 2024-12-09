using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Incremental : TerrainBrush 
{
    public float height = 5f;
    public float incrementAmount = 0.1f;
    
    public override void draw(int x, int z) 
    {
        for (int zi = -radius; zi <= radius; zi++) 
        {
            for (int xi = -radius; xi <= radius; xi++) 
            {
                float currentHeight = terrain.get(x + xi, z + zi);
                
                float distance = Mathf.Sqrt(xi * xi + zi * zi);
                
                if (distance <= radius)
                {
                    float falloff = 1f - (distance / radius);
                    
                    if (Input.GetMouseButton(0)) 
                    {
                        currentHeight += incrementAmount * falloff;
                    }
                    else if (Input.GetMouseButton(1))
                    {
                        currentHeight -= incrementAmount * falloff;
                    }
                    terrain.set(x + xi, z + zi, currentHeight);
                }
            }
        }
    }
}