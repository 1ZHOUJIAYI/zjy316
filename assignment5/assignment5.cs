using System;
using System.Collections.Generic;
using System.Linq;
public class Product
{
    public string ProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public Product(string productId, string name, decimal price)
    {
        ProductId = productId;
        Name = name;
        Price = price;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Product other = (Product)obj;
        return ProductId == other.ProductId;
    }

    public override int GetHashCode()
    {
        return ProductId.GetHashCode();
    }

    public override string ToString()
    {
        return $"商品ID: {ProductId}, 名称: {Name}, 单价: {Price:C}";
    }
}


public class Customer
{
    public string CustomerId { get; set; }
    public string Name { get; set; }

    public Customer(string customerId, string name)
    {
        CustomerId = customerId;
        Name = name;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Customer other = (Customer)obj;
        return CustomerId == other.CustomerId;
    }

    public override int GetHashCode()
    {
        return CustomerId.GetHashCode();
    }

    public override string ToString()
    {
        return $"客户ID: {CustomerId}, 姓名: {Name}";
    }
}

public class OrderDetail
{
    public Product Product { get; set; }
    public int Quantity { get; set; }

    public OrderDetail(Product product, int quantity)
    {
        Product = product;
        Quantity = quantity;
    }

    public decimal Amount => Product.Price * Quantity;

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        OrderDetail other = (OrderDetail)obj;
        return Product.Equals(other.Product);
    }

    public override int GetHashCode()
    {
        return Product.GetHashCode();
    }

    public override string ToString()
    {
        return $"{Product}, 数量: {Quantity}, 小计: {Amount:C}";
    }
}
public class Order : IComparable<Order>
{
    public string OrderId { get; set; }
    public Customer Customer { get; set; }
    public List<OrderDetail> Details { get; set; } = new List<OrderDetail>();
    public DateTime OrderDate { get; set; }

    public Order(string orderId, Customer customer, DateTime orderDate)
    {
        OrderId = orderId;
        Customer = customer;
        OrderDate = orderDate;
    }

    public decimal TotalAmount => Details.Sum(d => d.Amount);

    public void AddDetail(OrderDetail detail)
    {
        if (Details.Contains(detail))
        {
            throw new ArgumentException("订单明细已存在，不能重复添加");
        }
        Details.Add(detail);
    }

    public void RemoveDetail(Product product)
    {
        var detail = Details.FirstOrDefault(d => d.Product.Equals(product));
        if (detail == null)
        {
            throw new ArgumentException("订单中不包含该商品的明细");
        }
        Details.Remove(detail);
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Order other = (Order)obj;
        return OrderId == other.OrderId;
    }

    public override int GetHashCode()
    {
        return OrderId.GetHashCode();
    }

    public override string ToString()
    {
        string details = string.Join("\n  ", Details.Select(d => d.ToString()));
        return $"订单号: {OrderId}, 客户: {Customer.Name}, 订单日期: {OrderDate:yyyy-MM-dd}\n" +
               $"订单明细:\n  {details}\n" +
               $"总金额: {TotalAmount:C}";
    }

    public int CompareTo(Order other)
    {
        if (other == null) return 1;
        return OrderId.CompareTo(other.OrderId);
    }
}
public class OrderService
{
    private List<Order> orders = new List<Order>();

    // 添加订单
    public void AddOrder(Order order)
    {
        if (orders.Contains(order))
        {
            throw new ArgumentException("订单已存在，不能重复添加");
        }
        orders.Add(order);
    }

    // 删除订单
    public void RemoveOrder(string orderId)
    {
        var order = orders.FirstOrDefault(o => o.OrderId == orderId);
        if (order == null)
        {
            throw new ArgumentException($"订单号 {orderId} 不存在，无法删除");
        }
        orders.Remove(order);
    }

    // 修改订单
    public void UpdateOrder(Order order)
    {
        var existingOrder = orders.FirstOrDefault(o => o.OrderId == order.OrderId);
        if (existingOrder == null)
        {
            throw new ArgumentException($"订单号 {order.OrderId} 不存在，无法修改");
        }
        orders.Remove(existingOrder);
        orders.Add(order);
    }

    // 查询所有订单
    public List<Order> GetAllOrders()
    {
        return orders.OrderBy(o => o.TotalAmount).ToList();
    }

    // 按订单号查询
    public List<Order> QueryByOrderId(string orderId)
    {
        return orders.Where(o => o.OrderId.Contains(orderId))
                    .OrderBy(o => o.TotalAmount)
                    .ToList();
    }

    // 按商品名称查询
    public List<Order> QueryByProductName(string productName)
    {
        return orders.Where(o => o.Details.Any(d => d.Product.Name.Contains(productName)))
                    .OrderBy(o => o.TotalAmount)
                    .ToList();
    }

    // 按客户查询
    public List<Order> QueryByCustomer(string customerName)
    {
        return orders.Where(o => o.Customer.Name.Contains(customerName))
                    .OrderBy(o => o.TotalAmount)
                    .ToList();
    }

