using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace IGI_lab1
{
    class DBInitialization
    {
        public static void Initialize(CallContext db)
        {
            db.Database.EnsureCreated();

            // Проверка занесены ли виды топлива
            if (db.Calls.Any())
            {
                return;   // База данных инициализирована
            }

            int carModelNumber = 10;
            int carNumber = 15;
            int callsNumber = 40;
            decimal middleValue = 5000;
            decimal middleValueOfTariff = 50;
            DateTime middleDate = new DateTime(2000, 1, 1);

            Random MyRandom = new Random(DateTime.Now.Millisecond);

            string[] models = { "Audi", "BMW", "Citroen", "Dadi", "Daihatsu", "Dodge", "Eagle" };
            for (int i = 0; i < carModelNumber; i++)
                db.CarModels.Add(new CarModel { ModelName = models[MyRandom.Next(0, models.Length)], Specifications = "none", Cost = (middleValue + MyRandom.Next(-500, 500)) });
            db.SaveChanges();

            string[] tariffs = { "Стандарт", "Универсал", "Минивен", "Элит", "Доставка продуктов" };

            foreach (string currentName in tariffs)
                db.Tariffs.Add(new Tariff { TariffName = currentName, Description = "none", Cost = (middleValueOfTariff + MyRandom.Next(-10, 10)) });
            db.SaveChanges();

            string[] Names = { "Александр", "Алексей", "Анатолий", "Андрей", "Антон", "Антонин", "Аристарх", "Богдан", "Борис", "Вадим" };
            string[] Familis = { "Кириллов", "Киселёв", "Князев", "Ковалёв", "Козлов", "Мишин", "Моисеев", "Молчанов", "Морозов" };

            string[] Streets = { "А.Авакяна","Газарос","Адама","Шемеша","Азгура","Азизова","Айвазовского","Академика","Жебрака","Купревича","Фёдорова"};

            for (int i = 0; i <carNumber; i++)
            {
                string mech = Names[MyRandom.Next(0, Names.Length)] + " " + Familis[MyRandom.Next(0, Familis.Length)];
                string driver = Names[MyRandom.Next(0, Names.Length)] + " " + Familis[MyRandom.Next(0, Familis.Length)];
                db.Cars.Add(new Car { Number = i.ToString(),
                    Driver = driver,
                    Mechanic = mech,
                    CarModelId = MyRandom.Next(1, carModelNumber - 1),
                    Mileage = MyRandom.Next(20, 9999),
                    YearCreation = middleDate.AddYears(MyRandom.Next(-5, 5)),
                    LastTO = DateTime.Now.Date.AddDays(-MyRandom.Next(10, 130))
                });
            }
            db.SaveChanges();

            for (int i = 0; i < callsNumber; i++)
            {
                string disp = Names[MyRandom.Next(0, Names.Length)] + " " + Familis[MyRandom.Next(0, Familis.Length)];
                db.Calls.Add(new Call
                {
                    Date = DateTime.Now.Date.AddDays(-MyRandom.Next(0, 130)),
                    Number = "+37529" + MyRandom.Next(1000000, 9999999).ToString(),
                    Route = "ул." + Streets[MyRandom.Next(0, Streets.Length)] + "-ул." + Streets[MyRandom.Next(0, Streets.Length)],
                    Dispatcher = disp,
                    CarId= MyRandom.Next(1, carNumber - 1),
                    TariffId= MyRandom.Next(1, tariffs.Length - 1)
                });
            }
            db.SaveChanges();
        }

    }
}
