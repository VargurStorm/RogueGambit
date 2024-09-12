shader_type canvas_item;

uniform sampler2D piece_texture;

void vertex() {
    // Called for every vertex the material is visible on.
}

void fragment() {
    // Called for every pixel the material is visible on.
    vec4 piece_pixel = texture(piece_texture, UV);
    if (piece_pixel.a < 0.1) {
        discard;
    }
}
