using System;
using System.Linq;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Factories.Commands;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.UseCase.Interfaces;

namespace cv19ResSupportV3.V3.UseCase
{
    public class CreateResidentUseCase : ICreateResidentUseCase
    {
        private IResidentGateway _gateway;
        public CreateResidentUseCase(IResidentGateway gateway)
        {
            _gateway = gateway;
        }
        public Resident Execute(CreateResident command)
        {
            var existingResidentId = _gateway.FindResident(command.ToFindResidentCommand());

            if (existingResidentId != null)
            {
                var existingResident = _gateway.GetResident((int) existingResidentId);
                var updateResident = command.ToPatchResidentCommand();

                string[] telephoneNumbers = { existingResident.ContactTelephoneNumber, updateResident.ContactTelephoneNumber };
                string[] mobileNumbers = { existingResident.ContactMobileNumber, updateResident.ContactMobileNumber };

                updateResident.ContactTelephoneNumber = ConcatPhoneNumber(existingResident.ContactTelephoneNumber, updateResident.ContactTelephoneNumber);
                updateResident.ContactMobileNumber = ConcatPhoneNumber(existingResident.ContactMobileNumber,
                    updateResident.ContactMobileNumber);
                return _gateway.PatchResident((int) existingResidentId, updateResident);
            }
            var resident = _gateway.CreateResident(command);
            return resident;
        }

        private string ConcatPhoneNumber(string existingNumber, string newNumber)
        {
            if (string.IsNullOrEmpty(existingNumber))
            {
                return newNumber;
            }
            else if (string.IsNullOrEmpty(newNumber) || existingNumber.Contains(newNumber))
            {
                return existingNumber;
            }
            return String.Join(" / ", existingNumber, newNumber);
        }
    }
}
