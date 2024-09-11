using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

public class Point2D
{
    public double X;
    public double Y;
    public Point2D(double x, double y)
    {

        X = x;
        Y = y;
    }
}
public class Straight
{

    public Point2D Start;
    public double Direction;
    public Straight(Point2D startPoint, double direction)
    {

        Start = startPoint;
        Direction = direction;

    }

    public Point2D EvaluateAt(double length)
    {


        double X = Start.X + length * Math.Sin(Direction);
        double Y = Start.Y + length * Math.Cos(Direction);
        return new Point2D(X, Y);
    }
}
public class Circular
{
    public Point2D StartPoint;
    public double StartDirection;
    public double Radius;
    public Circular(Point2D point, double startDirection, double radius)
    {
        StartPoint = point;
        StartDirection = startDirection;
        Radius = radius;

    }
    public Point2D CalculateCenter()
    {

        double centerX = StartPoint.X + Radius * Math.Sin(Math.PI / 2 - StartDirection);
        double centerY = StartPoint.Y - Radius * Math.Cos(Math.PI / 2 - StartDirection);
        return new Point2D(centerX, centerY);
    }
    public Point2D EvaluateAt(double length)
    {


        Point2D center = CalculateCenter();
        double newX, newY;
        double newAngle = length / Math.Abs(Radius);
        if (Radius >= 0)
        {
            newX = center.X + Radius * Math.Sin(newAngle + StartDirection - Math.PI / 2);
            newY = center.Y + Radius * Math.Cos(newAngle + StartDirection - Math.PI / 2);
        }
        else
        {
            newX = center.X - Math.Abs(Radius) * Math.Sin(newAngle - StartDirection - Math.PI / 2);
            newY = center.Y + Math.Abs(Radius) * Math.Cos(newAngle - StartDirection - Math.PI / 2);

        }
        return new Point2D(newX, newY);
    }
}
public class Clothoid
{
    public Point2D StartPoint;
    public double StartDirection;
    public double StartCurvature;
    public double EndCurvature;
    public double Length;

    public Clothoid(Point2D startPoint, double startDirection, double startCurvature, double endCurvature, double length)
    {
        StartPoint = startPoint;
        StartDirection = startDirection;
        StartCurvature = startCurvature;
        EndCurvature = endCurvature;
        Length = length;
    }

    public Point2D EvaluateAt(double length)
    {
       
        
        double curvatureStep = (EndCurvature - StartCurvature) / Length;

        double currentX = StartPoint.X;
        double currentY = StartPoint.Y;
        double currentDirection = StartDirection;

        
        double stepSize = 0.1; 
        for (double s = 0; s < length; s += stepSize)
        {
            
            double currentCurvature = StartCurvature + curvatureStep * s;

            currentDirection += currentCurvature * stepSize;

            currentX += stepSize * Math.Cos(currentDirection);
            currentY += stepSize * Math.Sin(currentDirection);
        }

        return new Point2D(currentX, currentY);
    }
}




public class Program
{
    public static void Main()
    {
        var straightSegment = new Straight(new Point2D(4364223.753804, 5526665.362745), 0.6488475748464434);

        var newPointOnStraight = straightSegment.EvaluateAt(150.988734);
        //here we can add an  opportunity to input different values and in that case we would take values only if they are >=0 (length)
        Console.WriteLine("New point on straight is ( " + newPointOnStraight.X + "   ,   " + newPointOnStraight.Y + ")");

        var circularSegment = new Circular(new Point2D(4364344.213545, 5526822.467083), 0.7156848870803333, 351.6);
        var pointOnArc = circularSegment.EvaluateAt(409.716808);

        Console.WriteLine($"New point on circural arc is ( {pointOnArc.X}   ,   {pointOnArc.Y}) ");
        
        var clothoidSegment = new Clothoid(newPointOnStraight, 0.6488475748464434, 0, 0.002844141069397042, 470);
        double clothoidLength = 300.0;
        var pointOnClothoid = clothoidSegment.EvaluateAt(clothoidLength);

        Console.WriteLine($"New point on clothoid is ( {pointOnClothoid.X}   ,   {pointOnClothoid.Y}) ");
        Console.ReadKey();

    }

}



