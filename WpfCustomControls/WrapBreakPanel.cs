using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace WpfCustomControls
{
    public class WrapBreakPanel : Panel
    {
        public static readonly DependencyProperty LineBreakBeforeProperty;

        static WrapBreakPanel()
        {
            FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata
            {
                AffectsMeasure = true,
                AffectsArrange = true,
                DefaultValue = false
            };
            LineBreakBeforeProperty =
            DependencyProperty.RegisterAttached("LineBreakBefore", typeof(bool), typeof(WrapBreakPanel), metadata);
        }

        public static bool GetLineBreakBefore(DependencyObject obj)
        {
            return (bool)obj.GetValue(LineBreakBeforeProperty);
        }

        public static void SetLineBreakBefore(DependencyObject obj, bool value)
        {
            obj.SetValue(LineBreakBeforeProperty, value);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            Size currentLineSize = new Size();
            Size panelSize = new Size();

            foreach (UIElement element in base.InternalChildren)
            {
                element.Measure(availableSize);
                Size desiredSize = element.DesiredSize;

                if (GetLineBreakBefore(element) ||
                    currentLineSize.Width + desiredSize.Width > availableSize.Width)
                {
                    panelSize.Width = Math.Max(currentLineSize.Width, panelSize.Width);
                    panelSize.Height += currentLineSize.Height;
                    currentLineSize = desiredSize;

                    if (desiredSize.Width > availableSize.Width)
                    {
                        panelSize.Width = Math.Max(desiredSize.Width, panelSize.Width);
                        panelSize.Height += desiredSize.Height;
                        currentLineSize = new Size();
                    }
                }
                else
                {
                    currentLineSize.Width += desiredSize.Width;
                    currentLineSize.Height = Math.Max(desiredSize.Height, currentLineSize.Height);
                }
            }
            panelSize.Width = Math.Max(currentLineSize.Width, panelSize.Width);
            panelSize.Height += currentLineSize.Height;
            return panelSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Size currentLineSize = new Size();
            Size panelSize = new Size();

            var lstCurrentLineElements = new List<UIElement>();
            double top = 0;
            double left = 0;

            foreach (UIElement element in base.InternalChildren)
            {
                Size desiredSize = element.DesiredSize;

                if (GetLineBreakBefore(element) ||
                    currentLineSize.Width + desiredSize.Width > finalSize.Width)
                {
                    //currentLineSize.Width = lstCurrentLineElements.Sum(x => x.DesiredSize.Width);
                    //currentLineSize.Height= lstCurrentLineElements.Max(x=>x.DesiredSize.Height);
                    panelSize.Width = Math.Max(currentLineSize.Width, panelSize.Width);
                    left = 0;
                    foreach (var item in lstCurrentLineElements)
                    {
                        item.Arrange(new Rect(left, top, item.DesiredSize.Width, currentLineSize.Height));
                        left += item.DesiredSize.Width;
                    }

                    top += currentLineSize.Height;
                    lstCurrentLineElements.Clear();

                    lstCurrentLineElements.Add(element);
                    currentLineSize = element.DesiredSize;
                }
                else
                {
                    currentLineSize.Width += desiredSize.Width;
                    currentLineSize.Height = Math.Max(desiredSize.Height, currentLineSize.Height);
                    lstCurrentLineElements.Add(element);
                }
            }

            if (lstCurrentLineElements.Count > 0)
            {
                left = 0;
                foreach (var item in lstCurrentLineElements)
                {
                    item.Arrange(new Rect(left, top, item.DesiredSize.Width, currentLineSize.Height));
                    left += item.DesiredSize.Width;
                }

                top += currentLineSize.Height;
            }

            panelSize.Height = top;
            return panelSize;
        }
    }
}