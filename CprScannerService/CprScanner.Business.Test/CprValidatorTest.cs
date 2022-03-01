namespace CprScanner.Business.Test
{
    using System;
    using NUnit.Framework;

    class CprValidatorTest
    {
        [Test]
        public void Ctor_Null_KasterException()
        {
            Assert.Catch<ArgumentNullException>(() => new CprValidator(null));
        }

        [Test]
        public void Validate_TomtCpr_ReturnereFalse()
        {
            string cpr = string.Empty;
            var sut = new CprValidator(cpr);
            var result = sut.Validate();
            Assert.IsFalse(result);
        }

        [TestCase("ab00000000")]
        [TestCase("01ba000000")]
        public void Validate_TekstICpr_ReturnerFalse(string cpr)
        {
            var sut = new CprValidator(cpr);
            var result = sut.Validate();
            Assert.IsFalse(result);
        }

        [Test]
        public void Validate_ForKortCpr_ReturnereFalse()
        {
            var cpr = "010100000"; // Kun 9 cifre langt
            var sut = new CprValidator(cpr);
            var result = sut.Validate();
            Assert.IsFalse(result);
        }
        
        [Test]
        public void Validate_ForKortCprMedSeperator_ReturnereFalse()
        {
            var cpr = "010100-000"; // Kun 9 cifre langt
            var sut = new CprValidator(cpr);
            var result = sut.Validate();
            Assert.IsFalse(result);
        }

        [Test]
        public void Validate_ForLangtCpr_ReturnereFalse()
        {
            var cpr = "010100000000"; // Hele 11 cifre langt
            var sut = new CprValidator(cpr);
            var result = sut.Validate();
            Assert.IsFalse(result);
        }

        [TestCase("0101000000")]
        [TestCase("3112000000")]
        [TestCase("6101000000")]
        [TestCase("9112000000")]
        [TestCase("1005000000")]
        public void Validate_ValidCprWithoutSeperator_ReturnereTrue(string cpr)
        {
            var sut = new CprValidator(cpr);
            var result = sut.Validate();
            Assert.IsTrue(result);
        }

        [TestCase("010100-0000")]
        [TestCase("311200/0000")]
        [TestCase("610100-0000")]
        [TestCase("911200/0000")]
        public void Validate_ValidCprWithSeperator_ReturnereTrue(string cpr)
        {
            var sut = new CprValidator(cpr);
            var result = sut.Validate();
            Assert.IsTrue(result);
        }

        [TestCase("0000000000")]
        [TestCase("3201000000")]
        [TestCase("0100000000")]
        [TestCase("6001000000")]
        [TestCase("9212000000")]
        [TestCase("0113000000")]
        [TestCase("010100¤0000")]
        public void Validate_InvalidCpr_ReturnereFalse(string cpr)
        {
            var sut = new CprValidator(cpr);
            var result = sut.Validate();
            Assert.IsFalse(result);
        }
    }
}
