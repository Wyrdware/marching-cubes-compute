# Marching Cubes Compute Shader

This project is a exploration of generating real-time 3D meshes in Unity using a compute shader implementation of the Marching Cubes algorithm. The goal was to experiment with GPU-based mesh generation, chunking, and noise-driven scalar fields.

## Overview

- Implements Marching Cubes in a compute shader
- Uses FastNoiseLite to populate the scalar field
- Meshes update in real time based on noise input
- Chunked to stay under Unity’s 65,000 vertex limit
- Built to explore workflows for scalable, procedural surfaces

Chunks are sized at 16×16×16 scalar values (15×15×15 cubes). At a max of 15 vertices per cube, this keeps each chunk just below Unity’s vertex cap. A basic chunk manager was added to test the system at larger scales.

## Future work

- Add LOD and streaming for generating endless environments 
- Build tools for editing scalar fields in-editor for design-focused workflows
- Organize the project into a user-friendly package for easier integration 

## Use Cases

This kind of system could support:
- Water or fluid surfaces
- Volumetric terrain
- Destructible environments
- Flow-field-based visualizations

## References

- [Marching Cubes (Paul Bourke)](https://paulbourke.net/geometry/polygonise/)  
- [Polycoding – Marching Cubes Tutorial](https://polycoding.net/marching-cubes/part-1/)  
- [Polycoding – Compute Shaders in Unity](https://polycoding.net/compute-shaders-unity)  
- [FastNoiseLite GitHub](https://github.com/Auburn/FastNoiseLite)  
- [GameDevGuide – Marching Cubes](https://www.youtube.com/watch?v=BrZ4pWwkpto)  
- [Sebastian Lague – Procedural Generation](https://www.youtube.com/watch?v=M3iI2l0ltbE)


