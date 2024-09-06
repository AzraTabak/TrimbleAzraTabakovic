using System;

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

        double centerX = StartPoint.X - Math.Abs(Radius) * Math.Sin(StartDirection);//because of normal direction=startdiection+pi/2, we can leave also as + Cos(startdirection+pi/2))
        double centerY = StartPoint.Y + Math.Abs(Radius) * Math.Cos(StartDirection);
        return new Point2D(centerX, centerY);
    }
    public Point2D EvaluateAt(double length)
    {


        Point2D center = CalculateCenter();
        double newAngle = (length / Math.Abs(Radius)) * Math.Sign(Radius);
        double newX = center.X + Math.Abs(Radius) * Math.Cos(StartDirection + newAngle);
        double newY = center.Y + Math.Abs(Radius) * Math.Sin(StartDirection + newAngle);

        return new Point2D(newX, newY);
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

        Console.WriteLine($"New point on circural arc is ( {pointOnArc.X}   ,   {pointOnArc.Y})");
        Console.ReadKey();

    }

}



