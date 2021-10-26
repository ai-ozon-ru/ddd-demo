namespace DddDemo.ValueObjects
{
    public class Person
    {
        private readonly string _firstName;
        private readonly string _lastName;
        private readonly string _landlinePhone;
        private readonly string _mobilePhone;
        private readonly string _email;
        private readonly int _heightMetric;
        private readonly string _clothingSize;

        public Person(
            string firstName,
            string lastName,
            string landlinePhone,
            string mobilePhone,
            string email,
            int heightMetric,
            string clothingSize)
        {
            _firstName = firstName;
            _lastName = lastName;
            _landlinePhone = landlinePhone;
            _mobilePhone = mobilePhone;
            _email = email;
            _heightMetric = heightMetric;
            _clothingSize = clothingSize;
        }
    }
}
