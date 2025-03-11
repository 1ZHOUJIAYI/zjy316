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

// 正方形类
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

// 三角形类
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
        // 使用海伦公式计算三角形面积
        double s = (SideA + SideB + SideC) / 2;
        return Math.Sqrt(s * (s - SideA) * (s - SideB) * (s - SideC));
    }

    public bool IsValid()
    {
        // 判断三角形是否合法
        return SideA + SideB > SideC && SideA + SideC > SideB && SideB + SideC > SideA;
    }
}

// 简单工厂类
public static class ShapeFactory
{
    private static Random random = new Random();

    public static IShape CreateShape()
    {
        int shapeType = random.Next(0, 3); // 随机生成0, 1, 2

        switch (shapeType)
        {
            case 0:
                return new Rectangle(random.Next(1, 10), random.Next(1, 10));
            case 1:
                return new Square(random.Next(1, 10));
            case 2:
                return new Triangle(random.Next(1, 10), random.Next(1, 10), random.Next(1, 10));
            default:
                throw new InvalidOperationException("Invalid shape type");
        }
    }
}

// 主程序
class Program
{
    static void Main(string[] args) // 确保 Main 方法签名正确
    {
        double totalArea = 0;
        for (int i = 0; i < 10; i++)
        {
            IShape shape = ShapeFactory.CreateShape();
            if (shape.IsValid())
            {
                double area = shape.CalculateArea();
                totalArea += area;
                Console.WriteLine($"Created {shape.GetType().Name} with area: {area}");
            }
            else
            {
                Console.WriteLine($"Created invalid {shape.GetType().Name}");
            }
        }

        Console.WriteLine($"Total area of all shapes: {totalArea}");
    }
}