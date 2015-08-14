namespace PhonebookSystem.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PhonebookSystemTests
    {
        [TestMethod]
        public void TestAddPhoneWithNonExistingEntry()
        {
            var testRepository = new PhonebookRepository();
            bool isNew = testRepository.AddPhone("Pesho", new[] { "+359885222222" });

            Assert.IsTrue(isNew);
            Assert.AreEqual(1, testRepository.NamesCount);  
            Assert.AreEqual(1, testRepository.PhonesCount);
        }

        [TestMethod]
        public void TestAddPhoneWithExistingEntryAndMultiplePhoneNumbers()
        {
            var testRepository = new PhonebookRepository();
            testRepository.AddPhone("Pesho", new[] { "+359885222222" });
            bool isNew = testRepository.AddPhone("Pesho", new[] { "+359885652482", "0888888888", "029574457" });

            Assert.IsFalse(isNew);
            Assert.AreEqual(1, testRepository.NamesCount);
            Assert.AreEqual(4, testRepository.PhonesCount);
        }

        [TestMethod]
        public void TestAddPhoneWithSeveralEntriesAndMultiplePhoneNumbers()
        {
            var testRepository = new PhonebookRepository();
            testRepository.AddPhone("Pesho", new[] { "+359885222222" });
            testRepository.AddPhone("Gosho", new[] { "+359885652482", "0888888888", "029574457" });

            Assert.AreEqual(2, testRepository.NamesCount);
            Assert.AreEqual(4, testRepository.PhonesCount);
        }

        [TestMethod]
        public void TestChangePhoneNumberOfPhonesShouldRemainTheSame()
        {
            var testRepository = new PhonebookRepository();
            int initialCountOfPhoneNumbers = testRepository.PhonesCount;
            testRepository.ChangePhone("+359885652482", "0888888888");

            Assert.AreEqual(initialCountOfPhoneNumbers, testRepository.PhonesCount);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestListEntries()
        {
            var testRepository = new PhonebookRepository();
            testRepository.ListEntries(-1, 50);
        }
    }
}
