using System;
using System.Collections.Generic;
using cv19ResSupportV3.V3.Controllers;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.UseCase;
using cv19ResSupportV3.V3.UseCase.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Moq;

namespace cv19ResRupportV3.Tests.V3.Controllers
{

    [TestFixture]
    public class HelpRequestsControllerTests
    {
        private HelpRequestsController _classUnderTest;
        private Mock<ICreateHelpRequestUseCase> _fakeCreateHelpRequestUseCase;
        private Mock<IUpdateHelpRequestUseCase> _fakeUpdateHelpRequestUseCase;
        private Mock<IGetHelpRequestsUseCase> _fakeGetHelpRequestsUseCase;
        private Mock<IGetHelpRequestUseCase> _fakeGetHelpRequestUseCase;

        [SetUp]
        public void SetUp()
        {
            _fakeCreateHelpRequestUseCase = new Mock<ICreateHelpRequestUseCase>();
            _fakeUpdateHelpRequestUseCase = new Mock<IUpdateHelpRequestUseCase>();
            _fakeGetHelpRequestsUseCase = new Mock<IGetHelpRequestsUseCase>();
            _fakeGetHelpRequestUseCase = new Mock<IGetHelpRequestUseCase>();
            _classUnderTest = new HelpRequestsController(_fakeCreateHelpRequestUseCase.Object,
                _fakeGetHelpRequestsUseCase.Object, _fakeUpdateHelpRequestUseCase.Object, _fakeGetHelpRequestUseCase.Object);
        }

        // [Test]
        public void ReturnsResponseWithStatus()
        {
            var request = new HelpRequest
            {
                IsOnBehalf = false,
                ConsentToCompleteOnBehalf = false,
                OnBehalfFirstName = "Test",
                OnBehalfLastName = "Test",
                OnBehalfEmailAddress = "Test",
                OnBehalfContactNumber = "Test",
                RelationshipWithResident = "Test",
                PostCode = "Test",
                Uprn = "Test",
                Ward = "Test",
                AddressFirstLine = "Test",
                AddressSecondLine = "Test",
                AddressThirdLine = "Test",
                GettingInTouchReason = "Test",
                HelpWithAccessingFood = false,
                HelpWithAccessingMedicine = false,
                HelpWithAccessingOtherEssentials = false,
                HelpWithDebtAndMoney = false,
                HelpWithHealth = false,
                HelpWithMentalHealth = false,
                HelpWithAccessingInternet = false,
                HelpWithHousing = false,
                HelpWithJobsOrTraining = false,
                HelpWithChildrenAndSchools = false,
                HelpWithDisabilities = false,
                HelpWithSomethingElse = false,
                MedicineDeliveryHelpNeeded = false,
                IsPharmacistAbleToDeliver = false,
                WhenIsMedicinesDelivered = "Test",
                NameAddressPharmacist = "Test",
                UrgentEssentials = "Test",
                CurrentSupport = "Test",
                CurrentSupportFeedback = "Test",
                FirstName = "Test",
                LastName = "Test",
                DobMonth = "Test",
                DobYear = "Test",
                DobDay = "Test",
                ContactTelephoneNumber = "Test",
                ContactMobileNumber = "Test",
                EmailAddress = "Test",
                GpSurgeryDetails = "Test",
                NumberOfChildrenUnder18 = "Test",
                ConsentToShare = false,
                DateTimeRecorded = DateTime.Now
            };
            var expected = new Dictionary<string, object> { { "success", true } };
            var response = _classUnderTest.CreateHelpRequest(request) as OkObjectResult;

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(200);
            response.Value.Should().BeEquivalentTo(expected);
        }
    }
}
