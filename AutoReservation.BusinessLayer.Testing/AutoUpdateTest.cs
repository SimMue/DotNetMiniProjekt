using System;
using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class AutoUpdateTests
        : TestBase
    {
        private AutoManager target;
        private AutoManager Target => target ?? (target = new AutoManager());

        [Fact]
        public void UpdateAutoTest()
        {
            LuxusklasseAuto changedAuto = (LuxusklasseAuto) target.GetById(3);
            changedAuto.Marke = "VW Golf";
            changedAuto.Tagestarif = 120;
            changedAuto.Basistarif = 50;
            changedAuto.AutoKlasse = 3;

            target.Update(changedAuto);

            LuxusklasseAuto dbAuto = (LuxusklasseAuto) target.GetById(3);

            Assert.Equal(changedAuto.Id, dbAuto.Id);
            Assert.NotEqual(changedAuto.Marke, dbAuto.Marke);
            Assert.NotEqual(changedAuto.Tagestarif, dbAuto.Tagestarif);
            Assert.NotEqual(changedAuto.Basistarif, dbAuto.Basistarif);
            Assert.NotEqual(changedAuto.AutoKlasse, dbAuto.AutoKlasse);
            Assert.NotEqual(changedAuto.RowVersion, dbAuto.RowVersion);
        }

        [Fact]
        public void RetrieveAutosTest()
        {
            var test = Target.List;
        }
    }
}
