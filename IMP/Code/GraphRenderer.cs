using Raylib_cs;
using static Raylib_cs.Raylib;
using static System.Convert;

namespace Game;
class GraphRenderer
{
    // Rewrite to use less cpu
    double[] graphData;
    int selected;
    public GraphRenderer(int graphLenght)
    {
        graphData = new double[graphLenght];
        selected = graphLenght-1;
    }

    public void UpdateValue(double value)
    {
        selected++;
        if(selected >= graphData.Length)
            selected = 0;
        graphData[selected] = Math.Clamp(value,0,1);
    }

    int times = 0;
    double avgValue = 0;
    public void AddValueAvg(double value)
    {
        avgValue += value;
        times++;
    }

    public void UpdateValueAvg()
    {
        UpdateValue(avgValue/times);
        times = 0;
        avgValue = 0;
    }

    public ref double GetItemArrayShuffled(int i)
    {
        i += selected;
        if(i >= graphData.Length)
            i -= graphData.Length;
        return ref graphData[i];
    }

    public void Draw(int posX,int posY,int width,int height,Color color)
    {
        //Rewrite to single draw call
        //Make smooth line animation
        int rGraphLenght = graphData.Length-1;
        double chunkLenX = (double)width/rGraphLenght;
        double lastVal = GetItemArrayShuffled(1);
        for (int i = 0; i < rGraphLenght; i++)
        {
            DrawLine(ToInt32(posX+chunkLenX*i),ToInt32(posY+(1-lastVal)*height),ToInt32(posX+chunkLenX*(i+1)),posY+ToInt32((1-GetItemArrayShuffled(i+1))*height),color);
            lastVal = GetItemArrayShuffled(i+1);
        }
        
    }


}

