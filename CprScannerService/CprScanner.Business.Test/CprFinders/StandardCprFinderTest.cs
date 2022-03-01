namespace CprScanner.Business.Test.CprFinders
{
    using CprScanner.Business.CprFinders;
    using Forca.Test.Contexts;
    using NUnit.Framework;

    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class StandardCprFinderTest
    {
        public class StandardCprFinderTestContext : ForcaTestContext
        {
            public StandardCprFinder Sut { get; private set; }

            public override void SetUp()
            {
                this.Sut = new StandardCprFinder();
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
            public void TekstErNull_ReturnerIntet()
            {
                using (var testContext = new StandardCprFinderTestContext())
                {
                    //Arrange
                    //Act
                    var resultat = testContext.Sut.Match(null);

                    //Assert
                    Assert.That(resultat, Is.Empty);
                }
            }

            [Test]
            public void TekstErTomStreng_ReturnerIntet()
            {
                using (var testContext = new StandardCprFinderTestContext())
                {
                    //Arrange
                    //Act
                    var resultat = testContext.Sut.Match(string.Empty);

                    //Assert
                    Assert.That(resultat, Is.Empty);
                }
            }

            [Test]
            public void TekstErCprMedBindestreg_ReturnerFundet()
            {

                using (var testContext = new StandardCprFinderTestContext())
                {
                    //Arrange
                    var forventet = "000000-0000";

                    //Act
                    var resultat = testContext.Sut.Match(forventet);

                    //Assert
                    Assert.That(resultat, Has.Exactly(1).EqualTo(new Cpr(forventet)));
                }
            }

            [Test]
            public void TekstErNiCifre_ReturnererIntet()
            {
                using (var testContext = new StandardCprFinderTestContext())
                {
                    //Arrange
                    var tekst = "000000000";

                    //Act
                    var resultat = testContext.Sut.Match(tekst);

                    //Assert
                    Assert.That(resultat, Is.Empty);
                }
            }

            [Test]
            public void TekstErElleveCifre_ReturnererIntet()
            {
                using (var testContext = new StandardCprFinderTestContext())
                {
                    //Arrange
                    var tekst = "00000000000";

                    //Act
                    var resultat = testContext.Sut.Match(tekst);

                    //Assert
                    Assert.That(resultat, Is.Empty);
                }
            }

            [TestCase("000000-0000", "000000-0000")]
            [TestCase("test000000/0000", "000000/0000")]
            [TestCase("000000-0000test", "000000-0000")]
            [TestCase("test000000/0000test", "000000/0000")]
            public void TextErKendtCprFormat_ReturnerFundetCprNummer(string tekst, string forventet)
            {
                using (var testContext = new StandardCprFinderTestContext())
                {
                    //Arrange
                    //Act
                    var resultat = testContext.Sut.Match(tekst);

                    //Assert
                    Assert.That(resultat, Has.Exactly(1).EqualTo(new Cpr(forventet)));
                }
            }

            [TestCase("000000-0000test000000-0001", "000000-0000", "000000-0001")]
            [TestCase("test000000-0000test000000/0001test", "000000-0000", "000000/0001")]
            [TestCase("000000/0000test000000-0001", "000000/0000", "000000-0001")]
            [TestCase("test000000/0000test000000/0001", "000000/0000", "000000/0001")]
            public void TextErKendtCprFormat_ReturnerFlereFundneCprNumre(string tekst, string førsteForventet, string andetForventet)
            {
                using (var testContext = new StandardCprFinderTestContext())
                {
                    //Arrange
                    var forventet = new Cpr[] { førsteForventet, andetForventet };

                    //Act
                    var resultat = testContext.Sut.Match(tekst);

                    //Assert
                    CollectionAssert.AreEquivalent(forventet, resultat);
                }
            }

            [TestCase("0000000000")]
            [TestCase("test000000.0000")]
            [TestCase("0000000000test")]
            [TestCase("test000000#0000test")]
            public void TextErAlternativtCprFormat_ReturnerIntet(string tekst)
            {
                using (var testContext = new StandardCprFinderTestContext())
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