using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class BookingHelperTests
    {
        private Booking _existingBooking;
        private Mock<IBookingsRepository> _repository;

        [SetUp]
        public void SetUp()
        {
            _existingBooking = new Booking
            {
                Id = 2,
                ArrivalDate = ArrivedOn(2017, 1, 15),
                DepartureDate = DepartureOn(2017, 1, 20),
                Reference = "a"
            };
            
            _repository = new Mock<IBookingsRepository>();
            _repository.Setup(r => r.GetActiveBookings(1)).Returns(new List<Booking> { _existingBooking }.AsQueryable());
        }
        
        [Test]
        public void OverlappingBookingsExist_BookingStartAndFinishesBeforeAnExistingBooking_ReturnEmptyString()
        {
            var booking = new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate, 2),
                DepartureDate = Before(_existingBooking.ArrivalDate)
            };

            var result = BookingHelper.OverlappingBookingsExist(booking, _repository.Object);
            
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartBeforeAndFinishesInTheMiddleOfAnExistingBooking_ReturnExistingBookingReference()
        {
            var booking = new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.ArrivalDate)
            };

            var result = BookingHelper.OverlappingBookingsExist(booking, _repository.Object);
            
            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartBeforeAndFinishesAfterAnExistingBooking_ReturnExistingBookingReference()
        {
            var booking = new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate)
            };

            var result = BookingHelper.OverlappingBookingsExist(booking, _repository.Object);
            
            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartAndFinishesInTheMiddleOfAnExistingBooking_ReturnExistingBookingReference()
        {
            var booking = new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = Before(_existingBooking.DepartureDate)
            };

            var result = BookingHelper.OverlappingBookingsExist(booking, _repository.Object);
            
            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartInTheMiddleOfAnExistingBookingAndFinishesAfterAnExistingBooking_ReturnExistingBookingReference()
        {
            var booking = new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate)
            };

            var result = BookingHelper.OverlappingBookingsExist(booking, _repository.Object);
            
            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartAndFinishesAfterAnExistingBooking_ReturnEmptyString()
        {
            var booking = new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.DepartureDate),
                DepartureDate = After(_existingBooking.DepartureDate, 2)
            };

            var result = BookingHelper.OverlappingBookingsExist(booking, _repository.Object);
            
            Assert.That(result, Is.Empty);
        }
        
        [Test]
        public void OverlappingBookingsExist_BookingOverlapButNewBookingIsCancelled_ReturnEmptyString()
        {
            var booking = new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate, 2),
                Status = "Cancelled"
            };

            var result = BookingHelper.OverlappingBookingsExist(booking, _repository.Object);
            
            Assert.That(result, Is.Empty);
        }

        private DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(-days);
        }
        
        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(days);
        }
        
        private DateTime ArrivedOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 14, 0, 0);
        }

        private DateTime DepartureOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 14, 0, 0);
        }
    }
}