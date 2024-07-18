using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Game
{
    public string Name { get; set; }
    public int Gold { get; set; } = 10000;
    public int Day { get; set; } = 1;
    public string UserInput { get; set; } = "";
    public int LotteryTicket { get; set; } = 0;
    private Random random = new Random();
    public List<Coin> Coins { get; set; } = new List<Coin>()
    {
        new Coin("Bitcoin", 50000),
        new Coin("Ethereum", 20000),
        new Coin("Litecoin", 1500),
        new Coin("Ripple", 100),
        new Coin("Dogecoin", 3)
    };
    public List<Coin> NewCoins { get; set; } = new List<Coin>()
    {
        new Coin("Polkadot", 300),
        new Coin("Cardano", 10.5),
        new Coin("Chainlink", 8000),
        new Coin("Shiba_inu", 5),
        new Coin("pepe", 4000)
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
            if (Day > 80)
            {
                EndGame();
                break;
            }

            DisplayCurrentDay();
            AddNewCoin();
            GetLocationInput();
        }
    }

    private void DisplayCurrentDay()
    {
        Console.WriteLine($"이동할 장소를 선택하세요(광장, 집)\t현재:{Day}일차");
    }

    private void AddNewCoin()
    {
        double newCoinProbability = 0.05; // 5% 확률로 새로운 코인 추가
        if (random.NextDouble() < newCoinProbability && NewCoins.Count > 0)
        {
            Coin newCoin = NewCoins[0];
            Coins.Add(newCoin);
            NewCoins.RemoveAt(0);
            Console.WriteLine($"{newCoin.Name} 코인이 새로 상장되었습니다!");
        }
    }

    private void GetLocationInput()
    {
        UserInput = Console.ReadLine().ToLower();
        switch (UserInput)
        {
            case "광장":
                MoveToSquare();
                break;
            case "집":
                MoveToHome();
                break;
            default:
                InvalidLocationInput();
                break;
        }
    }

    private void MoveToSquare()
    {
        Console.WriteLine("광장으로 이동합니다.");
        Place();
    }

    private void MoveToHome()
    {
        Console.WriteLine("집으로 이동합니다.");
        Home();
    }

    private void InvalidLocationInput()
    {
        Console.WriteLine("이동할 장소를 잘못입력하였습니다.");
    }

    private void UpdateCoinPrices()
    {
        for (int i = Coins.Count - 1; i >= 0; i--)
        {
            Coins[i].UpdatePrice(random, Day);
            if (Coins[i].IsDelisted)
            {
                Coins.RemoveAt(i);
            }
        }
    }

    public void Shop()
    {
        while (true)
        {
            DisplayShop();
            GetShopAction();
        }
    }

    private void DisplayShop()
    {
        Console.WriteLine("코인\t\t가격\t\t\t보유량\t변동가격");
        for (int i = 0; i < Coins.Count; i++)
        {
            string change = Day > 1 ? $"{Coins[i].Price - Coins[i].PreviousPrice:F2}" : "N/A";
            Console.WriteLine($"{i + 1}. {Coins[i].Name.PadRight(10)}\t{Coins[i].Price,10:F2}\t{Coins[i].Quantity,10}\t{change,10}");
        }
        Console.WriteLine("s. 코인 구매, b. 코인 판매, h. 집으로 돌아가기");
    }

    private void GetShopAction()
    {
        UserInput = Console.ReadLine().ToLower();
        switch (UserInput)
        {
            case "s" or "ㄴ":
                BuyCoin();
                break;
            case "b" or "ㅠ":
                SellCoin();
                break;
            case "h" or "ㅗ":
                Console.WriteLine("집으로 이동합니다.");
                Home();
                break;
            default:
                Console.WriteLine("잘못입력하였습니다.");
                break;
        }
    }

    public void BuyCoin()
    {
        Console.WriteLine("구매할 코인을 선택하세요 (숫자 입력)");
        if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= Coins.Count)
        {
            Console.WriteLine($"얼마나 구매하시겠습니까? (보유 자금: {Gold}원)");
            if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0)
            {
                Coin selectedCoin = Coins[choice - 1];
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
        if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= Coins.Count)
        {
            Coin selectedCoin = Coins[choice - 1];
            if (selectedCoin.Quantity > 0)
            {
                Console.WriteLine($"얼마나 판매하시겠습니까? (보유량: {selectedCoin.Quantity})");
                if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0 && quantity <= selectedCoin.Quantity)
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
        while (true)
        {
            if (Day > 80)
            {
                EndGame();
                break;
            }

            DisplayHomeActions();
            GetHomeActionInput();
        }
    }

    private void DisplayHomeActions()
    {
        Console.WriteLine("취할 행동을 고르세요.(수면, 코인, 가방, 저장, 불러오기, 나가기 입력)");
    }

    private void GetHomeActionInput()
    {
        UserInput = Console.ReadLine().ToLower();
        switch (UserInput)
        {
            case "수면":
                Sleep();
                break;
            case "코인":
                DisplayCoins();
                break;
            case "가방":
                DisplayBackpack();
                break;
            case "저장":
                SaveGame();
                break;
            case "불러오기":
                LoadGame();
                break;
            case "나가기":
                ExitHome();
                break;
            default:
                InvalidHomeActionInput();
                break;
        }
    }

    private void Sleep()
    {
        Day++;
        UpdateCoinPrices();
        Console.WriteLine($"하루가 지나 {Day}일차입니다");
    }

    private void DisplayCoins()
    {
        Console.WriteLine("코인을 확인합니다.");
        Shop();
    }

    private void DisplayBackpack()
    {
        Backpack();
    }

    private void ExitHome()
    {
        Console.WriteLine("집을 나갑니다.");
        GameUpdate();
    }

    private void InvalidHomeActionInput()
    {
        Console.WriteLine("잘못입력하였습니다");
    }

    public void UseItem()
    {
        Console.WriteLine("사용할 아이템을 선택하세요.");
        Console.WriteLine($"즉석복권\t{LotteryTicket}개");
        Console.WriteLine("나가기");

        UserInput = Console.ReadLine().ToLower();
        switch (UserInput)
        {
            case "즉석복권":
                UseLotteryTicket();
                break;
            case "나가기":
                Home();
                break;
            default:
                Console.WriteLine("잘못입력하였습니다");
                UseItem();
                break;
        }
    }

    private void UseLotteryTicket()
    {
        if (LotteryTicket > 0)
        {
            LotteryTicket--;
            int prize = ScratchLottery();
            if (prize > 0)
            {
                Gold += prize;
                Console.WriteLine($"축하합니다! {prize}원을 획득하였습니다. (보유 자금: {Gold}원)");
            }
            else
            {
                Console.WriteLine("아쉽게도 당첨되지 않았습니다.");
            }
        }
        else
        {
            Console.WriteLine("즉석복권이 없습니다.");
        }
        UseItem();
    }

    public int ScratchLottery()
    {
        double rand = random.NextDouble();
        if (rand < 0.01)
            return 1000000;
        else if (rand < 0.05)
            return 200000;
        else if (rand < 0.15)
            return 50000;
        else if (rand < 0.35)
            return 5000;
        else if (rand < 0.55)
            return 1000;
        else
            return 0;
    }

    public void Place()
    {
        Console.WriteLine("무엇을 하시겠습니까?(아르바이트 또는 복권 입력)");
        UserInput = Console.ReadLine().ToLower();
        switch (UserInput)
        {
            case "아르바이트":
                PartTimeJob();
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

    private void PartTimeJob()
    {
        Console.WriteLine("아르바이트를 시작합니다");
        for (int i = 0; i < 3; i++)
        {
            int randomGold = random.Next(300, 3000);
            Gold += randomGold;
            Console.WriteLine($"획득한 Gold: {randomGold} (보유 자금 : {Gold})");
        }
        Console.WriteLine($"현재 보유 자금은 {Gold}원 입니다.");
        Console.WriteLine("아르바이트를 하여 하루가 지나갑니다~");
        Day++;
        UpdateCoinPrices();
        Console.WriteLine($"하루가 지나 {Day}일차입니다");
        Home();
    }

    public void Lotto()
    {
        Console.WriteLine("즉석복권을 구매하시겠습니까? 가격은 5000원입니다.\t(예, 아니요)");
        UserInput = Console.ReadLine().ToLower();
        switch (UserInput)
        {
            case "예":
                BuyLotteryTicket();
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

    private void BuyLotteryTicket()
    {
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
    }

    public void Backpack()
    {
        Console.WriteLine("가방을 열었습니다.");
        Console.WriteLine($"소지품\t\t보유 갯수\t (보유자금 : {Gold}원)");
        Console.WriteLine($"즉석복권\t {LotteryTicket}");
        Console.WriteLine("보유 코인");
        for (int i = 0; i < Coins.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Coins[i].Name}\t{Coins[i].Quantity}개");
        }
        GetBackpackAction();
    }

    private void GetBackpackAction()
    {
        UserInput = Console.ReadLine().ToLower();
        switch (UserInput)
        {
            case "나가기":
                Home();
                break;
            case "아이템사용":
                UseItem();
                break;
            default:
                Console.WriteLine("잘못입력하였습니다");
                Backpack();
                break;
        }
    }

    public void SaveGame()
    {
        Console.WriteLine("저장할 파일 이름을 입력하세요:");
        string fileName = Console.ReadLine();
        string directoryPath = "saves";
        string filePath = Path.Combine(directoryPath, $"{fileName}.json");

        try
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string jsonString = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, jsonString);
            Console.WriteLine("게임이 저장되었습니다.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"게임 저장 중 오류가 발생했습니다: {ex.Message}");
        }
    }

    public void LoadGame()
    {
        Console.WriteLine("불러올 파일 이름을 입력하세요:");
        string fileName = Console.ReadLine();
        string directoryPath = "saves";
        string filePath = Path.Combine(directoryPath, $"{fileName}.json");

        try
        {
            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);
                Game loadedGame = JsonSerializer.Deserialize<Game>(jsonString);

                this.Name = loadedGame.Name;
                this.Gold = loadedGame.Gold;
                this.Day = loadedGame.Day;
                this.LotteryTicket = loadedGame.LotteryTicket;
                this.Coins = loadedGame.Coins;
                this.NewCoins = loadedGame.NewCoins;

                Console.WriteLine("게임이 불러와졌습니다.");
                Home();
            }
            else
            {
                Console.WriteLine("저장된 게임이 없습니다.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"게임 불러오기 중 오류가 발생했습니다: {ex.Message}");
        }
    }

    public void EndGame()
    {
        Console.WriteLine("축하합니다! 80일차가 완료되었습니다. 게임이 종료됩니다.");
        Console.WriteLine($"최종 보유 자금: {Gold}원");
        Environment.Exit(0);
    }
}

class Coin
{
    public string Name { get; set; }
    public double Price { get; set; }
    public double PreviousPrice { get; set; }
    public int Quantity { get; set; }
    public bool IsDelisted { get; set; }

    public Coin(string name, double price)
    {
        Name = name;
        Price = price;
        PreviousPrice = price;
        Quantity = 0;
        IsDelisted = false;
    }

    public void UpdatePrice(Random random, int day)
    {
        PreviousPrice = Price;
        double trend = 0.1; // 장기적인 상승 추세 반영 (예: 매일 0.1% 상승)
        double dailyVolatility = random.NextDouble() * 0.2 - 0.1; // -10% ~ +10% 변동

        // 큰 변동 이벤트 발생 확률 (5%)
        bool isMajorEvent = random.NextDouble() < 0.05;
        if (isMajorEvent)
        {
            // 큰 변동은 ±30% 범위 내
            dailyVolatility = random.NextDouble() * 0.6 - 0.3;
            Console.WriteLine($"{Name}의 가격이 크게 변동했습니다!");
        }

        double percentChange = trend + dailyVolatility;
        Price += Price * percentChange / 100.0;

        // 가격이 0 이하로 내려가면 상장 폐지
        if (Price <= 0.01)
        {
            IsDelisted = true;
            Price = 0.00; //0원으로 고정
            Console.WriteLine($"{Name}은(는) 상장 폐지되었습니다.");
        }
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
