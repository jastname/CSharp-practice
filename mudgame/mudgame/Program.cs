using System;
using System.Net.Sockets;
using System.Reflection.Metadata.Ecma335;




class Game
{
    public string Name;
    public int Gold = 1000;
    public int Day = 1;
    public string UserInput = "";
    public int Lottery_ticket = 0;
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
                Console.WriteLine("광장으로 이동하였습니다.");
                Place();
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
        Console.WriteLine("현재 위치는 집입니다.");
        Console.WriteLine("취할 행동을 고르세요.(수면,코인,아이템을 입력하세요.)");
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
    public void Place()
    {
        
        Console.WriteLine("무엇을 하시겠습니다?(아르바이트또는 복권)");
        UserInput = Console.ReadLine();
        if (UserInput == "아르바이트")
        {
            Console.WriteLine("아르바이트를 시작합니다");
            for (int i = 0; i < 3; i++)
            {
                int randomGold = random.Next(300, 500);
                Gold += randomGold;
                Console.WriteLine($"획득한 Gold: {randomGold} (보유 자금 : {Gold})");
            }
            Console.WriteLine($"현재 보유 자금은 {Gold}원 입니다.");
            Console.WriteLine("아르바이트를 하여 하루가 지나갑니다~");
            Day += 1;
            Console.WriteLine($"하루가 지나 {Day}일차입니다");

            Home();
        }
        else if(UserInput == "복권")
        {
            Console.WriteLine("즉석복권을 구매하시겠습니까? 가격은 1000원입니다.\t(예,아니요)");
            UserInput = Console.ReadLine();
            if (UserInput == "예")
            {
                if (Gold >= 1000)
                {
                    Lottery_ticket += 1;
                    Gold = Gold - 1000;
                    Console.WriteLine($"복권을 1장 구매하였습니다.\t(보유자금 : {Gold}원");
                    Place();
                }

                else
                {
                    Console.WriteLine("보유 자금이 부족합니다.");
                    Place();
                }
            }
            else if (UserInput == "나가기")
            {
                GameUpdate();
            }
            else
            {
                Console.WriteLine("잘못입력하였습니다");
                Home();
            }

        }

    }

    public void Luck()
    {
        Console.WriteLine("즉석복권을 구매하였습니다.");
        Random random = new Random();

        Console.WriteLine("복권 추첨을 시작합니다!");

        double[] probabilities = { 43.0, 10.2, 33.1, 5.1, 7.0, (100 - (43.0 + 10.2 + 33.1 + 5.1 + 7.0)) }; // 각 상금에 대한 확률 배열

        int[] prizeValues = { 0, 500, 1000, 100, 3000, 50000 }; // 각 상금에 대응하는 금액 배열

        double randomValue = random.NextDouble() * 100; // 0부터 100 사이의 랜덤 값

        double cumulativeProbability = 0.0;
        bool prizeWon = false;

        // 각 상금에 대한 확률을 검사하여 당첨 여부 판정
        for (int i = 0; i < probabilities.Length; i++)
        {
            cumulativeProbability += probabilities[i];

            if (randomValue < cumulativeProbability)
            {
                // 해당 확률 범위에 속하면 해당 상금 당첨
                Console.WriteLine($"추첨 결과: {prizeValues[i]}원 당첨!");
                Console.WriteLine($"상금: {prizeValues[i]}원\t (보유 자금 : {Gold})");
                Gold += prizeValues[i];
                prizeWon = true;
                break;
            }

        }
        if (!prizeWon)
        {
            // 모든 상금 확률 범위를 통과했는데도 당첨된 상금이 없으면 꽝
            Console.WriteLine($"추첨 결과: 꽝...");
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
        double percentChange = rand.NextDouble() * (0.03 + 0.015) - 0.03; // -1.5%에서 3% 사이의 랜덤한 변동
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
