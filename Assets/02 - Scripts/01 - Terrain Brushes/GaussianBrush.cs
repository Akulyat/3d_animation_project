using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaussianBrush : TerrainBrush {

    public float intensity = 1f;
    public float sigma = 1f;            

    public override void draw(int x, int z) {
        for (int zi = -radius; zi <= radius; zi++) {
            for (int xi = -radius; xi <= radius; xi++) {
                float distance = Mathf.Sqrt(xi * xi + zi * zi);
                float factor = Mathf.Exp(-(distance * distance) / (2 * sigma * sigma));
                float heightChange = intensity * factor;
                terrain.set(x + xi, z + zi, terrain.get(x + xi, z + zi) + heightChange);
            }
        }
    }
}
