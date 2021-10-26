namespace DddDemo.ValueObjects
{
    public sealed class Demo
    {
        #region Value Object

        /*
         *  1. Измеряет, оценивает или описывает доменный объект
            2. Можно считать неизменяемым
            3. Нечто целостное, объединяет связанные атрибуты в единое целое
            4. Можно сравнивать по значению
         */

        #endregion

        #region Value object example

        public void CreatePerson()
        {
            var person = new Person(
                "John",
                "Smith",
                "555223344",
                "555887766",
                "john@smith.com",
                180,
                "XL");
        }

        #endregion

        #region Better value object example

        public void CreateBetterPerson()
        {
            var betterPerson = new BetterPersonExample(
                PersonName.Create("John", "Smith"),
                PhoneNumber.Parse("555223344"),
                PhoneNumber.Parse("555887766"),
                new Address("Street, 1", "Moscow", "Moscow", "Russia", "127001"),
                Email.Parse("john@smith.com"),
                Height.FromMetrics(180),
                ClothingSize.XL);
        }

        #endregion
    }
}
