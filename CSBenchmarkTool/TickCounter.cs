using ICities;
using System;
using System.Diagnostics;
using ColossalFramework;

namespace CSBenchmarkTool
{
    public class TickCounter : IThreadingExtension
    {
        private static Stopwatch stopwatch;

        private const uint MAX_REMEMBERED_TICKS = 2000;

        private static uint counter = 0;
        private static double[] msPerTick = new double[MAX_REMEMBERED_TICKS];
        private static double[] msPerTickSorted;


        public void OnAfterSimulationFrame()
        {
            //Calculate Average MS
            stopwatch.Stop();

            double currentms = (double)stopwatch.ElapsedTicks / (double)Stopwatch.Frequency * 1000d;

            msPerTick[counter] = currentms;

            counter++;
            if (counter >= MAX_REMEMBERED_TICKS) { counter = 0; Loader.TickReadyStatus.text = "Ready"; Loader.TickReadyStatus.textColor = new UnityEngine.Color32(0, 255, 0, 255); }

            double averagems = 0d;
            for (uint i = 0; i < MAX_REMEMBERED_TICKS; i++)
            {
                averagems += msPerTick[i];
            }
            averagems = averagems / MAX_REMEMBERED_TICKS;

            //Calculate One Percent Low
            msPerTickSorted = (double[])msPerTick.Clone();
            Array.Sort(msPerTickSorted);

            double OnePercentLow = 0;
            for (uint i = MAX_REMEMBERED_TICKS / 100u * 99u; i < MAX_REMEMBERED_TICKS; i++)
            {
                OnePercentLow += msPerTickSorted[i];
            }
            OnePercentLow = OnePercentLow / (MAX_REMEMBERED_TICKS - MAX_REMEMBERED_TICKS / 100u * 99u);

            //Calculate Point One Percent Low
            double PointOnePercentLow = 0;
            for (uint i = MAX_REMEMBERED_TICKS / 1000u * 999u; i < MAX_REMEMBERED_TICKS; i++)
            {
                PointOnePercentLow += msPerTickSorted[i];
            }
            PointOnePercentLow = PointOnePercentLow / (MAX_REMEMBERED_TICKS - MAX_REMEMBERED_TICKS / 1000u * 999u);

            Loader.TickAverage.text = averagems.ToString("F2") + "ms - " + (1d / (averagems / 1000d)).ToString("F2") + " TPS";
            Loader.TickOnePercentLow.text = OnePercentLow.ToString("F2") + "ms - " + (1d / (OnePercentLow / 1000d)).ToString("F2") + " TPS";
            Loader.TickPointOnePercentLow.text = PointOnePercentLow.ToString("F2") + "ms - " + (1d / (PointOnePercentLow / 1000d)).ToString("F2") + " TPS";
        }

        public void OnAfterSimulationTick()
        {
            
        }

        public void OnBeforeSimulationFrame()
        {
            stopwatch = Stopwatch.StartNew();
        }

        public void OnBeforeSimulationTick()
        {

        }

        public void OnCreated(IThreading threading)
        {
            for (uint i = 0; i < MAX_REMEMBERED_TICKS; i++) { msPerTick[i] = 0; }
        }

        public void OnReleased()
        {

        }

        public void OnUpdate(float realTimeDelta, float simulationTimeDelta)
        {
            
        }

        public static void Reset()
        {
            counter = 0;
            for (uint i = 0; i < MAX_REMEMBERED_TICKS; i++) { msPerTick[i] = 0; }
        }
    }
}
