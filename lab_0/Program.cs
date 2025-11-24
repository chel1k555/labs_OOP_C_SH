using System;
using System.Collections.Generic;

public class Product
{
    public string Name { get; set; }
    public int Price { get; set; }
    public int Quantity { get; set; }

    public Product(string name, int price, int quantity)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }
}

public class Money
{
    private int[] coins_count = { 10, 10, 10, 10 }; //рубасики 1, 2, 5, 10
    private int[] value_coin = { 1, 2, 5, 10 };
    int summ = 0;
    int[] insert_mon = new int[4];
    int insert_sum = 0;

    public Money()
    {
        calculate_sum();
    }

    public void calculate_sum()
    {
        summ = 0;
        for (int i = 0; i < 4; ++i)
        {
            summ += coins_count[i] * value_coin[i];
        }
    }

    public void insert_coins()
    {
        Console.WriteLine("Внесите сумму монетами 10, 5, 2, 1 рубль:");
        insert_sum = 0;
        Array.Clear(insert_mon, 0, insert_mon.Length);

        for (int i = 3; i >= 0; --i)
        {
            int count;
            while (true)
            {
                Console.Write($"Монеты по {value_coin[i]} руб: ");
                string? line = Console.ReadLine();
                if (!int.TryParse(line, out count))
                {
                    Console.WriteLine("Неверный формат. Введите целое неотрицательное число.");
                    continue;
                }
                if (count < 0)
                {
                    Console.WriteLine("Нельзя вводить отрицательное количество монет.");
                    continue;
                }
                break;
            }

            insert_mon[i] = count;
            insert_sum += count * value_coin[i];
        }

        calculate_sum();
        Console.WriteLine($"Внесено: {insert_sum} руб.");
    }

   
    public int buy_product(int price)
    {
        if (insert_sum < price)
        {
            Console.WriteLine($"Недостаточно средств. Нужно: {price} руб., у вас: {insert_sum} руб.");
            return 1;
        }

        int needed_change = insert_sum - price;

        if (needed_change == 0)
        {
            insert_sum = 0;
            Array.Clear(insert_mon, 0, insert_mon.Length);
            Console.WriteLine($"Товар куплен за {price} руб.");
            return 0;
        }
        else
        {
            if (can_give_change(needed_change))
            {
                give_change(needed_change);
                insert_sum = 0;
                Array.Clear(insert_mon, 0, insert_mon.Length);
                return 0;
            }
            else
            {
                Console.WriteLine("В автомате недостаточно денег для сдачи, внесенная сумма будет возвращена.");
                return_money();
                return 2;
            }
        }
    }

    private bool can_give_change(int amount)
    {
        int[] temp_coins = new int[coins_count.Length];
        for (int i = 0; i < coins_count.Length; i++)
        {
            temp_coins[i] = coins_count[i];
        }
        int remaining = amount;

        for (int i = 3; i >= 0; --i)
        {
            int coins_needed = remaining / value_coin[i];
            int coins_available = temp_coins[i];
            int coins_to_give = Math.Min(coins_needed, coins_available);

            remaining -= coins_to_give * value_coin[i];
            if (remaining <= 0) break;
        }

        return remaining == 0;
    }

    private void give_change(int amount)
    {
        int remaining = amount;
        Console.WriteLine($"Сдача: {amount} руб.");

        for (int i = 3; i >= 0; --i)
        {
            int coins_needed = remaining / value_coin[i];
            int coins_available = coins_count[i];
            int coins_to_give = Math.Min(coins_needed, coins_available);
            
            if (coins_to_give > 0)
            {
                Console.WriteLine($"{coins_to_give} монет по {value_coin[i]} руб.");
                coins_count[i] -= coins_to_give;
                remaining -= coins_to_give * value_coin[i];
            }
            if (remaining <= 0) break;
        }
        
        calculate_sum();
    }

    public void return_money()
    {
        if (insert_sum > 0)
        {
            Console.WriteLine($"Возвращено: {insert_sum} руб.");
            insert_sum = 0;
            Array.Clear(insert_mon, 0, insert_mon.Length);
        }
    }

