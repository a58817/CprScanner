namespace CprScanner.Business.Test.CprFinders
{
    using CprScanner.Business.CprFinders;
    using Forca.Test.Contexts;
    using NSubstitute;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class CprFinderCompositTest
    {
        public class CprFinderCompositTestContext : ForcaTestContext
        {
            public List<ICprFinder> CprFinders { get; private set; }

            public ICprFinder CprFinder { get; private set; }

            public override void SetUp()
            {
                this.CprFinders = new List<ICprFinder>();
                this.CprFinder = Substitute.For<ICprFinder>();
            }

            public override void TearDown()
            {
            }
        }

        [TestFixture]
        [Parallelizable(ParallelScope.All)]
        public class Constructor
        {
            [Test]
            public void CprFindersErNull_KasterArgumentNullException()
            {
                using (var testContext = new CprFinderCompositTestContext())
                {
                    //Arrange
                    //Act
                    var exception = Assert.Throws<ArgumentNullException>(() => new CprFinderComposit(null));

                    //Assert
                    Assert.That(exception.ParamName, Is.EqualTo("cprFinders"));
                }
            }

            [Test]
            public void CprFindersErIkkeNull_ObjektOprettes()
            {
                using (var testContext = new CprFinderCompositTestContext())
                {
                    //Arrange
                    //Act
                    //Assert
                    Assert.DoesNotThrow(() => new CprFinderComposit(new List<ICprFinder>()));
                }
            }
        }

        [TestFixture]
        [Parallelizable(ParallelScope.All)]
        public class Match
        {
            [Test]
            public void IngenCprFinders_ReturnererTomListe()
            {
                using (var testContext = new CprFinderCompositTestContext())
                {
                    //Arrange
                    var sut = new CprFinderComposit(testContext.CprFinders);

                    //Act
                    var matches = sut.Match("121200-1212");

                    //Assert
                    Assert.That(matches, Is.Empty);
                }
            }

            [Test]
            public void CprFinderReturnererIngenResultater_ReturnererTomListe()
            {
                using (var testContext = new CprFinderCompositTestContext())
                {
                    //Arrange
                    testContext.CprFinder.Match(Arg.Any<string>()).Returns(new List<Cpr>());
                    testContext.CprFinders.Add(testContext.CprFinder);
                    var sut = new CprFinderComposit(testContext.CprFinders);

                    //Act
                    var matches = sut.Match("121200-1212");

                    //Assert
                    Assert.That(matches, Is.Empty);
                }
            }

            [Test]
            public void TextErNull_ReturnererTomListe()
            {
                using (var testContext = new CprFinderCompositTestContext())
                {
                    //Arrange
                    testContext.CprFinder.Match(null).Returns(new List<Cpr>());
                    testContext.CprFinders.Add(testContext.CprFinder);
                    var sut = new CprFinderComposit(testContext.CprFinders);

                    //Act
                    var matches = sut.Match(null);

                    //Assert
                    Assert.That(matches, Is.Empty);
                }
            }

            [Test]
            public void CprFinderReturnererEtResultatet_ReturnererListeMedEtCprNummer()
            {
                using (var testContext = new CprFinderCompositTestContext())
                {
                    //Arrange
                    var forventetMatch = "121200-1212";
                    testContext.CprFinder.Match(forventetMatch).Returns(new List<Cpr>() { new Cpr(forventetMatch) });
                    testContext.CprFinders.Add(testContext.CprFinder);
                    var sut = new CprFinderComposit(testContext.CprFinders);

                    //Act
                    var matches = sut.Match(forventetMatch);

                    //Assert
                    Assert.That(matches, Has.Exactly(1).EqualTo(new Cpr(forventetMatch)));
                }
            }
        }
    }
}