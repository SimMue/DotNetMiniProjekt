using System;
using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class KundeUpdateTest
        : TestBase
    {
        private KundeManager target;
        private KundeManager Target => target ?? (target = new KundeManager());

        [Fact]
        public void UpdateKundeTest()
        {
            Kunde changedKunde = target.GetById(1);
            changedKunde.Nachname = "Beil";
            changedKunde.Vorname = "Timo";
            changedKunde.Geburtsdatum = new DateTime();

            target.Update(changedKunde);

            Kunde dbKunde = target.GetById(1);

            Assert.Equal(changedKunde.Id, dbKunde.Id);
            Assert.Equal(changedKunde.Nachname, dbKunde.Nachname);
            Assert.Equal(changedKunde.Vorname, dbKunde.Vorname);
            Assert.Equal(changedKunde.Geburtsdatum, dbKunde.Geburtsdatum);
            Assert.Equal(changedKunde.RowVersion, dbKunde.RowVersion);
        }
    }
}
