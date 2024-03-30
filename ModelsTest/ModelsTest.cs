using taskograph.Models.Tables;

namespace ModelsTest
{
    [TestClass]
    public class ModelsTest
    {
        [TestMethod]
        public void DurationAddOperatorTest()
        {
            Duration d1 = new Duration()
            {
                Minutes = 15
            };
            Duration d2 = new Duration()
            {
                Minutes = 30
            };
            Duration res = d1 + d2;
            Assert.AreEqual(45, res.Minutes);
            Assert.AreEqual(null, res.Hours);

            //45min + 45min = 01hr 30m
            NullAllFields(ref d1);
            NullAllFields(ref d2);
            d1.Minutes = 45;
            d2.Minutes = 45;
            res = d1 + d2;
            Assert.AreEqual(30, res.Minutes);
            Assert.AreEqual(1, res.Hours);
            Assert.AreEqual(null, res.Days);

            //60min + 60min = 2hr 00m (minutes = null)
            NullAllFields(ref d1);
            NullAllFields(ref d2);
            d1.Minutes = 60;
            d2.Minutes = 60;
            res = d1 + d2;
            Assert.AreEqual(null, res.Minutes);
            Assert.AreEqual(2, res.Hours);
            Assert.AreEqual(null, res.Days);

            //20hr + 10hr = 1d 06hr
            NullAllFields(ref d1);
            NullAllFields(ref d2);
            d1.Hours = 20;
            d2.Hours = 10;
            res = d1 + d2;
            Assert.AreEqual(null, res.Minutes);
            Assert.AreEqual(6, res.Hours);
            Assert.AreEqual(1, res.Days);
            Assert.AreEqual(null, res.Weeks);

            //5days + 10day = 2wk 1day
            NullAllFields(ref d1);
            NullAllFields(ref d2);
            d1.Days = 5;
            d2.Days = 10;
            res = d1 + d2;
            Assert.AreEqual(2, res.Weeks);
            Assert.AreEqual(1, res.Days);

            //10wk + 5wk = 3months 3weeks
            NullAllFields(ref d1);
            NullAllFields(ref d2);
            d1.Weeks = 10;
            d2.Weeks = 5;
            res = d1 + d2;
            Assert.AreEqual(3, res.Months);
            Assert.AreEqual(3, res.Weeks);

            //3wk + 10min = 3weeks 10minutes
            NullAllFields(ref d1);
            NullAllFields(ref d2);
            d1.Weeks = 3;
            d2.Minutes = 10;
            res = d1 + d2;
            Assert.AreEqual(3, res.Weeks);
            Assert.AreEqual(10, res.Minutes);
        }

        private void NullAllFields(ref Duration input)
        {
            input.Minutes = null;
            input.Hours = null;
            input.Days = null;
            input.Weeks = null;
            input.Months = null;
        }
    }
}