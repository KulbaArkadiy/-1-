using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Linq;

namespace IGI_lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (CallContext db = new CallContext())
            {
                DBInitialization.Initialize(db);
                Console.WriteLine("====== Выборки данных ========");
                Select(db);
                Console.WriteLine("\n====== Вставка данных ========");
                Insert(db);
                Console.WriteLine("\n====== Удаление данных ========");
                Delete(db);
                Console.WriteLine("\n====== Обновление данных ========");
                Update(db);
            }
            Console.ReadKey();
        }

        static void Print(string sqltext, IEnumerable items)
        {
            Console.WriteLine("\nНажмите пробел для выполнения след. шага");
            Console.ReadKey();
            Console.WriteLine("\n" + sqltext);
            foreach (var item in items)
            {
                Console.WriteLine(item.ToString());
            }
        }
        static void Select(CallContext db)
        {
            var queryLINQ1 = from f in db.CarModels
                             select new
                             {
                                 f.Id,
                                 Название = f.ModelName,
                                 Спецификация = f.Specifications,
                                 Стоимость = f.Cost
                             };
            

            string comment = "1. Выборка всех данных из таблицы Марки стоящей на стороне отношения 'один':";
            Print(comment, queryLINQ1.ToList());

            var queryLINQ2 = from f in db.CarModels
                             where (f.Cost>5100)
                             select new
                             {
                                 f.Id,
                                 Название = f.ModelName,
                                 Спецификация = f.Specifications,
                                 Стоимость = f.Cost
                             };
            comment = "2. Выборка всех данных из таблицы Марки стоимость которых больше 5100:";
            Print(comment, queryLINQ2.ToList());

            var queryLINQ3 = from f in db.Cars
                             group f by f.CarModelId into gr
                             join t in db.CarModels
                             on gr.Key equals t.Id
                             orderby t.ModelName descending
                             select new
                             {
                                 Марка = t.ModelName,
                                 Стоимость = t.Cost,
                                 Колво_машин_этой_марки=gr.Count()
                             };
            comment = "3. Вывод информации о том сколько машин каждой марки имеются в автопарке:";
            Print(comment, queryLINQ3.ToList());


            var queryLINQ4 = from f in db.Cars
                             join t in db.CarModels
                             on f.CarModelId equals t.Id
                             orderby t.ModelName descending
                             select new
                             {
                                 Марка = t.ModelName,
                                 Стоимость = t.Cost,
                                 Год_выпуска= f.YearCreation.Year,
                                 Пробег = f.Mileage,
                                 Механик = f.Mechanic,
                                 Водитель = f.Driver
                             };
            comment = "4. Вывод информации о всех машинах в автопарке (для примера выведены только 10 записей):";
            //для примера выведем только 10
            Print(comment, queryLINQ4.Take(10).ToList());

            var queryLINQ5 = from f in db.Cars
                             where f.YearCreation.Year>1997
                             join t in db.CarModels
                             on f.CarModelId equals t.Id
                             orderby t.ModelName descending
                             select new
                             {
                                 Марка = t.ModelName,
                                 Стоимость = t.Cost,
                                 Год_выпуска = f.YearCreation.Year,
                                 Пробег = f.Mileage,
                                 Механик = f.Mechanic,
                                 Водитель = f.Driver
                             };
            comment = "5. Вывод информации о всех машинах в автопарке выпущеных после 1997 года:";
            Print(comment, queryLINQ5.ToList());
        }
        static void Insert(CallContext db)
        {
            CarModel carModel = new CarModel
            {
                ModelName="Porshe",
                Specifications="Elit",
                Cost=6800
            };
            db.CarModels.Add(carModel);//добавление новой марки машины
            db.SaveChanges();

            var queryLINQ1 = from f in db.CarModels
                             select new {
                                 f.Id,
                                 Название = f.ModelName,
                                 Спецификация = f.Specifications,
                                 Стоимость = f.Cost
                             };

            string comment = "6. Выборка данных из таблицы Марки с добавленной маркой 'Porshe':";
            Print(comment, queryLINQ1.ToList());

            Car car = new Car
            {
                Number = "EAl2046-H",
                YearCreation = new DateTime(2010, 1, 1),
                Mileage = 100,
                Driver = "Игорь Евланов",
                Mechanic = "Станислав Рубин",
                LastTO = DateTime.Today.Date,
                CarModelId = carModel.Id
            };
            db.Cars.Add(car);
            db.SaveChanges();

            var queryLINQ2 = from f in db.Cars
                             where f.YearCreation.Year > 2000
                             join t in db.CarModels
                             on f.CarModelId equals t.Id
                             orderby t.ModelName descending
                             select new
                             {
                                 Марка = t.ModelName,
                                 Стоимость = t.Cost,
                                 Год_выпуска = f.YearCreation.Year,
                                 Пробег = f.Mileage,
                                 Механик = f.Mechanic,
                                 Водитель = f.Driver
                             };
            comment = "7. Выборка данных из таблицы Машины, с годом выпуска после 2000 с добавленной машиной 'Porshe':";
            Print(comment, queryLINQ2.ToList());
        }
        static void Delete(CallContext db)
        {
            string modelName = "Porshe";
            var model = db.CarModels.Where(m => m.ModelName == modelName);
            var cars = db.Cars.Include("CarModel").Where(c => c.CarModel.ModelName == modelName);

            db.Cars.RemoveRange(cars);
            db.SaveChanges();

            var queryLINQ1 = from f in db.Cars
                             where f.YearCreation.Year > 2000
                             join t in db.CarModels
                             on f.CarModelId equals t.Id
                             orderby t.ModelName descending
                             select new
                             {
                                 Марка = t.ModelName,
                                 Стоимость = t.Cost,
                                 Год_выпуска = f.YearCreation.Year,
                                 Пробег = f.Mileage,
                                 Механик = f.Mechanic,
                                 Водитель = f.Driver
                             };
            string comment = "8. Выборка данных из таблицы Машины, с годом выпуска после 2000 после удаления машины 'Porshe':";
            Print(comment, queryLINQ1.ToList());

            db.CarModels.RemoveRange(model);
            db.SaveChanges();

            var queryLINQ2 = from f in db.CarModels
                             select new
                             {
                                 f.Id,
                                 Название = f.ModelName,
                                 Спецификация = f.Specifications,
                                 Стоимость = f.Cost
                             };

            comment = "9. Выборка данных из таблицы Марки после удаления марки 'Porshe':";
            Print(comment, queryLINQ2.ToList());
        }
        static void Update(CallContext db)
        {
            string modelName = "BMW";
            var queryLINQ1 = from f in db.CarModels
                             where f.ModelName == modelName
                             select new
                             {
                                 f.Id,
                                 Название = f.ModelName,
                                 Спецификация = f.Specifications,
                                 Стоимость = f.Cost
                             };


            string comment = "10. Выборка всех данных из таблицы Марки с названием марки BMW:";
            Print(comment, queryLINQ1.ToList());

            var bmwModels = from f in db.CarModels
                             where f.ModelName == modelName
                             select f;

            foreach (CarModel model in bmwModels)
                model.Cost += 1000;
            db.SaveChanges();

            comment = "11. Выборка всех данных из таблицы Марки с названием марки BMW с увеличенной на 1000 стоимостью:";
            Print(comment, queryLINQ1.ToList());
        }
    }
}
