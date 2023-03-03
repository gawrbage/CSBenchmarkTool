using ColossalFramework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CSBenchmarkTool
{
    public class BenchmarkPanel : UIPanel
    {
        public override void Start()
        {
            base.Start();
            backgroundSprite = "MenuPanel";
            isVisible = true;
            canFocus = true;
            isInteractive = true;
            width = 500;
            height = 200;

            GameObject dragHandleObject = new GameObject("DragHandler");
            dragHandleObject.transform.parent = this.transform;
            dragHandleObject.transform.localPosition = Vector3.zero;
            UIDragHandle dragHandle = dragHandleObject.AddComponent<UIDragHandle>();
            dragHandle.width = this.width;
            dragHandle.height = 40f;
            dragHandle.zOrder = 0;
            dragHandle.BringToFront();

            GameObject closeButtonObject = new GameObject("CloseButton");
            closeButtonObject.transform.parent = this.transform;
            closeButtonObject.transform.localPosition = Vector3.zero;
            UIButton closeButton = closeButtonObject.AddComponent<UIButton>();
            closeButton.width = 32f;
            closeButton.height = 32f;
            closeButton.normalBgSprite = "buttonclose";
            closeButton.hoveredBgSprite = "buttonclosehover";
            closeButton.pressedBgSprite = "buttonclosepressed";
            closeButton.relativePosition = new Vector3(this.width - closeButton.width, 2f);
            closeButton.eventClick += (UIComponent component, UIMouseEventParameter eventParam) =>
            {
                this.isVisible = false;
            };

            GameObject resetButtonGO = new GameObject("ResetButton");
            resetButtonGO.transform.parent = this.transform;
            resetButtonGO.transform.localPosition = Vector3.zero;
            UIButton resetButton = resetButtonGO.AddComponent<UIButton>();
            resetButton.width = 64f;
            resetButton.height = 32f;
            resetButton.text = "Reset";
            resetButton.position = new Vector3(40f,-150f,0);
            resetButton.textColor = new Color32(0, 255, 255, 255);
            resetButton.hoveredTextColor = new Color32(0, 0, 255, 255);
            resetButton.pressedTextColor = new Color32(0, 255, 255, 255);
            resetButton.focusedTextColor = new Color32(0, 255, 255, 255);
            resetButton.eventClick += (UIComponent component, UIMouseEventParameter eventParam) =>
            {
                Loader.FrameReadyStatus.text = "Not Ready";
                Loader.TickReadyStatus.text = "Not Ready";
                Loader.FrameReadyStatus.textColor = new Color32(255, 0, 0, 255);
                Loader.TickReadyStatus.textColor = new Color32(255, 0, 0, 255);
                TickCounter.Reset();
                FrameCounter.Reset();
            };
        }

        public override void Update()
        {
            if (Input.GetKey(KeyCode.P) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))) { this.isVisible = true; }
            base.Update();
        }
    }
}
