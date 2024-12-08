using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BrushMode {
    Add,
    Subtract
}

public class Brush1 : TerrainBrush {

    public float grow_speed = 0.1f;
    public float height = 2;
    public BrushMode mode = BrushMode.Add;

    public override void draw(int x, int z) {
        float mean = 0;
        for (int zi = -radius; zi <= radius; zi++) {
            for (int xi = -radius; xi <= radius; xi++) {
                mean += terrain.get(x + xi, z + zi);
            }
        }
        int n = radius * 2 + 1;
        mean /= n * n;

        if (mode == BrushMode.Add)
            mean = 0;

        for (int zi = -radius; zi <= radius; zi++) {
            for (int xi = -radius; xi <= radius; xi++) {
                float r = Mathf.Sqrt(xi * xi + zi * zi);
                float current_height = terrain.get(x + xi, z + zi);

                float freedom = 0;
                if (r <= radius)
                    freedom = Mathf.Sqrt(radius * radius - r * r);

                float base_height = mean - height * freedom;
                float lim_height = mean + height * freedom;
                float add = 0;
                float goal = 0;
                if (current_height < base_height)
                    goal = base_height;
                else if (current_height > lim_height)
                    goal = lim_height;
                else {
                    goal = mean;
                    if (mode == BrushMode.Add) 
                        goal += height * freedom;
                    else
                        goal -= height * freedom;
                }
                add = grow_speed * (goal - current_height);
                if (add < 0)
                    add /= 3;

                terrain.set(x + xi, z + zi, current_height + add);
            }
        }
    }
}
