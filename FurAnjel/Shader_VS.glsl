// Identify GL version
#version 430 core

// Vertex-Specific Inputs
// Input 0: Position Coordinate
layout (location = 0) in vec3 position;
// Input 1: Texture Coordinate
layout (location = 1) in vec2 texcoord;

// Shared Inputs
// Input 1: Projection Matrix (4x4). Translates world coordinates to screen coordinates.
layout (location = 1) uniform mat4 projection = mat4(1.0);
// Input 2: Model Matrix (4x4). Translates vertex coordinates to world coordinates.
layout (location = 2) uniform mat4 model = mat4(1.0);
// Input 3: Object Color Red/Green/Blue/Alpha (Alpha = Opacity)
layout (location = 3) uniform vec4 color = vec4(1.0);

// Output structure
out struct vs_out
{
	// Output 1: Color
	vec4 color;
	// Output 2: Texture Coordinate
	// Note that it is automatically smoothed from vertex coordinates among the screen space coordinates.
	vec2 texcoord;
} to_frag;

// The primary run method - the entry point of this vertex shader.
void main()
{
	// Set the fragment position to the transformation of the position by the two input matrices.
	// This translates vertex coordinates to world coordinates then to screen coordinates!
	gl_Position = projection * model * vec4(position, 1.0);
	// Set the output color.
	to_frag.color = color; // Optionally add a multiply by a per-vertex color, to improve dynamicness and allow neat effects!
	// Set the output texture coordinate.
	to_frag.texcoord = texcoord;
}
