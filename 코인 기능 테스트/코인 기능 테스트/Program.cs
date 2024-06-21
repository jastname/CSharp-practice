using System;

class Coin
{
    public string Name { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }

    public Coin(string name, double price)
    {
        Name = name;
        Price = price;
        Quantity = 0;
    }

    public void UpdatePrice(Random random, int day)
    {
        double x = random.NextDouble() * 2 * Math.PI; // 0에서 2π까지의 랜덤 각도 (라디안)
        double percentChange = (day * Math.Sin(x))+1;
        Price += Price * percentChange / 100.0; // percentChange는 퍼센트로 변환
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

        int day = 1;
        Random random = new Random();

        while (true)
        {
            Console.WriteLine($"코인\t가격\t\t{day}");
            foreach (var coin in coins)
            {
                coin.UpdatePrice(random, day);
                Console.WriteLine($"{coin.Name}\t{coin.Price:F2}"); // 소수점 둘째 자리까지 표시
            }
            day++; // day 변수 증가
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            Console.Clear(); // 콘솔을 깨끗하게 지움
        }
    }
}
