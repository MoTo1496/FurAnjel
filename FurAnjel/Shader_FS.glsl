// Identify GL version
#version 430 core

// The texture currently bound for usage.
layout (binding = 0) uniform sampler2D tex;

// We could put input uniform variables here if we needed any to be available to the fragment directly.

// Input structure, copied from VS
in struct vs_out
{
	// Input 1: Color
	vec4 color;
	// Input 2: Texture Coordinate
	// Note that it is automatically smoothed from vertex coordinates among the screen space coordinates.
	vec2 texcoord;
} to_frag;

// The output color. R/G/B/A
out vec4 color;

// The primary run method - the entry point of this fragment shader.
void main()
{
	// Output color is set to the texture at current (smoothed) texture coordinate position, multiplied by the fragment color.
	color = texture(tex, to_frag.texcoord) * to_frag.color;
}
