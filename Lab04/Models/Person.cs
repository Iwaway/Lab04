using Lab04.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab04.Models
{
    internal class Person
    {
        private DateTime birthday;
        private string chineseSign;
        private string sunSign;
        private bool isAdult;
        private bool isBirthday;
        private int age;
        private string birthdayInString;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Birthday
        {
            get => birthday;
            set => birthday = value;
        }
        public bool IsAdult
        {
            get => isAdult;
        }
        public bool IsBirthday
        {
            get => isBirthday;
        }
        public string ChineseSign
        {
            get => chineseSign;

        }
        public string SunSign
        {
            get => sunSign;
        }
        public string BirthdayInString
        {
            get => birthdayInString;
        }
        public int Age { get => age; set => age = value; }

        public Person(string firstName, string lastName, string email, DateTime birthday, bool isAdult, string sunSign, string chineseSign, bool isBirthday, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Birthday = birthday;
            this.isAdult = isAdult;
            this.sunSign = sunSign;
            this.chineseSign = chineseSign;
            this.isBirthday = isBirthday;
            birthdayInString = birthday.ToShortDateString();
            Age = age;
            if (!ValidateEmail(email))
            {
                throw new EmailException("Your email is incorrect");
            }
            if (age < 0)
            {
                throw new BirthInFutureException("Your age is below 0.It can't be true");
            }
            if (age > 135)
            {
                throw new BirthInFutureException("Your age is above 135.It can't be true");
            }
        }
        public static int getAge(DateTime birthday)
        {

            if (DateTime.Today.Year == birthday.Year && (DateTime.Today.Month < birthday.Month ||
                (DateTime.Today.Month == birthday.Month && DateTime.Today.Day < birthday.Day)))
                return -1;
            if (DateTime.Today.Month < birthday.Month || (DateTime.Today.Month == birthday.Month && DateTime.Today.Day < birthday.Day))
            {
                return DateTime.Today.Year - birthday.Year - 1;
            }
            return DateTime.Today.Year - birthday.Year;
        }
        public static string CalculateSunSign(DateTime birthday)
        {
            if (birthday.Day >= 21 && birthday.Month == 1 ||
                birthday.Day <= 19 && birthday.Month == 2)
            {
                return SunSigns.Aquarius.ToString();
            }
            if (birthday.Day >= 20 && birthday.Month == 2 ||
               birthday.Day <= 20 && birthday.Month == 3)
            {
                return SunSigns.Pisces.ToString();
            }
            if (birthday.Day >= 21 && birthday.Month == 3 ||
               birthday.Day <= 20 && birthday.Month == 4)
            {
                return SunSigns.Aries.ToString();
            }
            if (birthday.Day >= 21 && birthday.Month == 4 ||
               birthday.Day <= 20 && birthday.Month == 5)
            {
                return SunSigns.Taurus.ToString();
            }
            if (birthday.Day >= 21 && birthday.Month == 5 ||
               birthday.Day <= 21 && birthday.Month == 6)
            {
                return SunSigns.Gemini.ToString();
            }
            if (birthday.Day >= 22 && birthday.Month == 6 ||
               birthday.Day <= 22 && birthday.Month == 7)
            {
                return SunSigns.Cancer.ToString();
            }
            if (birthday.Day >= 23 && birthday.Month == 7 ||
               birthday.Day <= 22 && birthday.Month == 8)
            {
                return SunSigns.Leo.ToString();
            }
            if (birthday.Day >= 23 && birthday.Month == 8 ||
               birthday.Day <= 22 && birthday.Month == 9)
            {
                return SunSigns.Virgo.ToString();
            }
            if (birthday.Day >= 23 && birthday.Month == 9 ||
               birthday.Day <= 22 && birthday.Month == 10)
            {
                return SunSigns.Libra.ToString();
            }
            if (birthday.Day >= 23 && birthday.Month == 10 ||
               birthday.Day <= 22 && birthday.Month == 11)
            {
                return SunSigns.Scorpio.ToString();
            }
            if (birthday.Day >= 23 && birthday.Month == 11 ||
               birthday.Day <= 21 && birthday.Month == 12)
            {
                return SunSigns.Saggitarius.ToString();
            }
            return SunSigns.Capricorn.ToString();

        }
        public static string CalculateChineseSign(DateTime birthday)
        {
            int tmp = birthday.Year % 12;
            switch (tmp)
            {
                case 0:
                    return ChineseSigns.Monkey.ToString();
                case 1:
                    return ChineseSigns.Rooster.ToString();
                case 2:
                    return ChineseSigns.Dog.ToString();
                case 3:
                    return ChineseSigns.Pig.ToString();
                case 4:
                    return ChineseSigns.Rat.ToString();
                case 5:
                    return ChineseSigns.Ox.ToString();
                case 6:
                    return ChineseSigns.Tiger.ToString();
                case 7:
                    return ChineseSigns.Rabbit.ToString();
                case 8:
                    return ChineseSigns.Dragon.ToString();
                case 9:
                    return ChineseSigns.Snake.ToString();
                case 10:
                    return ChineseSigns.Horse.ToString();
                default:
                    return ChineseSigns.Goat.ToString();
            }
        }
        private bool ValidateEmail(string email)
        {
            Regex regex = new Regex(@"(\w+)@(\w+)\.(\w+)");
            return regex.IsMatch(email);
        }
        public static bool CalculateIsAdult(int age)
        {
            return age >= 18;
        }
        public static bool CalculateIsBirthday(DateTime birthday)
        {
            return birthday.Day == DateTime.Today.Day && birthday.Month == DateTime.Today.Month;
        }
    }

    enum SunSigns
    {
        Aries,
        Taurus,
        Gemini,
        Cancer,
        Leo,
        Virgo,
        Libra,
        Scorpio,
        Saggitarius,
        Capricorn,
        Aquarius,
        Pisces,
    }
    enum ChineseSigns
    {
        Monkey,
        Rooster,
        Dog,
        Pig,
        Rat,
        Ox,
        Tiger,
        Rabbit,
        Dragon,
        Snake,
        Horse,
        Goat,
    }
}
