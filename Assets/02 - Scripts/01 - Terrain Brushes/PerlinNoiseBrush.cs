using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoiseBrush : TerrainBrush {

    public float heightScale = 10f;
    public float noiseScale = 0.1f;

    public override void draw(int x, int z) {
        for (int zi = -radius; zi <= radius; zi++) {
            for (int xi = -radius; xi <= radius; xi++) {
                float noiseValue = Mathf.PerlinNoise(
                    (x + xi) * noiseScale, 
                    (z + zi) * noiseScale
                );
                float height = noiseValue * heightScale;
                terrain.set(x + xi, z + zi, height);
            }
        }
    }
}
