﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfDraw
{
    public class DrawingCanvas : Canvas
    {
        private List<Visual> _visuals = new List<Visual>();

        protected override int VisualChildrenCount => _visuals.Count;

        protected override Visual GetVisualChild(int index) => _visuals[index];

        public void AddVisual(Visual visual)
        {
            _visuals.Add(visual);
            base.AddVisualChild(visual);
            base.AddLogicalChild(visual);
        }

        public void DeleteVisual(Visual visual)
        {
            _visuals.Remove(visual);
            base.RemoveVisualChild(visual);
            base.RemoveLogicalChild(visual);
        }

        public DrawingVisual GetVisual(Point point)
        {
            HitTestResult hitResult = VisualTreeHelper.HitTest(this, point);
            return hitResult.VisualHit as DrawingVisual;
        }

        private List<DrawingVisual> hits = new List<DrawingVisual>();

        public List<DrawingVisual> GetVisuals(Geometry region)
        {
            hits.Clear();

            GeometryHitTestParameters parameters = new GeometryHitTestParameters(region);

            HitTestResultCallback callback = new HitTestResultCallback(this.HitTestCallBack);

            VisualTreeHelper.HitTest(this, null, callback, parameters);

            return hits;
        }

        private HitTestResultBehavior HitTestCallBack(HitTestResult result)
        {
            GeometryHitTestResult geometryResult = (GeometryHitTestResult)result;
            DrawingVisual visual = result.VisualHit as DrawingVisual;

            if (visual != null &&
                geometryResult.IntersectionDetail == IntersectionDetail.FullyInside)
            {
                hits.Add(visual);
            }
            return HitTestResultBehavior.Continue;
        }
    }
}