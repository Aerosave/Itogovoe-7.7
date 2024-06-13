
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopOrder
{
    /// <summary>
    /// Абстрактный класс Delivery с инкапсулированным полем Address и свойством для доступа к нему
    /// </summary>
    public abstract class Delivery
    {
        private string protected_address;
        public string Address
        {
            get { return protected_address; }
            protected set { protected_address = value; }
        }

        protected Delivery(string address)
        {
            protected_address = address;
        }

        public abstract void DisplayInfo();
    }

    /// <summary>
    /// Класс HomeDelivery с переопределением метода DisplayInfo
    /// </summary>
    public class HomeDelivery : Delivery
    {
        public DateTime EstimatedDeliveryTime { get; private set; }

        public HomeDelivery(DateTime estimatedDeliveryTime) : base(null)
        {
            EstimatedDeliveryTime = estimatedDeliveryTime;
        }

        public void SetAddress(string address)
        {
            Address = address;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Доставка на дом. Адрес: {Address}, Ожидаемое время доставки: {EstimatedDeliveryTime}");
        }
    }

    /// <summary>
    /// Класс PickPointDelivery с переопределением метода DisplayInfo
    /// </summary>
    public class PickPointDelivery : Delivery
    {
        public string PickPointName { get; private set; }
        public string Company { get; private set; }

        public PickPointDelivery(string address, string pickPointName, string company) : base(address)
        {
            PickPointName = pickPointName;
            Company = company;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Пункт выдачи. Адрес: {Address}, Почтомат: {PickPointName}, Компания: {Company}");
        }
    }

    /// <summary>
    /// Класс ShopDelivery с переопределением метода DisplayInfo
    /// </summary>
    public class ShopDelivery : Delivery
    {
        public string ShopName { get; private set; }

        public ShopDelivery(string address, string shopName) : base(address)
        {
            ShopName = shopName;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Доставка в магазин. Адрес магазина: {Address}, Название магазина: {ShopName}");
        }
    }

    /// <summary>
    /// Класс Product
    /// </summary>
    public class Product
    {
        public string Name { get; private set; }
        public double Price { get; private set; }

        public Product(string name, double price)
        {
            Name = name;
            Price = price;
        }
    }

    /// <summary>
    /// Класс Order с использованием индексатора и перегруженных операторов
    /// </summary>
    /// <typeparam name="TDelivery"></typeparam>
    public class Order<TDelivery> where TDelivery : Delivery
    {
       
        private List<Product> indProducts = new List<Product>();

        public TDelivery Delivery { get; private set; }
        public string Number { get; private set; }
        public string Description { get; set; }

        public Order(TDelivery delivery, string description)
        {
            Delivery = delivery;
            Description = description;
            Number = GenerateOrderNumber();
        }
        /// Метод генерации номера заказа
        private string GenerateOrderNumber()
        {
            var random = new Random();
            var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var builder = new StringBuilder();

            // Генерация трех случайных букв
            for (int i = 0; i < 3; i++)
            {
                var letterIndex = random.Next(0, letters.Length);
                builder.Append(letters[letterIndex]);
            }

            builder.Append('-');

            // Генерация пяти случайных цифр
            for (int i = 0; i < 5; i++)
            {
                builder.Append(random.Next(0, 10).ToString());
            }

            return builder.ToString();
        }
        public void AddProduct(Product product)
        {
            indProducts.Add(product);
        }

        public void DisplayOrderDetails()
        {
            Console.WriteLine($"Заказ №{Number}: {Description}");
            Delivery.DisplayInfo();
            Console.WriteLine("Товары в заказе:");
            foreach (var product in indProducts)
            {
                Console.WriteLine($"{product.Name}, цена: {product.Price}");
            }
        }

        // Индексатор для доступа к товарам
        public Product this[int index]
        {
            get 
            {
                return indProducts[index]; 
            }
            set 
            { 
                indProducts[index] = value; 
            }
        }

        // Метод подсчета общей суммы заказа
        public double CalculateTotalPrice()
        {
            double total = 0;
            foreach (var product in indProducts)
            {
                total += product.Price;
            }
            return total;
        } 
        // Перегрузка оператора ==
        public static bool operator ==(Order<TDelivery> a, Order<TDelivery> b)
        {
            return a.Number == b.Number;
        }

        // Перегрузка оператора !=
        public static bool operator !=(Order<TDelivery> a, Order<TDelivery> b)
        {
            return a.Number != b.Number;
        }

    }

}
