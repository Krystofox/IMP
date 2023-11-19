namespace Game.Map;

class Map
{
    //REWRITE TO NEW RESOURCE SYSTEM
    
    /*
    public static Map map;
        public Physics physics = new Physics();
        public Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        public Block[] blocks;

        public Map()
        {
            map = this;
            physics.Initialize();
            new GameGraphics();
        }

        public void Unload()
        {
            foreach (var texture in textures)
                UnloadTexture(texture.Value);
            foreach (var block in blocks)
                block.Unload();
            physics.Dispose();
            GC.Collect();
        }
        unsafe public void DrawBlocks()
        {
            for (int i = 0; i < blocks.Length; i++)
            {
                // DRAW CUBE WIRES + ROTATION ADD TO BLOCK
                blocks[i].Model.materials[0].shader = GameGraphics.instance.lightingShader;
                DrawModel(blocks[i].Model, blocks[i].Position, 1, Color.WHITE);
                blocks[i].DrawWireframe(1, 1, Color.BLUE);
                //var physObj = physics.simulation.Statics[blocks[i].Collider];
                //DrawCubeWiresV(physObj.Pose.Position,physObj.BoundingBox.Max,Color.RED);
            }
        }
        unsafe public void Draw()
        {
            SetShaderValue(GameGraphics.instance.lightingShader, GameGraphics.instance.lightingShader.locs[(int)SHADER_LOC_VECTOR_VIEW], PlayerController.instance.playerLocation + PlayerController.instance.cameraOffset, ShaderUniformDataType.SHADER_UNIFORM_VEC3);
            physics.SimulationStep();
            DrawBlocks();
        }
    */
}