namespace CprScanner.Business.Test
{
    using Forca.Test.Contexts;
    using NSubstitute;
    using NUnit.Framework;
    using System;

    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class CprTest
    {
        public class CprTestContext : ForcaTestContext
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
        public class Constructor
        {
            [Test]
            public void CprErNull_KasterArgumentNullException()
            {
                using (var testContext = new CprTestContext())
                {
                    //Arrange
                    //Act
                    var exception = Assert.Throws<ArgumentNullException>(
                        () => new Cpr(null));

                    //Assert
                    Assert.That(exception.ParamName, Is.EqualTo("cpr"));
                }
            }

            [Test]
            public void CprErIkkeNull_ObjektOprettes()
            {
                using (var testContext = new CprTestContext())
                {
                    //Arrange
                    //Act
                    //Assert
                    Assert.DoesNotThrow(() => new Cpr(string.Empty));
                }
            }
        }

        [TestFixture]
        [Parallelizable(ParallelScope.All)]
        public class _ToString
        {
            [Test]
            public void SomStandard_ReturnererCprStreng()
            {
                using (var testContext = new CprTestContext())
                {
                    //Arrange
                    var forventet = "test";
                    var sut = new Cpr(forventet);

                    //Act
                    var result = sut.ToString();

                    //Assert
                    Assert.That(result, Is.EqualTo(forventet));
                }
            }
        }

        [TestFixture]
        [Parallelizable(ParallelScope.All)]
        public class _Equals
        {
            [Test]
            public void Null_ReturnererFalse()
            {
                using (var testContext = new CprTestContext())
                {
                    //Arrange
                    var sut = new Cpr("");

                    //Act
                    var result = sut.Equals(null);

                    //Assert
                    Assert.That(result, Is.False);
                }
            }

            [Test]
            public void AndetObjekt_ReturnererFalse()
            {
                using (var testContext = new CprTestContext())
                {
                    //Arrange
                    var sut = new Cpr("");

                    //Act
                    var result = sut.Equals(new object());

                    //Assert
                    Assert.That(result, Is.False);
                }
            }

            [Test]
            public void ForskelligeCpr_ReturnererFalse()
            {
                using (var testContext = new CprTestContext())
                {
                    //Arrange
                    var sut = new Cpr("");

                    //Act
                    var result = sut.Equals("x");

                    //Assert
                    Assert.That(result, Is.False);
                }
            }

            [Test]
            public void IdentiskeCpr_ReturnererTrue()
            {
                using (var testContext = new CprTestContext())
                {
                    //Arrange
                    var sut = new Cpr("x");

                    //Act
                    var result = sut.Equals(new Cpr("x"));

                    //Assert
                    Assert.That(result, Is.True);
                }
            }

            [Test]
            public void Equals_SammeObjekt_ReturnereTrue()
            {
                using (var testContext = new CprTestContext())
                {
                    //Arrange
                    var sut = new Cpr("x");

                    //Act
                    var result = sut.Equals(sut);

                    //Assert
                    Assert.That(result, Is.True);
                }
            }
        }

        [TestFixture]
        [Parallelizable(ParallelScope.All)]
        public class _GetHashCode
        {
            [Test]
            public void SomStandard_KalderGetHashCode()
            {
                using (var testContext = new CprTestContext())
                {
                    //Arrange
                    var input = "1234567890";
                    var forventet = input.GetHashCode();
                    var sut = new Cpr(input);

                    //Act
                    var result = sut.GetHashCode();

                    //Assert
                    Assert.That(result, Is.EqualTo(forventet));
                }
            }
        }

        [TestFixture]
        [Parallelizable(ParallelScope.All)]
        public class ImplicitOperator
        {
            [Test]
            public void StringVærdi_ReturnereCpr()
            {
                using (var testContext = new CprTestContext())
                {
                    //Arrange
                    var str = "1234567890";

                    //Act
                    Cpr resultat = str;

                    //Assert
                    Assert.That(resultat, Is.InstanceOf<Cpr>());
                }
            }

            [Test]
            public void StringTilCpr_BeholderVærdi()
            {
                using (var testContext = new CprTestContext())
                {
                    //Arrange
                    var str = "1234567890";

                    //Act
                    Cpr resultat = str;

                    //Assert
                    Assert.That(resultat.ToString(), Is.EqualTo(str));
                }
            }
        }

        [TestFixture]
        [Parallelizable(ParallelScope.All)]
        public class Validate
        {
            [Test]
            public void ValidCpr_ReturnerTrue()
            {
                using (var testContext = new CprTestContext())
                {
                    //Arrange
                    var sut = new Cpr("220200-0001");

                    //Act
                    var result = sut.ErValid();

                    //Assert
                    Assert.That(result, Is.True);
                }
            }

            [Test]
            public void IkkeValidCpr_ReturnerFalse()
            {
                using (var testContext = new CprTestContext())
                {
                    //Arrange
                    var sut = new Cpr("x");

                    //Act
                    var result = sut.ErValid();

                    //Assert
                    Assert.That(result, Is.False);
                }
            }
        }
    }
}
