using System;

// 定义形状接口
public interface IShape
{
    double CalculateArea();
    bool IsValid();         
}

// 长方形类
public class Rectangle : IShape
{
    public double Width { get; set; }
    public double Height { get; set; }

    public Rectangle(double width, double height)
    {
        Width = width;
        Height = height;
    }

    public double CalculateArea()
    {
        return Width * Height;
    }

    public bool IsValid()
    {
        return Width > 0 && Height > 0;
    }
}

public class Square : IShape
{
    public double Side { get; set; }

    public Square(double side)
    {
        Side = side;
    }

    public double CalculateArea()
    {
        return Side * Side;
    }

    public bool IsValid()
    {
        return Side > 0;
    }
}

public class Triangle : IShape
{
    public double SideA { get; set; }
    public double SideB { get; set; }
    public double SideC { get; set; }


    public Triangle(double sideA, double sideB, double sideC)
    {
        SideA = sideA;
        SideB = sideB;
        SideC = sideC;
    }
    public double CalculateArea()
    {
        double s = (SideA + SideB + SideC) / 2; 
        return Math.Sqrt(s * (s - SideA) * (s - SideB) * (s - SideC));
    }

    public bool IsValid()
    {
        return SideA + SideB > SideC && SideA + SideC > SideB && SideB + SideC > SideA;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // 创建长方形对象
        Rectangle rectangle = new Rectangle(5, 10);
        Console.WriteLine($"Rectangle Area: {rectangle.CalculateArea()}, Valid: {rectangle.IsValid()}");

        // 创建正方形对象
        Square square = new Square(7);
        Console.WriteLine($"Square Area: {square.CalculateArea()}, Valid: {square.IsValid()}");

        // 创建三角形对象
        Triangle triangle = new Triangle(3, 4, 5);
        Console.WriteLine($"Triangle Area: {triangle.CalculateArea()}, Valid: {triangle.IsValid()}");

        Triangle invalidTriangle = new Triangle(1, 2, 5);
        Console.WriteLine($"Invalid Triangle Area: {invalidTriangle.CalculateArea()}, Valid: {invalidTriangle.IsValid()}");
    }
}