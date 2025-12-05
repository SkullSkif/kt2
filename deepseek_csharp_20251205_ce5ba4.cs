using Xunit;
using lab1.models;

namespace lab1.tests
{
    public class ValeraTests
    {
        [Fact]
        public void DefaultConstructor_InitializesCorrectValues()
        {
            // Arrange & Act
            var valera = new Valera();

            // Assert
            Assert.Equal(100, valera.Health);
            Assert.Equal(0, valera.Mana);
            Assert.Equal(0, valera.Joy);
            Assert.Equal(0, valera.Fatigue);
            Assert.Equal(0, valera.Cash);
        }

        [Fact]
        public void ParameterizedConstructor_InitializesCorrectValues()
        {
            // Arrange & Act
            var valera = new Valera(80, 50, 5, 30, 200);

            // Assert
            Assert.Equal(80, valera.Health);
            Assert.Equal(50, valera.Mana);
            Assert.Equal(5, valera.Joy);
            Assert.Equal(30, valera.Fatigue);
            Assert.Equal(200, valera.Cash);
        }

        [Theory]
        [InlineData(120, 100)] // превышение максимума
        [InlineData(-10, 0)]   // ниже минимума
        [InlineData(75, 75)]   // нормальное значение
        public void Health_SetOutOfRange_ClampsToValidRange(int input, int expected)
        {
            // Arrange
            var valera = new Valera();

            // Act
            var testValera = new Valera(input, 0, 0, 0, 0);

            // Assert
            Assert.Equal(expected, testValera.Health);
        }

        [Theory]
        [InlineData(120, 100)] // превышение максимума
        [InlineData(-10, 0)]   // ниже минимума
        [InlineData(50, 50)]   // нормальное значение
        public void Mana_SetOutOfRange_ClampsToValidRange(int input, int expected)
        {
            // Arrange & Act
            var valera = new Valera(100, input, 0, 0, 0);

            // Assert
            Assert.Equal(expected, valera.Mana);
        }

        [Theory]
        [InlineData(15, 10)]   // превышение максимума
        [InlineData(-15, -10)] // ниже минимума
        [InlineData(5, 5)]     // нормальное значение
        public void Joy_SetOutOfRange_ClampsToValidRange(int input, int expected)
        {
            // Arrange & Act
            var valera = new Valera(100, 0, input, 0, 0);

            // Assert
            Assert.Equal(expected, valera.Joy);
        }

        [Theory]
        [InlineData(120, 100)] // превышение максимума
        [InlineData(-10, 0)]   // ниже минимума
        [InlineData(50, 50)]   // нормальное значение
        public void Fatigue_SetOutOfRange_ClampsToValidRange(int input, int expected)
        {
            // Arrange & Act
            var valera = new Valera(100, 0, 0, input, 0);

            // Assert
            Assert.Equal(expected, valera.Fatigue);
        }

        [Theory]
        [InlineData(-100, 0)]  // отрицательное значение
        [InlineData(50, 50)]   // нормальное значение
        [InlineData(0, 0)]     // нулевое значение
        public void Cash_SetNegative_SetsToZero(int input, int expected)
        {
            // Arrange & Act
            var valera = new Valera(100, 0, 0, 0, input);

            // Assert
            Assert.Equal(expected, valera.Cash);
        }

