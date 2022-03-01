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
    public class FileExtractorStrategyTest
    {
        public class FileExtractorStrategyTestContext : ForcaTestContext
        {
            public IDictionary<string, IFileExtractor> Extractors { get; private set; }

            public IFileExtractor Extractor { get; private set; }

            public FileExtractorStrategy Sut { get; private set; }

            public override void SetUp()
            {
                this.Extractors = new Dictionary<string, IFileExtractor>();
                this.Extractor = Substitute.For<IFileExtractor>();
                this.Sut = new FileExtractorStrategy(this.Extractors);
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
            public void LoggerErNull_KasterArgumentNullException()
            {
                using (var testContext = new FileExtractorStrategyTestContext())
                {
                    //Arrange
                    //Act
                    var exception = Assert.Throws<ArgumentNullException>(
                        () => new FileExtractorStrategy(null));

                    //Assert
                    Assert.That(exception.ParamName, Is.EqualTo("extractors"));
                }
            }

            [Test]
            public void IngenParametreErNulle_ObjektOprettes()
            {
                using (var testContext = new FileExtractorStrategyTestContext())
                {
                    //Arrange
                    //Act
                    //Assert
                    Assert.DoesNotThrow(
                        () => new FileExtractorStrategy(testContext.Extractors));
                }
            }
        }

        [TestFixture]
        [Parallelizable(ParallelScope.All)]
        public class ExtractFiles
        {

            [Test]
            public void IngenExtractors_ReturnerInput()
            {
                using (var testContext = new FileExtractorStrategyTestContext())
                {
                    //Arrange
                    var forventet = new CandidateFile(null, null);

                    //Act
                    var resultat = testContext.Sut.ExtractFiles(forventet);

                    //Assert
                    Assert.That(resultat, Has.Exactly(1).EqualTo(forventet));
                }
            }

            [Test]
            public void ExtractorsHarExtension_ReturnerExtractFilesResultat()
            {
                using (var testContext = new FileExtractorStrategyTestContext())
                {
                    //Arrange
                    var forventet = new CandidateFile(null, "test");
                    testContext.Extractor.ExtractFiles(Arg.Any<Stream>()).Returns(Array.Empty<CandidateFile>());
                    testContext.Extractors.Add("", testContext.Extractor);

                    //Act
                    var resultat = testContext.Sut.ExtractFiles(forventet);

                    //Assert
                    Assert.That(resultat, Has.Exactly(1).EqualTo(forventet));
                }
            }

            [Test]
            public void FlereNiveauer_ReturnerExtractFilesResultat()
            {
                using (var testContext = new FileExtractorStrategyTestContext())
                {
                    //Arrange
                    var stream = new MemoryStream();
                    var forventetParent = new CandidateFile(null, "parent");
                    var forventetChild = new CandidateFile(stream, "child");

                    testContext.Extractor.ExtractFiles(null).Returns(new CandidateFile[] { forventetChild });
                    testContext.Extractor.ExtractFiles(stream).Returns(Array.Empty<CandidateFile>());

                    testContext.Extractors.Add("", testContext.Extractor);

                    //Act
                    var resultater = testContext.Sut.ExtractFiles(forventetParent);

                    //Assert
                    var enumerator = resultater.GetEnumerator();

                    enumerator.MoveNext();
                    Assert.That(enumerator.Current, Is.EqualTo(forventetParent));

                    enumerator.MoveNext();
                    Assert.That(enumerator.Current, Is.EqualTo(forventetChild));
                }
            }
        }
    }
}