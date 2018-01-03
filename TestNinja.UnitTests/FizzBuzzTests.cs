using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class FizzBuzzTests
    {
        [Test]
        [TestCase(9)]
        [TestCase(12)]
        [TestCase(27)]
        public void GetOutput_NumberIsDivisibleBy3Only_ShouldReturnFizz(int number)
        {
            var result = FizzBuzz.GetOutput(number);
            
            Assert.That(result, Is.EqualTo("Fizz"));
        }

        [Test]
        [TestCase(5)]
        [TestCase(20)]
        [TestCase(35)]
        public void GetOutput_NumberIsDivisibleBy5Only_ShouldReturnBuzz(int number)
        {
            var result = FizzBuzz.GetOutput(number);
            
            Assert.That(result, Is.EqualTo("Buzz"));
        }
        
        [Test]
        [TestCase(15)]
        [TestCase(30)]
        [TestCase(45)]
        public void GetOutput_NumberIsDivisibleBy3And5_ShouldReturnFizzBuzz(int number)
        {
            var result = FizzBuzz.GetOutput(number);
            
            Assert.That(result, Is.EqualTo("FizzBuzz"));
        }
        
        [Test]
        [TestCase(1)]
        [TestCase(4)]
        [TestCase(7)]
        public void GetOutput_NumberNotDivisibleBy3Or5_ShouldReturnNumber(int number)
        {
            var result = FizzBuzz.GetOutput(number);
            
            Assert.That(result, Is.EqualTo(number.ToString()));
        }
    }
}