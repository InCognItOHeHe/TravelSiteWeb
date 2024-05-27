using FluentValidation;
using RepositoryUsingEFinMVC.Repository;
using TravelSiteWeb.Models;

namespace TravelSiteWeb.Services
{
    public class ReservationValidator : AbstractValidator<Reservations>
    {
        private readonly IClientsRepository _clientsRepository;
        private readonly IReservationsRepository _reservationsRepository;

        public ReservationValidator(IClientsRepository clientRepository, IReservationsRepository reservationRepository)
        {
            _clientsRepository = clientRepository;
            _reservationsRepository = reservationRepository;

            RuleFor(x => x.ReservationDate).NotEmpty();
            RuleFor(x => x.Cost).NotNull();
            RuleFor(x => x.Contact).Length(0, 255);
            RuleFor(x => x.Adress).Length(0, 255);
            RuleFor(x => x.ClientsID).Must(BeExistingClientID).WithMessage("Provided ClientID doesn't exist");
            RuleFor(x => x.ReservationsID).Must(BeExistingTravelDestinationID).WithMessage("Provided TravelDestinationID doesn't exist");
        }

        public bool BeExistingClientID(int clientID)
        {
            var existingClientID = _clientsRepository.GetById(clientID);
            return existingClientID != null;
        }

        public bool BeExistingTravelDestinationID(int travelDestinationID)
        {
            var existingTravelDestinationID = _reservationsRepository.GetById(travelDestinationID);
            return existingTravelDestinationID != null;
        }
    }
}