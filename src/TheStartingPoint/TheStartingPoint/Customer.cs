using System;
using System.Collections.Generic;

namespace TheStartingPoint
{
    public class Customer
    {
        public string Name { get; private set; }
        private readonly IList<Rental> _rentals = new List<Rental>();
        public Customer(string name)
        {
            Name = name;
        }
        public void AddRental(Rental rental)
        {
            _rentals.Add(rental);
        }

        public String Statement()
        {
            double totalAmount = 0F;
            int frequentRenterPoints = 0;

            String result = "Rental record for " + Name + "\n";

            // determine amounts for each line
            foreach (var rental in _rentals)
            {
                double thisAmount = 0f;

                switch (rental.Movie.PriceCode)
                {
                    case Movie.Regular:
                        thisAmount += 2;
                        if (rental.DaysRented > 2)
                            thisAmount += (rental.DaysRented - 2) * 1.5;
                        break;
                    case Movie.Childrens:
                        thisAmount += 1.5;
                        if (rental.DaysRented > 3)
                            thisAmount += (rental.DaysRented - 3) * 1.5;
                        break;
                    case Movie.NewRelease:
                        thisAmount += rental.DaysRented * 3;
                        break;
                }
                //add frequent renter points
                frequentRenterPoints++;
                // add bonus for a tow day new release rental
                if (rental.Movie.PriceCode == Movie.NewRelease && rental.DaysRented > 1)
                    frequentRenterPoints++;

                // show figures for this rental
                result += "\t" + rental.Movie.Title +
                          "\t" + thisAmount + "\n";
                totalAmount += thisAmount;
            }

            // add footer lines
            result += "Amount owed is " + totalAmount + "\n";
            result += "You earned " + frequentRenterPoints + " frequent renter points";

            return result;
        }
    }
}