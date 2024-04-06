using System;

class Coin
{
    public string Name { get; set; }
    public double Price { get; set; }

    public Coin(string name, double price)
    {
        Name = name;
        Price = price;
    }

    public void UpdatePrice()
    {
        Random rand = new Random();
        double percentChange = rand.NextDouble() * (0.03 + 0.015) - 0.015; // -1.5%에서 3% 사이의 랜덤한 변동
        Price += Price * percentChange;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Coin[] coins = new Coin[5];
        coins[0] = new Coin("Bitcoin", 50000);
        coins[1] = new Coin("Ethereum", 2000);
        coins[2] = new Coin("Litecoin", 150);
        coins[3] = new Coin("Ripple", 1);
        coins[4] = new Coin("Dogecoin", 0.3);

        while (true)
        {
            Console.WriteLine("코인\t가격");
            foreach (var coin in coins)
            {
                coin.UpdatePrice();
                Console.WriteLine($"{coin.Name}\t{coin.Price:F2}"); // 소수점 둘째 자리까지 표시
            }
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            Console.Clear(); // 콘솔을 깨끗하게 지움
        }
    }
}

