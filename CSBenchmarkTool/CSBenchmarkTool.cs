using ColossalFramework;
using ColossalFramework.UI;
using ICities;
using System.ComponentModel;
using UnityEngine;
using System.Diagnostics;
using System;

namespace CSBenchmarkTool
{
    public class CSBenchmarkTool : IUserMod
    {
        public string Name
        {
            get { return "Benchmarking Tools"; }
        }

        public string Description
        {
            get { return "Test your TPS/FPS (made by gawrbage)"; }
        }

    }

    public class Loader : ILoadingExtension
    {
        public static BenchmarkPanel panelInstance;

        public static UILabel TickAverage;
        public static UILabel TickOnePercentLow;
        public static UILabel TickPointOnePercentLow;

        public static UILabel FrameAverage;
        public static UILabel FrameOnePercentLow;

        public static UILabel TickReadyStatus;
        public static UILabel FrameReadyStatus;
        public void OnLevelLoaded(LoadMode mode)
        {
            if (panelInstance != null) return;
            panelInstance = UIView.GetAView().AddUIComponent(typeof(BenchmarkPanel)) as BenchmarkPanel;

            //Title
            {
                UILabel lb = panelInstance.AddUIComponent<UILabel>();
                lb.text = "Benchmark Tool by gawrbage";
                lb.position = new Vector3(100f, -12f, 0);
            }

            //For TPS
            {
                UILabel lb = panelInstance.AddUIComponent<UILabel>();
                lb.text = "TPS";
                lb.position = new Vector3(50f, -50f, 0);
            }
            TickAverage = panelInstance.AddUIComponent<UILabel>();
            TickAverage.text = "Average";
            TickAverage.position = new Vector3(50f, -70f, 0);
            TickAverage.autoHeight = false;
            TickAverage.autoSize = false;
            TickAverage.size = new Vector2(200, 18);

            TickOnePercentLow = panelInstance.AddUIComponent<UILabel>();
            TickOnePercentLow.text = "1% Low";
            TickOnePercentLow.position = new Vector3(50f, -90f, 0);
            TickOnePercentLow.autoHeight = false;
            TickOnePercentLow.autoSize = false;
            TickOnePercentLow.size = new Vector2(200, 18);

            TickPointOnePercentLow = panelInstance.AddUIComponent<UILabel>();
            TickPointOnePercentLow.text = "0.1% Low";
            TickPointOnePercentLow.position = new Vector3(50f, -110f, 0);
            TickPointOnePercentLow.autoHeight = false;
            TickPointOnePercentLow.autoSize = false;
            TickPointOnePercentLow.size = new Vector2(200, 18);

            //For FPS
            {
                UILabel lb = panelInstance.AddUIComponent<UILabel>();
                lb.text = "FPS";
                lb.position = new Vector3(250f, -50f, 0);
            }

            FrameAverage = panelInstance.AddUIComponent<UILabel>();
            FrameAverage.text = "Frame Average";
            FrameAverage.position = new Vector3(250f, -70f, 0);
            FrameAverage.autoHeight = false;
            FrameAverage.autoSize = false;
            FrameAverage.size = new Vector2(200, 18);

            FrameOnePercentLow = panelInstance.AddUIComponent<UILabel>();
            FrameOnePercentLow.text = "Frame 1%";
            FrameOnePercentLow.position = new Vector3(250f, -90f, 0);
            FrameOnePercentLow.autoHeight = false;
            FrameOnePercentLow.autoSize = false;
            FrameOnePercentLow.size = new Vector3(200, 18);

            //Ready Statuses
            TickReadyStatus = panelInstance.AddUIComponent<UILabel>();
            TickReadyStatus.text = "Not Ready";
            TickReadyStatus.position = new Vector3(50f, -130f, 0);
            TickReadyStatus.autoHeight = false;
            TickReadyStatus.autoSize = false;
            TickReadyStatus.textColor = new Color32(255, 0, 0, 255);

            FrameReadyStatus = panelInstance.AddUIComponent<UILabel>();
            FrameReadyStatus.text = "Not Ready";
            FrameReadyStatus.position = new Vector3(250f, -130f, 0);
            FrameReadyStatus.autoHeight = false;
            FrameReadyStatus.autoSize = false;
            FrameReadyStatus.textColor = new Color32(255, 0, 0, 255);

            //Reference Labels
            {
                UILabel lb = panelInstance.AddUIComponent<UILabel>();
                lb.text = "avg";
                lb.position = new Vector3(10, -70f, 0);
            }

            {
                UILabel lb = panelInstance.AddUIComponent<UILabel>();
                lb.text = "1%";
                lb.position = new Vector3(10, -90f, 0);
            }

            {
                UILabel lb = panelInstance.AddUIComponent<UILabel>();
                lb.text = ".1%";
                lb.position = new Vector3(10, -110f, 0);
            }
        }

        public void OnCreated(ILoading loading)
        {
            
        }


        public void OnLevelUnloading()
        {
            
        }

        public void OnReleased()
        {
            
        }
    }
}