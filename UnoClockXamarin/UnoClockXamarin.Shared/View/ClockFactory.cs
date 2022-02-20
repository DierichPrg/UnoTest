using System;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace UnoClockXamarin.View
{
    // internal class because only this project need access
    internal class ClockFactory
    {
        private readonly Color clockBorderColor = Colors.Aqua;
        private readonly Color clockEllipsesColor = Colors.Aqua;
        private readonly Color hourPointerColor = Colors.Blue;
        private readonly Color minutePointerColor = Colors.Red;
        private readonly Color secondPointerColor = Colors.Silver;

        private readonly double clockHeight;
        private readonly double clockRadius;
        private readonly double hourPointerThinkness;
        private readonly double minPointerThinkness;
        private readonly double secPointerThinkness;
        private readonly double ellipseHourCircleThinkness;
        private readonly double ellipseClockBorderThinkness;
        private readonly double hourLength;
        private readonly double minLength;
        private readonly double secLength;

        private readonly double centerX;
        private readonly double centerY;

        public ClockFactory(
            double clockHeight,
            double hourPointerThinkness,
            double minPointerThinkness,
            double secPointerThinkness,
            double ellipseHourCircleThinkness,
            double ellipseClockBorderThinkness)
        {
            // set all params to fields
            this.clockHeight = clockHeight;
            this.hourPointerThinkness = hourPointerThinkness;
            this.minPointerThinkness = minPointerThinkness;
            this.secPointerThinkness = secPointerThinkness;
            this.ellipseHourCircleThinkness = ellipseHourCircleThinkness;
            this.ellipseClockBorderThinkness = ellipseClockBorderThinkness;

            // do some math and set to fields
            this.clockRadius = clockHeight * 0.6d;
            this.hourLength = clockHeight / 3.5d;
            this.minLength = clockHeight / 3.5d;
            this.secLength = clockHeight / 2.5d;
            this.centerX = clockHeight / 2;
            this.centerY = clockRadius;
        }

        // the only public method that return all UIElements necessary to draw the clock
        public IEnumerable<UIElement> GetClock(DateTime date)
        {
            // based on liskov principle, lets start a UIElement list
            List<UIElement> lst = new List<UIElement>();

            // add all defaults itens on list
            lst.Add(this.GetClockBorderEllipse());
            lst.AddRange(this.GetClockEllipses());

            // add text of hour on list
            lst.Add(this.GetTimeInText(date));

            // calc all radians necessary to use sin e cos
            double hourRadian = (date.Hour % 12) * 360d / 12d * System.Math.PI / 180d;
            double minRadian = date.Minute * 360d / 60d * System.Math.PI / 180d;
            double secRadian = date.Second * 360d / 60d * System.Math.PI / 180d;

            // calc position of hour pointer
            double hourEndPointX = hourLength * (double)System.Math.Sin(hourRadian);
            double hourEndPointY = hourLength * (double)System.Math.Cos(hourRadian);
            lst.Add(
                this.GetLinePointer(
                    this.centerX,
                    this.centerY,
                    this.centerX + hourEndPointX,
                    centerY - hourEndPointY,
                    this.hourPointerColor,
                    this.hourPointerThinkness));

            // calc position of minute pointer
            double minEndPointX = minLength * (double)System.Math.Sin(minRadian);
            double minEndPointY = minLength * (double)System.Math.Cos(minRadian);
            lst.Add(
                this.GetLinePointer(
                    centerX,
                    centerY,
                    centerX + minEndPointX,
                    centerY - minEndPointY,
                    this.minutePointerColor,
                    this.minPointerThinkness));

            // calc position of Second pointer
            double secEndPointX = secLength * (double)System.Math.Sin(secRadian);
            double secEndPointY = secLength * (double)System.Math.Cos(secRadian);
            lst.Add(
                this.GetLinePointer(
                    centerX,
                    centerY,
                    centerX + secEndPointX,
                    centerY - secEndPointY,
                    this.secondPointerColor,
                    this.secPointerThinkness));

            return lst;
        }

        // the circle of clock view
        private UIElement GetClockBorderEllipse()
        {
            return new Ellipse()
            {
                Stroke = new SolidColorBrush(this.clockBorderColor),
                StrokeThickness = this.ellipseClockBorderThinkness,
                Width = this.clockHeight,
                Height = this.clockHeight,
                Margin = new Thickness(
                    (this.clockHeight - this.ellipseClockBorderThinkness) / System.Math.Pow(System.Math.PI, 6),
                    (this.clockHeight - this.ellipseClockBorderThinkness) / System.Math.Pow(System.Math.PI, 2),
                    0,
                    0)
            };
        }

        // all elipses that represents hour on clock view
        private IEnumerable<UIElement> GetClockEllipses()
        {
            List<UIElement> lst = new List<UIElement>();

            for (int i = 0; i < 60; i += 5)
            {
                double x1 = centerX + (double) (clockRadius / 1.50F * System.Math.Sin(i * 6 * System.Math.PI / 180));
                double y1 = centerY - (double) (clockRadius / 1.50F * System.Math.Cos(i * 6 * System.Math.PI / 180));
                double x2 = centerX + (double) (clockRadius / 1.65F * System.Math.Sin(i * 6 * System.Math.PI / 180));
                double y2 = centerY - (double) (clockRadius / 1.65F * System.Math.Cos(i * 6 * System.Math.PI / 180));

                lst.Add(new Ellipse()
                {
                    Stroke = new SolidColorBrush(this.clockEllipsesColor),
                    StrokeThickness = this.ellipseHourCircleThinkness,
                    Margin = new Thickness(
                        x1 - this.ellipseHourCircleThinkness / 2,
                        y1 - this.ellipseHourCircleThinkness / 2,
                        x2 - this.ellipseHourCircleThinkness / 2,
                        y2 - this.ellipseHourCircleThinkness / 2)
                });
            }

            return lst;
        }

        // text of time
        private UIElement GetTimeInText(DateTime date)
        {
            return new TextBlock()
            {
                Text = date.ToString("HH:mm:ss"),
                FontSize = this.clockRadius / System.Math.PI * 1.15d,
                Margin = new Thickness(
                    this.clockHeight / System.Math.Pow(System.Math.PI, 2),
                    this.clockHeight / System.Math.Pow(System.Math.PI, 0.9d),
                    0,
                    0),
                CharacterSpacing = 0
            };
        }

        // method that create the line UIElement of hour, minute and second pointers
        private UIElement GetLinePointer(double x1, double y1, double x2, double y2, Color color, double thinkness)
        {
            return new Line()
            {
                X1 = x1,
                Y1 = y1,
                X2 = x2,
                Y2 = y2,
                StrokeThickness = thinkness,
                Stroke = new SolidColorBrush(color)
            };
        }
    }
}
