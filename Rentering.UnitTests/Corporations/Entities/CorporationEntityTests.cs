﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rentering.Corporation.Domain.Entities;

namespace Rentering.UnitTests.Corporations.Entities
{
    [TestClass]
    public class CorporationEntityTests
    {
        [TestMethod]
        public void ShouldNotInviteParticipant_WhenCorporationIsNotInProgress()
        {
            var corporation = new CorporationEntity("corporation 1", 1);
            corporation.InviteParticipant(10, 55);
            corporation.FinishCreation();
            corporation.InviteParticipant(11, 45);

            Assert.AreEqual(false, corporation.Valid);
        }

        [TestMethod]
        public void ShouldNotInviteParticipant_WhenParticipantIsAlreadyInvited()
        {
            var corporation = new CorporationEntity("corporation 1", 1);
            corporation.InviteParticipant(10, 55);
            corporation.InviteParticipant(10, 45);

            Assert.AreEqual(false, corporation.Valid);
        }

        [TestMethod]
        public void ShouldInviteParticipant()
        {
            var corporation = new CorporationEntity("corporation 1", 1);
            corporation.InviteParticipant(10, 55);
            corporation.InviteParticipant(11, 40);

            Assert.AreEqual(true, corporation.Valid);
        }

        [TestMethod]
        public void ShouldNotFinishCreation_WhenCorporationIsNotInProgress()
        {
            var corporation = new CorporationEntity("corporation 1", 1);
            corporation.InviteParticipant(10, 55);
            corporation.FinishCreation();
            corporation.FinishCreation();

            Assert.AreEqual(false, corporation.Valid);
        }

        [TestMethod]
        public void ShouldNotFinishCreation_WhenThereAreNoParticipantsInvited()
        {
            var corporation = new CorporationEntity("corporation 1", 1);
            corporation.FinishCreation();

            Assert.AreEqual(false, corporation.Valid);
        }

        [TestMethod]
        public void ShouldFinishCreation()
        {
            var corporation = new CorporationEntity("corporation 1", 1);
            corporation.InviteParticipant(10, 55);
            corporation.FinishCreation();

            Assert.AreEqual(true, corporation.Valid);
        }

        [TestMethod]
        public void ShouldNotAcceptToParticipate_WhenCorporationIsNotWaitingParticipants()
        {
            var corporation = new CorporationEntity("corporation 1", 1);
            corporation.InviteParticipant(10, 55);
            corporation.InviteParticipant(11, 60);
            corporation.AcceptToParticipate(1);

            Assert.AreEqual(true, corporation.Valid);
        }
    }
}
