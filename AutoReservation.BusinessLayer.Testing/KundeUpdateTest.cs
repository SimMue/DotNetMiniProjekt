using System;
using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class KundeUpdateTest
        : TestBase
    {
        private KundeManager _target;
        private KundeManager Target => _target ?? (_target = new KundeManager());

        [Fact]
        public void UpdateKundeTest()
        {
            Kunde changedKunde = Target.GetById(1);
            changedKunde.Nachname = "Beil";
            changedKunde.Vorname = "Timo";
            changedKunde.Geburtsdatum = new DateTime();

            Target.Update(changedKunde);

            Kunde dbKunde = Target.GetById(1);

            Assert.Equal(changedKunde.Id, dbKunde.Id);
            Assert.Equal(changedKunde.Nachname, dbKunde.Nachname);
            Assert.Equal(changedKunde.Vorname, dbKunde.Vorname);
            Assert.Equal(changedKunde.Geburtsdatum, dbKunde.Geburtsdatum);
            Assert.Equal(changedKunde.RowVersion, dbKunde.RowVersion);
        }
    }
}