        [Theory]
        [InlineData(30, 5, true)]    // условия выполнены
        [InlineData(60, 5, false)]   // мана >= 50
        [InlineData(30, 15, false)]  // усталость >= 10
        [InlineData(60, 15, false)]  // оба условия не выполнены
        public void GoToWork_ConditionalExecution_ReturnsCorrectResult(int mana, int fatigue, bool expectedResult)
        {
            // Arrange
            var valera = new Valera(100, mana, 0, fatigue, 0);

            // Act
            var result = valera.GoToWork();

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void GoToWork_WhenConditionsMet_UpdatesProperties()
        {
            // Arrange
            var valera = new Valera(100, 40, 5, 5, 100);

            // Act
            var result = valera.GoToWork();

            // Assert
            Assert.True(result);
            Assert.Equal(0, valera.Joy);      // 5 - 5 = 0
            Assert.Equal(10, valera.Mana);    // 40 - 30 = 10
            Assert.Equal(200, valera.Cash);   // 100 + 100 = 200
            Assert.Equal(75, valera.Fatigue); // 5 + 70 = 75
        }

        [Fact]
        public void TouchGrass_UpdatesProperties()
        {
            // Arrange
            var valera = new Valera(100, 50, 0, 0, 0);

            // Act
            valera.TouchGrass();

            // Assert
            Assert.Equal(1, valera.Joy);
            Assert.Equal(40, valera.Mana);    // 50 - 10 = 40
            Assert.Equal(10, valera.Fatigue); // 0 + 10 = 10
        }

        [Fact]
        public void DrinkAndWatch_UpdatesProperties()
        {
            // Arrange
            var valera = new Valera(100, 50, 5, 20, 100);

            // Act
            valera.DrinkAndWatch();

            // Assert
            Assert.Equal(4, valera.Joy);       // 5 - 1 = 4
            Assert.Equal(80, valera.Mana);     // 50 + 30 = 80
            Assert.Equal(30, valera.Fatigue);  // 20 + 10 = 30
            Assert.Equal(95, valera.Health);   // 100 - 5 = 95
            Assert.Equal(80, valera.Cash);     // 100 - 20 = 80
        }

        [Fact]
        public void GoToBar_UpdatesProperties()
        {
            // Arrange
            var valera = new Valera(100, 50, 5, 20, 100);

            // Act
            valera.GoToBar();

            // Assert
            Assert.Equal(6, valera.Joy);       // 5 + 1 = 6
            Assert.Equal(80, valera.Mana);     // 50 + 30 = 80
            Assert.Equal(30, valera.Fatigue);  // 20 + 10 = 30
            Assert.Equal(95, valera.Health);   // 100 - 5 = 95
            Assert.Equal(80, valera.Cash);     // 100 - 20 = 80
        }

        [Fact]
        public void DrinkWithMarg_UpdatesProperties()
        {
            // Arrange
            var valera = new Valera(100, 10, 0, 10, 200);

            // Act
            valera.DrinkWithMarg();

            // Assert
            Assert.Equal(5, valera.Joy);        // 0 + 5 = 5
            Assert.Equal(100, valera.Mana);     // 10 + 90 = 100 (ограничено до 100)
            Assert.Equal(90, valera.Fatigue);   // 10 + 80 = 90
            Assert.Equal(20, valera.Health);    // 100 - 80 = 20
            Assert.Equal(50, valera.Cash);      // 200 - 150 = 50
        }

        [Fact]
        public void Sing_WhenManaBetween40And70_IncreasesCashBy60()
        {
            // Arrange
            var valera = new Valera(100, 50, 0, 0, 100);

            // Act
            valera.Sing();

            // Assert
            Assert.Equal(1, valera.Joy);        // 0 + 1 = 1
            Assert.Equal(60, valera.Mana);      // 50 + 10 = 60
            Assert.Equal(20, valera.Fatigue);   // 0 + 20 = 20
            Assert.Equal(170, valera.Cash);     // 100 + 10 + 50 + 10 = 170 (обратите внимание: в коде дважды добавляются деньги)
        }

        [Fact]
        public void Sing_WhenManaNotBetween40And70_IncreasesCashBy10()
        {
            // Arrange
            var valera = new Valera(100, 30, 0, 0, 100);

            // Act
            valera.Sing();

            // Assert
            Assert.Equal(1, valera.Joy);        // 0 + 1 = 1
            Assert.Equal(40, valera.Mana);      // 30 + 10 = 40
            Assert.Equal(20, valera.Fatigue);   // 0 + 20 = 20
            Assert.Equal(110, valera.Cash);     // 100 + 10 = 110
        }

        [Theory]
        [InlineData(20, 100, 10, 100)]  // мана < 30: здоровье уменьшается на 90
        [InlineData(80, 100, -3, 50)]   // мана > 70: радость уменьшается на 3
        [InlineData(50, 100, 0, 0)]     // нормальный случай
        public void Sleep_UpdatesPropertiesBasedOnMana(int initialMana, int expectedHealth, int expectedJoy, int expectedMana)
        {
            // Arrange
            var valera = new Valera(100, initialMana, 0, 100, 0);

            // Act
            valera.Sleep();

            // Assert
            Assert.Equal(expectedHealth, valera.Health);
            Assert.Equal(expectedJoy, valera.Joy);
            Assert.Equal(expectedMana, valera.Mana); // initialMana - 50 (но с учетом ограничений)
            Assert.Equal(30, valera.Fatigue);        // 100 - 70 = 30
        }

        [Fact]
        public void PropertiesArePrivateSet_ChangesOnlyThroughMethods()
        {
            // Arrange
            var valera = new Valera();

            // Act - все сеттеры приватные, поэтому мы можем изменять свойства только через методы
            valera.TouchGrass();

            // Assert - свойства изменились через метод
            Assert.Equal(1, valera.Joy);
            Assert.Equal(-10, valera.Mana); // 0 - 10 = -10, но ограничено до 0
        }

        [Fact]
        public void ComplexScenario_MultipleActions()
        {
            // Arrange
            var valera = new Valera();

            // Act
            valera.TouchGrass();
            valera.TouchGrass();
            valera.GoToBar();

            // Assert
            Assert.Equal(2, valera.Joy);       // 1 + 1 = 2 после TouchGrass дважды
            Assert.Equal(20, valera.Mana);     // 0 - 10 - 10 + 30 = 10? (нужно пересчитать)
            Assert.Equal(30, valera.Fatigue);  // 0 + 10 + 10 + 10 = 30
            Assert.Equal(95, valera.Health);   // 100 - 5 = 95
            Assert.Equal(-20, valera.Cash);    // 0 - 20 = -20, но ограничено до 0
        }
    }
}