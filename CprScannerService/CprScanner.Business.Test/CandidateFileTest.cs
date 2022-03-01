namespace CprScanner.Business.Test
{
    using Forca.Test.Contexts;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class CandidateFileTest
    {
        public class CandidateFileTestContext : ForcaTestContext
        {
            public override void SetUp()
            {
            }

            public override void TearDown()
            {
            }
        }

        [TestFixture]
        [Parallelizable(ParallelScope.All)]
        public class GetExtension
        {

            [Test]
            public void ExtensionMedStoreBogstaver_ReturnererExtensionMedSmåBogstaver()
            {
                using (var testContext = new CandidateFileTestContext())
                {
                    //Arrange
                    var forventet = ".stor";
                    var sut = new CandidateFile(null, ".STOR");
                    
                    //Act
                    var resultat = sut.GetExtension();

                    //Assert
                    Assert.That(resultat, Is.EqualTo(forventet));
                }
            }

            [Test]
            public void FileNameNull_GemmerExtensionSomTomStreng()
            {
                using (var testContext = new CandidateFileTestContext())
                {
                    //Arrange
                    var forventet = "";
                    var sut = new CandidateFile(null, null);

                    //Act
                    var resultat = sut.GetExtension();

                    //Assert
                    Assert.That(resultat, Is.EqualTo(forventet));
                }
            }

            [Test]
            public void FilnavnUdenExtensionNull_ReturnererTomStreng()
            {
                using (var testContext = new CandidateFileTestContext())
                {
                    //Arrange
                    var forventet = "";
                    var sut = new CandidateFile(null, "test");

                    //Act
                    var resultat = sut.GetExtension();

                    //Assert
                    Assert.That(resultat, Is.EqualTo(forventet));
                }
            }

            [Test]
            public void FilnavnMedExtensionNull_ReturnererExtension()
            {
                using (var testContext = new CandidateFileTestContext())
                {
                    //Arrange
                    var forventet = ".ext";
                    var sut = new CandidateFile(null, "test.ext");

                    //Act
                    var resultat = sut.GetExtension();

                    //Assert
                    Assert.That(resultat, Is.EqualTo(forventet));
                }
            }
        }
    }
}