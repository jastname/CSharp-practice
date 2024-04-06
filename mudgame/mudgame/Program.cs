using System;
using System.IO;

class Game
{
    public string Name;
    public int Gold = 1000;
    Random random = new Random();
    public void GameSet()
    {
        Console.WriteLine("당신의 이름을 입력하세요.");
        Name = Console.ReadLine();
        string UserInput = "";
        Coin[] coins = new Coin[5]; // 코인 클래스 불러오기 
        coins[0] = new Coin("Bitcoin", 50000);
        coins[1] = new Coin("Ethereum", 2000);
        coins[2] = new Coin("Litecoin", 150);
        coins[3] = new Coin("Ripple", 1);
        coins[4] = new Coin("Dogecoin", 0.3);
        while (true)
        {
            Console.WriteLine("이동할 장소를 선택하세요(광장, 상점)");
            UserInput = Console.ReadLine();
            if (UserInput == "광장")
            {
                Console.WriteLine("광장으로 이동합니다.");
                break;
            }
            else if (UserInput == "상점")
            {
                Console.WriteLine("상점으로 이동합니다.");
                Shop();
                break;
            }
            else
            {
                Console.WriteLine("이동할 장소를 잘못입력하였습니다.");
            }
        }
    }

    public void Shop()
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
            Console.Clear(); // 코인 가격 갱신
        }
    }
}

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

class MainCode
{
    public static void Main(string[] args)
    {
        Game game = new Game();
        game.GameSet();
    }
}
