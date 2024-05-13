using System;
using System.Collections.Generic;

class Game
{
    private string Name;
    private int Gold = 1000;
    private int Day = 1;
    private string UserInput = "";
    private int LotteryTicket = 0;
    private Random random = new Random();
    private List<Coin> coins = new List<Coin>()
    {
        new Coin("Bitcoin", 50000),
        new Coin("Ethereum", 2000),
        new Coin("Litecoin", 150),
        new Coin("Ripple", 1),
        new Coin("Dogecoin", 0.3)
    };

    public void GameSet()
    {
        Console.WriteLine("당신의 이름을 입력하세요.");
        Name = Console.ReadLine();
        Console.WriteLine($"당신의 이름은 {Name}이군요? {Day}일차를 시작합니다.");
        GameUpdate();
    }

    public void GameUpdate()
    {
        while (true)
        {
            Console.WriteLine($"이동할 장소를 선택하세요(광장, 집)\t현재:{Day}일차");
            UserInput = Console.ReadLine().ToLower();
            switch (UserInput)
            {
                case "광장":
                    Console.WriteLine("광장으로 이동합니다.");
                    Place();
                    return;
                case "집":
                    Console.WriteLine("집으로 이동합니다.");
                    Home();
                    return;
                default:
                    Console.WriteLine("이동할 장소를 잘못입력하였습니다.");
                    break;
            }
        }
    }

    public void Shop()
    {
        if (Day > 1)
        {
            foreach (var coin in coins)
                coin.UpdatePrice();
        }

        while (true)
        {
            Console.WriteLine("코인\t가격\t보유량");
            for (int i = 0; i < coins.Count; i++)
                Console.WriteLine($"{i + 1}. {coins[i].Name}\t{coins[i].Price:F2}\t{coins[i].Quantity}");

            Console.WriteLine("s. 코인 구매, b. 코인 판매, h. 집으로 돌아가기");
            UserInput = Console.ReadLine().ToLower();
            switch (UserInput)
            {
                case "s":
                    BuyCoin();
                    break;
                case "b":
                    SellCoin();
                    break;
                case "h":
                    Console.WriteLine("집으로 이동합니다.");
                    Home();
                    return;
                default:
                    Console.WriteLine("잘못입력하였습니다.");
                    break;
            }
        }
    }