    // 按金额范围查询
    public List<Order> QueryByAmountRange(decimal min, decimal max)
    {
        return orders.Where(o => o.TotalAmount >= min && o.TotalAmount <= max)
                    .OrderBy(o => o.TotalAmount)
                    .ToList();
    }

    // 排序方法
    public void SortOrders()
    {
        orders.Sort();
    }

    public void SortOrders(Func<Order, Order, int> compare)
    {
        if (compare == null)
        {
            SortOrders();
            return;
        }
        orders.Sort((a, b) => compare(a, b));
    }

    // 导出订单到文件
    public void Export(string fileName)
    {
        System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<Order>));
        using (FileStream fs = new FileStream(fileName, FileMode.Create))
        {
            serializer.Serialize(fs, orders);
        }
    }

    // 从文件导入订单
    public void Import(string fileName)
    {
        if (!File.Exists(fileName))
            throw new FileNotFoundException("文件不存在");

        System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<Order>));
        using (FileStream fs = new FileStream(fileName, FileMode.Open))
        {
            List<Order> importedOrders = (List<Order>)serializer.Deserialize(fs);
            orders.Clear();
            orders.AddRange(importedOrders);
        }
    }
}
class Program
{
    static OrderService orderService = new OrderService();

    static void Main(string[] args)
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\n订单管理系统");
            Console.WriteLine("1. 添加订单");
            Console.WriteLine("2. 删除订单");
            Console.WriteLine("3. 修改订单");
            Console.WriteLine("4. 查询订单");
            Console.WriteLine("5. 显示所有订单");
            Console.WriteLine("6. 排序订单");
            Console.WriteLine("7. 导入订单");
            Console.WriteLine("8. 导出订单");
            Console.WriteLine("0. 退出");
            Console.Write("请选择操作: ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                try
                {
                    switch (choice)
                    {
                        case 1:
                            AddOrder();
                            break;
                        case 2:
                            RemoveOrder();
                            break;
                        case 3:
                            UpdateOrder();
                            break;
                        case 4:
                            QueryOrders();
                            break;
                        case 5:
                            DisplayAllOrders();
                            break;
                        case 6:
                            SortOrders();
                            break;
                        case 7:
                            ImportOrders();
                            break;
                        case 8:
                            ExportOrders();
                            break;
                        case 0:
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("无效的选择，请重新输入。");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"发生错误: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("无效的输入，请输入数字。");
            }
        }
    }

    static void AddOrder()
    {
        Console.Write("输入订单号: ");
        string orderId = Console.ReadLine();
        Console.Write("输入客户ID: ");
        string customerId = Console.ReadLine();
        Console.Write("输入客户姓名: ");
        string customerName = Console.ReadLine();

        Customer customer = new Customer(customerId, customerName);
        Order order = new Order(orderId, customer, DateTime.Now);

        bool addingDetails = true;
        while (addingDetails)
        {
            Console.Write("输入商品ID (或输入'完成'结束添加): ");
            string productId = Console.ReadLine();
            if (productId.ToLower() == "完成") break;

            Console.Write("输入商品名称: ");
            string productName = Console.ReadLine();
            Console.Write("输入商品单价: ");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("输入商品数量: ");
            int quantity = int.Parse(Console.ReadLine());

            Product product = new Product(productId, productName, price);
            OrderDetail detail = new OrderDetail(product, quantity);
            order.AddDetail(detail);
        }

        orderService.AddOrder(order);
        Console.WriteLine("订单添加成功!");
    }

    static void RemoveOrder()
    {
        Console.Write("输入要删除的订单号: ");
        string orderId = Console.ReadLine();
        orderService.RemoveOrder(orderId);
        Console.WriteLine("订单删除成功!");
    }

