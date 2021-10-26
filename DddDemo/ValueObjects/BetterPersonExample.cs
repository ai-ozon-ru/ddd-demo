using System;
using System.Collections.Generic;

namespace DddDemo.ValueObjects
{
    public class BetterPersonExample
    {
        private readonly PersonName _name;
        private readonly PhoneNumber _landlinePhone;
        private readonly PhoneNumber _mobilePhone;
        private readonly Address _address;
        private readonly Email _email;
        private readonly Height _heightMetric;
        private readonly ClothingSize _clothingSize;

        public BetterPersonExample(
            PersonName name,
            PhoneNumber landlinePhone,
            PhoneNumber mobilePhone,
            Address address,
            Email email,
            Height heightMetric,
            ClothingSize clothingSize)
        {
            _name = name;
            _landlinePhone = landlinePhone;
            _mobilePhone = mobilePhone;
            _address = address;
            _email = email;
            _heightMetric = heightMetric;
            _clothingSize = clothingSize;
        }

    }

    #region Value objects

    public class PhoneNumber
    {
        public PhoneNumber(string value)
            => Value = value;

        public string Value { get; }

        public static PhoneNumber Parse(string number)
        {
            // Do some parsing logic
            return new (number);
        }
    }

    public class Email
    {
        public Email(string value)
            => Value = value;

        public string Value { get; }

        public static Email Parse(string number)
        {
            // Do some parsing logic
            return new (number);
        }
    }

    public class Height
    {
        public double Centimeters { get; }

        private Height(double centimeters) => Centimeters = centimeters;

        public static Height FromMetrics(double centimeters)
        {
            if (centimeters <= 0 || centimeters > 250)
            {
                throw new Exception("Incorrect height");
            }

            return new Height(centimeters);
        }

        public static Height FromImperial(int feet, int inches)
        {
            // TODO validation
            return new ((feet * 12 + inches) * 2.54);
        }
    }

    public class ClothingSize : Enumeration
    {
        // https://lostechies.com/jimmybogard/2008/08/12/enumeration-classes/
        // https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types

        public static ClothingSize XS =  new(1, nameof(XS), "Extra small");
        public static ClothingSize S =   new(2, nameof(S), "Small");
        public static ClothingSize M =   new(3, nameof(M), "Medium");
        public static ClothingSize L =   new(4, nameof(L), "Large");
        public static ClothingSize XL =  new(5, nameof(XL), "Extra large");
        public static ClothingSize XXL = new(6, nameof(XXL), "Extra extra large");

        public string Description { get; }

        /// <inheritdoc />
        public ClothingSize(int id, string name, string description) : base(id, name)
            => Description = description;

        public ClothingSize Parse(string size)
            => size?.ToUpper() switch
                {
                    "XS"  => XS,
                    "S"   => S,
                    "M"   => M,
                    "L"   => L,
                    "XL"  => XL,
                    "XXL" => XXL,
                    _ => throw new Exception("Unknown size")
                };
    }

    public sealed class Address : ValueObject
    {
        public string Street { get; }
        public string City { get; }
        public string State { get; }
        public string Country { get; }
        public string ZipCode { get; }

        public Address(string street, string city, string state, string country, string zipcode)
        {
            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipcode;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return City;
            yield return State;
            yield return Country;
            yield return ZipCode;
        }
    }

    public record PersonName
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }

        private PersonName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public static PersonName Create(string firstName, string lastName)
        {
            return new PersonName(firstName, lastName);
        }

        /*
         *  Проблемы:
         *  1. Не реализует IComparable
         *      var ericEvans = new PersonName("Eric", "Evans");
         *      var martinFowler = new PersonName("Martin", "Fowler");
         *      var names = new[] { ericEvans, martinFowler }.OrderBy(x => x).ToArray(); <- не работает
         *
         *  2. Если нужна валидация, то надо делать либо конструктор, либо фабричный метод
         *      Пропадает лаконичность записи
         *
         *  3. Нет контроля при сравнении объектов
         *
         *  4. Коллекции нельзя сравнивать по значению
         *      var ericEvans1 = new PersonName("Eric", "Evans", new [] { "Mr", "Dr", });
         *      var ericEvans2 = new PersonName("Eric", "Evans", new [] { "Mr", "Dr", });
         *      var result = ericEvans1 == ericEvans2;  <- false
         */
    }

    #endregion

}