    public int collect_money()
    {
        calculate_sum();
        int collected = summ;

        for (int i = 0; i < coins_count.Length; i++)
            coins_count[i] = 0;

        calculate_sum();

        Console.WriteLine($"Собрано: {collected} руб.");
        return collected;
    }

    public int get_user_balance() => insert_sum;
}

public class Automat
{
    private List<Product> products = new List<Product>();
    private Money money = new Money();

    public Automat()
    {
        products.Add(new Product("Кола", 90, 10));
        products.Add(new Product("Чипсы", 200, 10));
        products.Add(new Product("Вода", 40, 10));
        products.Add(new Product("Спрайт", 90, 10));
        products.Add(new Product("Сухарики", 110, 10));
        products.Add(new Product("Соломка", 40, 10));
        products.Add(new Product("Газированная вода", 40, 10));
        products.Add(new Product("Шоколадка", 150, 10));
        products.Add(new Product("Мармелад", 90, 10));
    }

    public void show_prod()
    {
        for (int i = 0; i < products.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {products[i].Name} - {products[i].Price} руб. (осталось: {products[i].Quantity} шт.)");
        }
        Console.WriteLine($"Ваш баланс: {money.get_user_balance()} руб.\n");
    }

    public void add_prod(bool admin, string name, int quantity, int price)
    {
        if (!admin) return;
        
        products.Add(new Product(name, price, quantity));
        Console.WriteLine($"Товар {name} добавлен. Цена: {price} руб., количество: {quantity} шт.");
    }

    public void buy_product(int product_index)
    {
        if (product_index < 1 || product_index > products.Count)
        {
            Console.WriteLine("Неверный номер товара.");
            return;
        }

        var product = products[product_index - 1];
        
        if (product.Quantity <= 0)
        {
            Console.WriteLine("Товар закончился.");
            return;
        }

        int result = money.buy_product(product.Price);
        if (result == 0)
        {
            product.Quantity--;
            Console.WriteLine($"Вы успешно купили {product.Name}!");
        }
    }

    public void insert_coins()
    {
        money.insert_coins();
    }

    public void return_money()
    {
        money.return_money();
    }

    public void collect_money(bool admin)
    {
        if (!admin)
        {
            Console.WriteLine("Недостаточно прав.");
            return;
        }
        money.collect_money();
    }
}

class Program
{
    static int Main(string[] args)
    {
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        Console.InputEncoding = System.Text.Encoding.GetEncoding(1251);
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        
        Automat auto_mat = new Automat();
        bool admin = false;


        string? comm = Console.ReadLine();
        while (comm != "stop")
        {
            if (comm == "product list")
            {
                auto_mat.show_prod();
            }
            else if (comm == "admin mode on")
            {
                admin = true;
                Console.WriteLine("Вы вошли в режим администратора.");
            }
            else if (comm == "admin mode off")
            {
                admin = false;
                Console.WriteLine("Вы вышли из режима администратора.");
            }
            else if (comm == "add product")
            {
                if (admin)
                {
                    Console.WriteLine("Внесите имя количество и цену.");
                    string? nme = Console.ReadLine();
                    string? cntInput = Console.ReadLine();
                    string? cstInput = Console.ReadLine();
                    int cnt = int.Parse(cntInput ?? "0");
                    int cst = int.Parse(cstInput ?? "0");
                    auto_mat.add_prod(admin, nme ?? "Unknown", cnt, cst);
                }
                else
                {
                    Console.WriteLine("Недостаточно прав.");
                }
            }
            else if (comm == "insert coins")
            {
                auto_mat.insert_coins();
            }
            else if (comm == "return money")
            {
                auto_mat.return_money();
            }
            else if (comm == "collect money")
            {
                auto_mat.collect_money(admin);
            }
            else if (comm.StartsWith("buy "))
            {
                if (int.TryParse(comm.Substring(4), out int product_num))
                {
                    auto_mat.buy_product(product_num);
                }
                else
                {
                    Console.WriteLine("Неверный формат команды buy.");
                }
            }
            else
            {
                Console.WriteLine("Команда не найдена.");
            }

            comm = Console.ReadLine();
        }

        return 0;
    }
}
