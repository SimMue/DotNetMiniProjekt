using System;
using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class AutoUpdateTests : TestBase
    {
        private AutoManager target;
        private AutoManager Target => target ?? (target = new AutoManager());

        [Fact]
        public void UpdateAutoTest()
        {
            LuxusklasseAuto changedAuto = (LuxusklasseAuto) Target.GetById(3);
            changedAuto.Marke = "VW Golf";
            changedAuto.Tagestarif = 120;
            changedAuto.Basistarif = 50;

            Target.Update(changedAuto);

            LuxusklasseAuto dbAuto = (LuxusklasseAuto)Target.GetById(3);

            Assert.Equal(changedAuto.Id, dbAuto.Id);
            Assert.Equal(changedAuto.Marke, dbAuto.Marke);
            Assert.Equal(changedAuto.Tagestarif, dbAuto.Tagestarif);
            Assert.Equal(changedAuto.Basistarif, dbAuto.Basistarif);
            Assert.Equal(changedAuto.RowVersion, dbAuto.RowVersion);
        }

    }
}
