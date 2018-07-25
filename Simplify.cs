using System;
using System.Collections.Generic;

namespace Simplify.NET
{
    public static class SimplifyNet
    {

        // square distance between 2 points
        private static double GetSqDist(Point p1, Point p2)
        {
            double dx = p1.X - p2.X;
            double dy = p1.Y - p2.Y;

            return (dx * dx) + (dy * dy);
        }

        // square distance from a point to a segment
        private static double GetSqSegDist(Point p, Point p1, Point p2)
        {
            double x = p1.X;
            double y = p1.Y;
            double dx = p2.X - x;
            double dy = p2.Y - y;

            if (Math.Abs(dx) > 0 || Math.Abs(dy) > 0)
            {
                double t = ((p.X - x) * dx + (p.Y - y) * dy) / (dx * dx + dy * dy);

                if (t > 1)
                {
                    x = p2.X;
                    y = p2.Y;
                }
                else if (t > 0)
                {
                    x += dx * t;
                    y += dy * t;
                }
            }

            dx = p.X - x;
            dy = p.Y - y;

            return dx * dx + dy * dy;
        }

        // basic distance-based simplification
        private static List<Point> SimplifyRadialDist(List<Point> points, double sqTolerance)
        {
            Point point = null;
            Point prevPoint = points[0];
            var newPoints = new List<Point>() { prevPoint };

            for (int i = 1, len = points.Count; i < len; i++)
            {
                point = points[i];

                if (GetSqDist(point, prevPoint) > sqTolerance)
                {
                    newPoints.Add(point);
                    prevPoint = point;
                }
            }

            if (prevPoint != point)
            {
                newPoints.Add(point);
            }

            return newPoints;
        }

        private static void SimplifyDpStep(List<Point> points, int first, int last, double sqTolerance, List<Point> simplified)
        {
            double maxSqDist = sqTolerance;
            var index = 0;

            for (int i = first + 1; i < last; i++)
            {
                double sqDist = GetSqSegDist(points[i], points[first], points[last]);

                if (sqDist > maxSqDist)
                {
                    index = i;
                    maxSqDist = sqDist;
                }
            }

            if (maxSqDist > sqTolerance)
            {
                if (index - first > 1)
                {
                    SimplifyDpStep(points, first, index, sqTolerance, simplified);

                }

                simplified.Add(points[index]);

                if (last - index > 1)
                {
                    SimplifyDpStep(points, index, last, sqTolerance, simplified);
                }
            }
        }

        // simplification using Ramer-Douglas-Peucker algorithm
        private static List<Point> SimplifyDouglasPeucker(List<Point> points, double sqTolerance)
        {
            int last = points.Count - 1;

            var simplified = new List<Point>() { points[0] };
            SimplifyDpStep(points, 0, last, sqTolerance, simplified);
            simplified.Add(points[last]);

            return simplified;
        }


        // both algorithms combined for awesome performance
        public static List<Point> Simplify(List<Point> points, double tolerance = 1, bool highestQuality = false)
        {
            if (points.Count <= 2)
            {
                return points;
            }

            double sqTolerance = tolerance * tolerance;

            points = highestQuality ? points : SimplifyRadialDist(points, sqTolerance);
            points = SimplifyDouglasPeucker(points, sqTolerance);

            return points;
        }
    }


    public class Point
    {
        public double X;
        public double Y;

        public Point(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
