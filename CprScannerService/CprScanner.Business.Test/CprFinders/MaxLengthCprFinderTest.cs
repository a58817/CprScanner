namespace CprScanner.Business.Test.CprFinders
{
    using CprScanner.Business.CprFinders;
    using Forca.Test.Contexts;
    using NUnit.Framework;

    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class MaxLengthCprFinderTest
    {
        public class MaxLengthCprFinderTestContext : ForcaTestContext
        {
            public MaxLengthCprFinder Sut { get; private set; }

            public override void SetUp()
            {
                this.Sut = new MaxLengthCprFinder();
            }

            public override void TearDown()
            {
            }
        }

        [TestFixture]
        [Parallelizable(ParallelScope.All)]
        public class Match
        {
            [Test]
            public void TextErNull_ReturnerIntet()
            {
                using (var testContext = new MaxLengthCprFinderTestContext())
                {
                    //Arrange
                    //Act
                    var resultat = testContext.Sut.Match(null);

                    //Assert
                    Assert.That(resultat, Is.Empty);
                }
            }

            [Test]
            public void TextErTomStreng_ReturnerIntet()
            {
                using (var testContext = new MaxLengthCprFinderTestContext())
                {
                    //Arrange
                    //Act
                    var resultat = testContext.Sut.Match(string.Empty);

                    //Assert
                    Assert.That(resultat, Is.Empty);
                }
            }

            [Test]
            public void TextErTiCifre_ReturnerCpr()
            {
                using (var testContext = new MaxLengthCprFinderTestContext())
                {
                    //Arrange
                    var forventet = "0000000000";

                    //Act
                    var resultat = testContext.Sut.Match(forventet);

                    //Assert
                    Assert.That(resultat, Does.Contain(new Cpr(forventet)));
                }
            }

            [Test]
            public void TextErNiCifre_ReturnererIntet()
            {
                using (var testContext = new MaxLengthCprFinderTestContext())
                {
                    //Arrange
                    var text = "000000000";

                    //Act
                    var resultat = testContext.Sut.Match(text);

                    //Assert
                    Assert.That(resultat, Is.Empty);
                }
            }

            [Test]
            public void TextErElleveCifre_ReturnererIntet()
            {
                using (var testContext = new MaxLengthCprFinderTestContext())
                {
                    //Arrange
                    var text = "00000000000";

                    //Act
                    var resultat = testContext.Sut.Match(text);

                    //Assert
                    Assert.That(resultat, Is.Empty);
                }
            }

            [TestCase("0000000000")]
            [TestCase("test0000000000")]
            [TestCase("0000000000test")]
            [TestCase("test0000000000test")]
            public void TextErKendtCprFormat_ReturnerFundetMatch(string tekst)
            {
                using (var testContext = new MaxLengthCprFinderTestContext())
                {
                    //Arrange
                    var forventet = "0000000000";

                    //Act
                    var resultat = testContext.Sut.Match(tekst);

                    //Assert
                    Assert.That(resultat, Does.Contain(new Cpr(forventet)));
                }
            }

            [TestCase("0000000000test0000000001")]
            [TestCase("test0000000000test0000000001test")]
            [TestCase("0000000000test0000000001")]
            [TestCase("test0000000000test0000000001")]
            public void TextErKendtCprFormat_ReturnererFlereFundneMatch(string tekst)
            {
                using (var testContext = new MaxLengthCprFinderTestContext())
                {
                    //Arrange
                    var forventet = new Cpr[] { "0000000000", "0000000001" };

                    //Act
                    var resultat = testContext.Sut.Match(tekst);

                    //Assert
                    CollectionAssert.AreEquivalent(forventet, resultat);
                }
            }

            [TestCase("000000-0000")]
            [TestCase("test000000.0000")]
            [TestCase("000000/0000test")]
            [TestCase("test000000#0000test")]
            public void TextErAlternativtCprFormat_ReturnerIntet(string tekst)
            {
                using (var testContext = new MaxLengthCprFinderTestContext())
                {
                    //Arrange
                    //Act
                    var resultat = testContext.Sut.Match(tekst);

                    //Assert
                    Assert.That(resultat, Is.Empty);
                }
            }
        }
    }
}