using System.Text.Json;

namespace Prog_Lab1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Date date2 = new Date(10, 6, 2020);
            Date date3 = new Date(16, 10, 2021);
            date2.DayDifference(date3);
            date3.RealTimeDifference();
            date3.DayOfTheWeek();
            SaveJson(date3);
            LoadJson(date3);
        }

        public static void SaveJson(Date date)
        {
            
            FileStream fs = new FileStream($"{date.GetDay()}.{date.GetMonth()}.{date.GetYear()}.json", FileMode.Create);
            JsonSerializer.SerializeAsync(fs, date);
            Console.WriteLine($"{date.GetDay()}.{date.GetMonth()}.{date.GetYear()}.json збережено");
            fs.Close();
        }

        public static void LoadJson(Date date)
        {
            FileStream fs = new FileStream($"{date.GetDay()}.{date.GetMonth()}.{date.GetYear()}.json", FileMode.Open);
            Date? date1 = JsonSerializer.Deserialize<Date>(fs);
            Console.WriteLine($"{date1.GetDay()}.{date1.GetMonth()}.{date1.GetYear()}");
        }

    }

    class Date
    {
        private int _day, _month, _year;
        public int Month
        {
            set => _month = Math.Clamp(value, 1, 12); 
            get => _month;
        }
        public int Day
        {
            set
            {
                if (Month == 2)
                    _day = Math.Clamp(value, 1, 28);
                else if (Month % 2 == 1 || Month == 8)
                    _day = Math.Clamp(value, 1, 31);
                else
                    _day = Math.Clamp(value, 1, 30);
            }
            get => _day;
        }
        public int Year
        {
            set => _year = value; 
            get => _year;
        }
        public Date(int day, int month, int year)
        {
            Year = year;
            Month = month;
            Day = day;
        }
        public int GetDay()
        {
            return Day;
        }
        public int GetMonth()
        {
            return Month;
        }
        public int GetYear()
        {
            return Year;
        }
        public void DayDifference(Date date)
        {
            int days = 0;
            for(int i = 0;i<Math.Abs(GetYear() - date.GetYear());i++) 
                days += 28 + 30 * 4 + 31 * 7;
            for (int i = Math.Min(date.GetMonth(), GetMonth()); i < Math.Max(date.GetMonth(), GetMonth()); i++)
            {
                int j;
                if (GetMonth() > date.GetMonth() && GetYear() < date.GetYear() ||
                    GetMonth() < date.GetMonth() && GetYear() > date.GetYear())
                    j = -1;
                else
                    j = 1;
                if (i == 2)
                    days += 28*j;
                else if (i % 2 == 1 || i == 8)
                    days += 31*j;
                else
                    days += 30*j;
            }
            days += Math.Abs(date.GetDay() - GetDay());
            Console.WriteLine($"Рiзниця мiж {GetDay()}.{GetMonth()}.{GetYear()} i {date.GetDay()}.{date.GetMonth()}" +
                              $".{date.GetYear()} - {days} днiв");
        }
        public void RealTimeDifference()
        {
            int days = 0;
            int month = 0;
            for (int i = 0; i < Math.Abs(GetYear() - DateTime.Today.Year); i++)
            {
                days += 28 + 30 * 4 + 31 * 7;
                month += 12;
            }
            for (int i = Math.Min(DateTime.Today.Month, GetMonth()); i < Math.Max(DateTime.Today.Month, GetMonth()); i++)
            {
                int j;
                if (GetMonth() > DateTime.Today.Month && GetYear() < DateTime.Today.Year ||
                    GetMonth() < DateTime.Today.Month && GetYear() > DateTime.Today.Year)
                {
                    j = -1;
                }
                else
                    j = 1;
                if (i == 2)
                    days += 28*j;
                else if (i % 2 == 1 || i == 8)
                    days += 31*j;
                else
                    days += 30*j;
                month+=j;
            }
            days += DateTime.Today.Day - GetDay();
            days++;
            Console.WriteLine($"Рiзниця мiж {GetDay()}.{GetMonth()}.{GetYear()} i {DateTime.Today.Day}.{DateTime.Today.Month}" +
                              $".{DateTime.Today.Year} - {days} днiв({month} мiсяцiв)");
        }
        public void DayOfTheWeek()
        {
            Console.WriteLine($"{GetDay()}.{GetMonth()}.{GetYear()} - {new DateTime(GetYear(), GetMonth(), GetDay()).DayOfWeek.ToString()}");
        }
    }
    
}