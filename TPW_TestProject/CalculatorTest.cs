namespace TPW_TestProject
{
    public class Tests
    {
        [Test]
        public void TestDodawania()
        {
            double num1 = 5;
            double num2 = 3;
            char operation = '+';

            double expectedResult = 8;

            double result = SimpleCalculator.Calculate(num1, num2, operation);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void TestOdejmowania()
        {
            double num1 = 10;
            double num2 = 4;
            char operation = '-';

            double expectedResult = 6;

            double result = SimpleCalculator.Calculate(num1, num2, operation);

            Assert.AreEqual(expectedResult, result);
        }
    }
}