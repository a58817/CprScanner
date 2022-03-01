namespace CprScanner.Business.Test.Strategies
{
    using CprScanner.Business.Ports;
    using CprScanner.Business.Strategies;
    using Forca.Test.Contexts;
    using NSubstitute;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.IO;

    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class FileConverterStrategyTest
    {
        public class FileConverterStrategyTestContext : ForcaTestContext
        {
            public IDictionary<string, ITextConverter> Converters { get; private set; }

            public ITextConverter Converter { get; private set; }

            public IBusinessLogger Logger { get; private set; }

            public FileConverterStrategy Sut { get; private set; }

            public override void SetUp()
            {
                this.Converters = new Dictionary<string, ITextConverter>();
                this.Converter = Substitute.For<ITextConverter>();
                this.Logger = Substitute.For<IBusinessLogger>();
                this.Sut = new FileConverterStrategy(this.Converters, this.Logger);
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
            public void ConvertersErNull_KasterArgumentNullException()
            {
                using (var testContext = new FileConverterStrategyTestContext())
                {
                    //Arrange
                    //Act
                    var exception = Assert.Throws<ArgumentNullException>(
                        () => new FileConverterStrategy(null, testContext.Logger));

                    //Assert
                    Assert.That(exception.ParamName, Is.EqualTo("converters"));
                }
            }

            [Test]
            public void LoggerErNull_KasterArgumentNullException()
            {
                using (var testContext = new FileConverterStrategyTestContext())
                {
                    //Arrange
                    //Act
                    var exception = Assert.Throws<ArgumentNullException>(
                        () => new FileConverterStrategy(testContext.Converters, null));

                    //Assert
                    Assert.That(exception.ParamName, Is.EqualTo("logger"));
                }
            }

            [Test]
            public void IngenParametreErNulle_ObjektOprettes()
            {
                using (var testContext = new FileConverterStrategyTestContext())
                {
                    //Arrange
                    //Act
                    //Assert
                    Assert.DoesNotThrow(
                        () => new FileConverterStrategy(testContext.Converters, testContext.Logger));
                }
            }
        }

        [TestFixture]
        [Parallelizable(ParallelScope.All)]
        public class Convert
        {
            [Test]
            public void ConverterHarIkkeExtension_ReturnerTomStreng()
            {
                using (var testContext = new FileConverterStrategyTestContext())
                {
                    //Arrange
                    //Act
                    var resultat = testContext.Sut.Convert(new CandidateFile(null, null));

                    //Assert
                    Assert.That(resultat, Is.Empty);
                }
            }

            [Test]
            public void ConverterHarIkkeExtension_LoggerError()
            {
                using (var testContext = new FileConverterStrategyTestContext())
                {
                    //Arrange
                    //Act
                    var resultat = testContext.Sut.Convert(new CandidateFile(null, null));

                    //Assert
                    testContext.Logger.Received().Error(Arg.Any<string>());
                }

            }

            [Test]
            public void ConverterHarExtension_ReturnerTextConverterResultat()
            {
                using (var testContext = new FileConverterStrategyTestContext())
                {
                    //Arrange
                    var forventet = "En tekst";
                    testContext.Converter.Convert(Arg.Any<Stream>()).Returns(forventet);
                    testContext.Converters.Add("", testContext.Converter);

                    //Act
                    var resultat = testContext.Sut.Convert(new CandidateFile(null, null));

                    //Assert
                    Assert.That(resultat, Is.EqualTo(forventet));
                }
            }
        }
    }
}