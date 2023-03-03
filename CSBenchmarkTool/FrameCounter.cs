using ICities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSBenchmarkTool
{
    public class FrameCounter : IThreadingExtension
    {
        private const uint MAX_REMEBERED_FRAMES = 500;

        private static uint counter = 0;

        private static float[] msPerFrame = new float[MAX_REMEBERED_FRAMES];
        private static float[] msPerFrameSorted;

        public void OnAfterSimulationFrame()
        {
            //throw new NotImplementedException();
        }

        public void OnAfterSimulationTick()
        {
            //throw new NotImplementedException();
        }

        public void OnBeforeSimulationFrame()
        {
            //throw new NotImplementedException();
        }

        public void OnBeforeSimulationTick()
        {
            //throw new NotImplementedException();
        }

        public void OnCreated(IThreading threading)
        {
            for(int i = 0; i < MAX_REMEBERED_FRAMES; i++) { msPerFrame[i] = 0; }
            counter = 0;
        }

        public void OnReleased()
        {
            //throw new NotImplementedException();
        }

        public void OnUpdate(float realTimeDelta, float simulationTimeDelta)
        {
            //Average
            msPerFrame[counter] = realTimeDelta;
            counter++;
            if (counter >= MAX_REMEBERED_FRAMES) { counter = 0; Loader.FrameReadyStatus.text = "Ready"; Loader.FrameReadyStatus.textColor = new UnityEngine.Color32(0, 255, 0, 255); }

            float averagemsPerFrame = 0f;
            for (uint i = 0; i < MAX_REMEBERED_FRAMES; i++)
            {
                averagemsPerFrame += msPerFrame[i];
            }
            averagemsPerFrame /= MAX_REMEBERED_FRAMES;
            //1% low
            msPerFrameSorted = (float[])msPerFrame.Clone();
            Array.Sort(msPerFrameSorted);

            double OnePercentLow = 0;
            for (uint i = MAX_REMEBERED_FRAMES / 100u * 99u; i < MAX_REMEBERED_FRAMES; i++)
            {
                OnePercentLow += msPerFrameSorted[i];
            }
            OnePercentLow /= (MAX_REMEBERED_FRAMES - MAX_REMEBERED_FRAMES / 100u * 99u);

            Loader.FrameAverage.text = (1f / averagemsPerFrame).ToString("F2") + " FPS - " + (averagemsPerFrame * 1000f).ToString("F2") + " ms";
            Loader.FrameOnePercentLow.text = (1f / OnePercentLow).ToString("F2") + " FPS - " + (OnePercentLow * 1000f).ToString("F2") + " ms";
        }

        public static void Reset()
        {
            counter = 0;
            for (int i = 0; i < MAX_REMEBERED_FRAMES; i++) { msPerFrame[i] = 0; }
        }
    }
}
