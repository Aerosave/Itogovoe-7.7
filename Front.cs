using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 namespace ShopOrder
{
    public class Front
    {
       public static void Main()
        {
            Console.WriteLine("Укажите тип доставки (на дом - введите 1, почтомат - введите 2, в магазин - введите 3):");
            string deliveryType = Console.ReadLine().ToLower();
            Delivery delivery;

            switch (deliveryType)
            {
                case "1":
                    Console.WriteLine("Введите адрес доставки:");
                    string homeAddress = Console.ReadLine();
                    delivery = new HomeDelivery(DateTime.Now.AddDays(2));
                    ((HomeDelivery)delivery).SetAddress(homeAddress);
                    break;
                case "2":
                    delivery = new PickPointDelivery("Улица Почтовая, дом 17", "Почтомат номер_17", "Почта России");
                    break;
                case "3":
                    delivery = new ShopDelivery("Улица Торговая, дом 67", "Торговый дом");
                    break;
                default:
                    Console.WriteLine("Введен неверный способ доставки");
                    return;
            }

            Console.WriteLine("Введите комментарий к заказу от покупателя:");
            string description = Console.ReadLine();

            var order = new Order<Delivery>(delivery, description);

            bool addingProducts = true;
            while (addingProducts)
            {
                Console.WriteLine("Введите имя продукта (или '0', что бы оформить заказ):");
                string productName = Console.ReadLine();
                if (productName.ToLower() == "0")
                {
                    addingProducts = false;
                    continue;
                }

                Console.WriteLine($"Введите цену продукта {productName}:");
                double productPrice = double.Parse(Console.ReadLine());

                order.AddProduct(new Product(productName, productPrice));
            }

            order.DisplayOrderDetails();
            Console.WriteLine("Итоговая сумма: " + order.CalculateTotalPrice());
            Console.WriteLine("Номер заказа: " + order.Number);
        }
    }

}     
