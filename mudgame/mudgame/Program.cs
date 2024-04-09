using System;
using System.IO;
using System.Runtime.InteropServices;



class Game
{
    public string Name;
    public int Gold = 1000;
    public int Day = 1;
    public string UserInput = "";
    Random random = new Random();
    public void GameSet()
    {
        Console.WriteLine("당신의 이름을 입력하세요.");
        
        Name = Console.ReadLine();
        Console.WriteLine($"당신의 이름은 {Name}이군요? {Day}일차를 시작합니다.");
        Coin[] coins = new Coin[5]; // 코인 클래스 불러오기 
        coins[0] = new Coin("Bitcoin", 50000);
        coins[1] = new Coin("Ethereum", 2000);
        coins[2] = new Coin("Litecoin", 150);
        coins[3] = new Coin("Ripple", 1);
        coins[4] = new Coin("Dogecoin", 0.3);
        GameUpdate();
    }
    public void GameUpdate() 
    { 
        while (true)
        {
            Console.WriteLine($"이동할 장소를 선택하세요(광장, 집)\t현재:{Day}일차");
            UserInput = Console.ReadLine();
            if (UserInput == "광장")
            {
                Console.WriteLine("광장으로 이동합니다.");
                break;
            }
            else if (UserInput == "집")
            {
                Console.WriteLine("집으로 이동합니다.");
                Home();
                break;
            }
            else
            {
                Console.WriteLine("이동할 장소를 잘못입력하였습니다.");
                GameUpdate();
            }
        }
    }

    public void Shop()
    {
        int shop_day = 1;
        Coin[] coins = new Coin[5];
        coins[0] = new Coin("Bitcoin", 50000);
        coins[1] = new Coin("Ethereum", 2000);
        coins[2] = new Coin("Litecoin", 150);
        coins[3] = new Coin("Ripple", 1);
        coins[4] = new Coin("Dogecoin", 0.3);
        if ( Day > shop_day)
        {
            shop_day = Day;
            foreach (var coin in coins)
            {
                coin.UpdatePrice();
            }
                
        }
        while (true)
        {
            Console.WriteLine("코인\t가격");
            foreach (var coin in coins)
            {
                
                Console.WriteLine($"{coin.Name}\t{coin.Price:F2}"); // 소수점 둘째 자리까지 표시
            }
            Console.WriteLine("집으로 돌아가려면 집을 입력하세요");
            UserInput = Console.ReadLine();
            if (UserInput == "집")
            {
                Console.WriteLine("집으로 이동합니다.");
                Home() ;
            }
            else
            {
                Console.WriteLine("잘못입력하였습니다.");
                Shop();
            }
            Console.ReadLine();
        }
    }


    public void Home()
    {
        string UserInput = "";
        Console.WriteLine("현재 위치는 집입니다.");
        Console.WriteLine("수면 또는 코인을 확인하시겠습니까?(수면 또는 코인을 입력하세요.)");
        UserInput = Console.ReadLine();
        while (true)
        {
            if (UserInput == "수면")
            {
                Day += 1;
                Console.WriteLine($"하루가 지나 {Day}일차입니다");

                Home();
            }
            else if (UserInput == "코인")
            {
                Console.WriteLine("코인을 확인합니다.");
                Shop();
                break;
            }
            else if (UserInput == "나가기")
            {
                Console.WriteLine("집을 나갑니다.");
                GameUpdate();
            }
            else
            {
                Console.WriteLine("잘못입력하였습니다");
                Home();
            }
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