    public void BuyCoin()
    {
        Console.WriteLine("구매할 코인을 선택하세요 (숫자 입력)");
        int choice;
        if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= coins.Count)
        {
            Console.WriteLine($"얼마나 구매하시겠습니까? (보유 자금: {Gold}원)");
            int quantity;
            if (int.TryParse(Console.ReadLine(), out quantity) && quantity > 0)
            {
                Coin selectedCoin = coins[choice - 1];
                int totalPrice = (int)(selectedCoin.Price * quantity);
                if (totalPrice <= Gold)
                {
                    Gold -= totalPrice;
                    selectedCoin.Quantity += quantity;
                    Console.WriteLine($"{selectedCoin.Name}을(를) {quantity}개 구매하였습니다. (보유 자금: {Gold}원)");
                }
                else
                {
                    Console.WriteLine("보유 자금이 부족합니다.");
                }
            }
            else
            {
                Console.WriteLine("잘못된 수량입니다.");
            }
        }
        else
        {
            Console.WriteLine("잘못된 선택입니다.");
        }
    }

    public void SellCoin()
    {
        Console.WriteLine("판매할 코인을 선택하세요 (숫자 입력)");
        int choice;
        if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= coins.Count)
        {
            Coin selectedCoin = coins[choice - 1];
            if (selectedCoin.Quantity > 0)
            {
                Console.WriteLine($"얼마나 판매하시겠습니까? (보유량: {selectedCoin.Quantity})");
                int quantity;
                if (int.TryParse(Console.ReadLine(), out quantity) && quantity > 0 && quantity <= selectedCoin.Quantity)
                {
                    int totalPrice = (int)(selectedCoin.Price * quantity);
                    Gold += totalPrice;
                    selectedCoin.Quantity -= quantity;
                    Console.WriteLine($"{selectedCoin.Name}을(를) {quantity}개 판매하였습니다. (보유 자금: {Gold}원)");
                }
                else
                {
                    Console.WriteLine("잘못된 수량입니다.");
                }
            }
            else
            {
                Console.WriteLine("보유량이 없습니다.");
            }
        }
        else
        {
            Console.WriteLine("잘못된 선택입니다.");
        }
    }

    public void Home()
    {
        Console.WriteLine("현재 위치는 집입니다.");
        Console.WriteLine("취할 행동을 고르세요.(수면, 코인, 가방, 나가기 입력)");
        UserInput = Console.ReadLine().ToLower();
        switch (UserInput)
        {
            case "수면":
                Day++;
                Console.WriteLine($"하루가 지나 {Day}일차입니다");
                Home();
                break;
            case "코인":
                Console.WriteLine("코인을 확인합니다.");
                Shop();
                break;
            case "가방":
                Backpack();
                break;
            case "나가기":
                Console.WriteLine("집을 나갑니다.");
                GameUpdate();
                break;
            default:
                Console.WriteLine("잘못입력하였습니다");
                Home();
                break;
        }
    }

    public void Place()
    {
        Console.WriteLine("무엇을 하시겠습니까?(아르바이트 또는 복권 입력)");
        UserInput = Console.ReadLine().ToLower();
        switch (UserInput)
        {
            case "아르바이트":
                Console.WriteLine("아르바이트를 시작합니다");
                for (int i = 0; i < 3; i++)
                {
                    int randomGold = random.Next(300, 500);
                    Gold += randomGold;
                    Console.WriteLine($"획득한 Gold: {randomGold} (보유 자금 : {Gold})");
                }
                Console.WriteLine($"현재 보유 자금은 {Gold}원 입니다.");
                Console.WriteLine("아르바이트를 하여 하루가 지나갑니다~");
                Day++;
                Console.WriteLine($"하루가 지나 {Day}일차입니다");
                Home();
                break;
            case "복권":
                Lotto();
                break;
            case "나가기":
                GameUpdate();
                break;
            default:
                Console.WriteLine("잘못입력하였습니다");
                Place();
                break;
        }
    }

    public void Lotto()
    {
        Console.WriteLine("즉석복권을 구매하시겠습니까? 가격은 1000원입니다.\t(예, 아니요)");
        UserInput = Console.ReadLine().ToLower();
        switch (UserInput)
        {
            case "예":
                if (Gold >= 1000)
                {
                    LotteryTicket++;
                    Gold -= 1000;
                    Console.WriteLine($"복권을 1장 구매하였습니다.\t(보유자금 : {Gold}원)");
                    Place();
                }
                else
                {
                    Console.WriteLine("보유 자금이 부족합니다.");
                    Place();
                }
                break;
            case "나가기":
                Place();
                break;
            default:
                Console.WriteLine("잘못입력하였습니다");
                Lotto();
                break;
        }
    }

    public void Backpack()
    {
        Console.WriteLine("가방을 열었습니다.");
        Console.WriteLine($"소지품\t\t보유 갯수\t (보유자금 : {Gold}원)");
        Console.WriteLine($"즉석복권\t {LotteryTicket}");
        Console.WriteLine("보유 코인");
        for (int i = 0; i < coins.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {coins[i].Name}\t{coins[i].Quantity}개");
        }
        UserInput = Console.ReadLine().ToLower();
        switch (UserInput)
        {
            case "나가기":
                Home();
                break;
            case "코인":
                Shop();
                break;
            default:
                Console.WriteLine("잘못입력하였습니다");
                Backpack();
                break;
        }
    }
}

class Coin
{
    public string Name { get; }
    public double Price { get; set; }
    public int Quantity { get; set; }

    public Coin(string name, double price)
    {
        Name = name;
        Price = price;
        Quantity = 0;
    }

    public void UpdatePrice()
    {
        double percentChange = (random.NextDouble() * 0.075) - 0.03; // -1.5%에서 3% 사이의 랜덤한 변동
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


