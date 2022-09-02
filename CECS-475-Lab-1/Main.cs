namespace Stock;

class CECS475Lab1
{
    static void Main(string[] args)
    {
        // Init Stocks
        Stock stock1 = new Stock("Technology", 160, 5, 15);
        Stock stock2 = new Stock("Retail", 30, 2, 6);
        Stock stock3 = new Stock("Banking", 90, 4, 10);
        Stock stock4 = new Stock("Commodity", 500, 20, 50);

        // Print header
        string titles = "Broker".PadRight(16) + "Stock".PadRight(16) +
            "Value".PadRight(16) + "Changes".PadRight(10) + "Date and Time";
        Console.WriteLine(titles);

        // Write header
        string destPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Lab1_output.txt");
        // Empty Output file
        System.IO.File.WriteAllText(destPath, string.Empty);

        using (StreamWriter outputFile = new StreamWriter(destPath, false))
        {
            outputFile.WriteLine(titles);
        }

        StockBroker b1 = new StockBroker("Broker 1");
        b1.AddStock(stock1);
        b1.AddStock(stock2);
        StockBroker b2 = new StockBroker("Broker 2");
        b2.AddStock(stock1);
        b2.AddStock(stock3);
        b2.AddStock(stock4);
        StockBroker b3 = new StockBroker("Broker 3");
        b3.AddStock(stock1);
        b3.AddStock(stock3);
        StockBroker b4 = new StockBroker("Broker 4");
        b4.AddStock(stock1);
        b4.AddStock(stock2);
        b4.AddStock(stock3);
        b4.AddStock(stock4);
    }
}