    static void UpdateOrder()
    {
        Console.Write("输入要修改的订单号: ");
        string orderId = Console.ReadLine();

        // 查找现有订单
        var existingOrders = orderService.QueryByOrderId(orderId);
        if (existingOrders.Count == 0)
        {
            Console.WriteLine("订单不存在");
            return;
        }

        var existingOrder = existingOrders[0];
        Console.WriteLine("现有订单信息:");
        Console.WriteLine(existingOrder);

        // 创建新订单
        Console.Write("输入新客户ID (留空保持不变): ");
        string customerId = Console.ReadLine();
        Console.Write("输入新客户姓名 (留空保持不变): ");
        string customerName = Console.ReadLine();

        Customer customer = new Customer(
            string.IsNullOrEmpty(customerId) ? existingOrder.Customer.CustomerId : customerId,
            string.IsNullOrEmpty(customerName) ? existingOrder.Customer.Name : customerName
        );

        Order newOrder = new Order(existingOrder.OrderId, customer, existingOrder.OrderDate);

        // 复制原有明细
        foreach (var detail in existingOrder.Details)
        {
            newOrder.AddDetail(detail);
        }

        // 修改明细
        bool modifyingDetails = true;
        while (modifyingDetails)
        {
            Console.WriteLine("\n1. 添加商品");
            Console.WriteLine("2. 删除商品");
            Console.WriteLine("3. 完成修改");
            Console.Write("选择操作: ");

            if (int.TryParse(Console.ReadLine(), out int detailChoice))
            {
                switch (detailChoice)
                {
                    case 1:
                        Console.Write("输入商品ID: ");
                        string productId = Console.ReadLine();
                        Console.Write("输入商品名称: ");
                        string productName = Console.ReadLine();
                        Console.Write("输入商品单价: ");
                        decimal price = decimal.Parse(Console.ReadLine());
                        Console.Write("输入商品数量: ");
                        int quantity = int.Parse(Console.ReadLine());

                        Product product = new Product(productId, productName, price);
                        OrderDetail detail = new OrderDetail(product, quantity);
                        newOrder.AddDetail(detail);
                        Console.WriteLine("商品添加成功!");
                        break;
                    case 2:
                        Console.Write("输入要删除的商品ID: ");
                        string removeProductId = Console.ReadLine();
                        var productToRemove = new Product(removeProductId, "", 0);
                        newOrder.RemoveDetail(productToRemove);
                        Console.WriteLine("商品删除成功!");
                        break;
                    case 3:
                        modifyingDetails = false;
                        break;
                    default:
                        Console.WriteLine("无效的选择");
                        break;
                }
            }
        }

        orderService.UpdateOrder(newOrder);
        Console.WriteLine("订单修改成功!");
    }

    static void QueryOrders()
    {
        Console.WriteLine("\n查询选项:");
        Console.WriteLine("1. 按订单号查询");
        Console.WriteLine("2. 按商品名称查询");
        Console.WriteLine("3. 按客户查询");
        Console.WriteLine("4. 按金额范围查询");
        Console.Write("选择查询方式: ");

        if (int.TryParse(Console.ReadLine(), out int queryChoice))
        {
            List<Order> results = new List<Order>();
            switch (queryChoice)
            {
                case 1:
                    Console.Write("输入订单号(或部分): ");
                    string orderId = Console.ReadLine();
                    results = orderService.QueryByOrderId(orderId);
                    break;
                case 2:
                    Console.Write("输入商品名称(或部分): ");
                    string productName = Console.ReadLine();
                    results = orderService.QueryByProductName(productName);
                    break;
                case 3:
                    Console.Write("输入客户姓名(或部分): ");
                    string customerName = Console.ReadLine();
                    results = orderService.QueryByCustomer(customerName);
                    break;
                case 4:
                    Console.Write("输入最小金额: ");
                    decimal min = decimal.Parse(Console.ReadLine());
                    Console.Write("输入最大金额: ");
                    decimal max = decimal.Parse(Console.ReadLine());
                    results = orderService.QueryByAmountRange(min, max);
                    break;
                default:
                    Console.WriteLine("无效的选择");
                    return;
            }

            if (results.Count == 0)
            {
                Console.WriteLine("没有找到匹配的订单");
            }
            else
            {
                Console.WriteLine($"找到 {results.Count} 个订单:");
                foreach (var order in results)
                {
                    Console.WriteLine(order);
                    Console.WriteLine("----------------------");
                }
            }
        }
    }

    static void DisplayAllOrders()
    {
        var orders = orderService.GetAllOrders();
        if (orders.Count == 0)
        {
            Console.WriteLine("没有订单");
            return;
        }

        Console.WriteLine("所有订单:");
        foreach (var order in orders)
        {
            Console.WriteLine(order);
            Console.WriteLine("----------------------");
        }
    }

    static void SortOrders()
    {
        Console.WriteLine("\n排序选项:");
        Console.WriteLine("1. 按订单号排序(默认)");
        Console.WriteLine("2. 按总金额排序");
        Console.WriteLine("3. 按订单日期排序");
        Console.Write("选择排序方式: ");

        if (int.TryParse(Console.ReadLine(), out int sortChoice))
        {
            switch (sortChoice)
            {
                case 1:
                    orderService.SortOrders();
                    Console.WriteLine("已按订单号排序");
                    break;
                case 2:
                    orderService.SortOrders((a, b) => a.TotalAmount.CompareTo(b.TotalAmount));
                    Console.WriteLine("已按总金额排序");
                    break;
                case 3:
                    orderService.SortOrders((a, b) => a.OrderDate.CompareTo(b.OrderDate));
                    Console.WriteLine("已按订单日期排序");
                    break;
                default:
                    Console.WriteLine("无效的选择，使用默认排序");
                    orderService.SortOrders();
                    break;
            }
        }
    }

    static void ImportOrders()
    {
        Console.Write("输入要导入的文件名: ");
        string fileName = Console.ReadLine();
        orderService.Import(fileName);
        Console.WriteLine("订单导入成功!");
    }

    static void ExportOrders()
    {
        Console.Write("输入要导出的文件名: ");
        string fileName = Console.ReadLine();
        orderService.Export(fileName);
        Console.WriteLine("订单导出成功!");
    }
}