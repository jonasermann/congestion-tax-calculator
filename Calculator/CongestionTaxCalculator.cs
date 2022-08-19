using System;
using System.Collections.Generic;
using congestion.calculator;
using System.Linq;

public class CongestionTaxCalculator
{
    /**
         * Calculate the total toll fee for one day
         *
         * @param vehicle - the vehicle
         * @param dates   - date and time of all passes on one day
         * @return - the total congestion tax for that day
         */

    public int GetTax(Vehicle vehicle, DateTime[] dates)
    {
        if(vehicle == null) return 0;

        var arrayOfDates = dates.Select(d => new List<DateTime>()).ToArray();
        var intervallStart = (dates[0].Hour * 60) + dates[0].Minute;
        var index = 0;

        for(int i = 0; i < dates.Length; i++)
        {
            var dateTime = (dates[i].Hour * 60) + dates[i].Minute;
            if (dateTime <= intervallStart + 60)
            {
                arrayOfDates[index].Add(dates[i]);
            }
            else
            {
                intervallStart = dateTime;
                ++index;
                arrayOfDates[index].Add(dates[i]);
            }
        }

        arrayOfDates = arrayOfDates.Where(d => d.Count > 0).ToArray();
        int totalFee = arrayOfDates.Sum(l => l.Select(d => GetTollFee(d, vehicle)).Max());
        
        if (totalFee > 60) totalFee = 60;
        return totalFee;
    }

    private bool IsTollFreeVehicle(Vehicle vehicle)
    {
        String vehicleType = vehicle.GetVehicleType();
        return vehicleType.Equals(TollFreeVehicles.Motorcycle.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Tractor.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Emergency.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Diplomat.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Foreign.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Military.ToString());
    }

    public int GetTollFee(DateTime date, Vehicle vehicle)
    {
        if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

        int hour = date.Hour;
        int minute = date.Minute;

        if (hour == 6 && minute >= 0 && minute <= 29) return 8;
        else if (hour == 6 && minute >= 30 && minute <= 59) return 13;
        else if (hour == 7 && minute >= 0 && minute <= 59) return 18;
        else if (hour == 8 && minute >= 0 && minute <= 29) return 13;
        else if (hour == 8 && minute >= 30 && minute <= 59) return 8;
        else if (hour >= 9 && hour <= 14) return 8;
        else if (hour == 15 && minute >= 0 && minute <= 29) return 13;
        else if (hour == 15 && minute >= 0 || hour == 16 && minute <= 59) return 18;
        else if (hour == 17 && minute >= 0 && minute <= 59) return 13;
        else if (hour == 18 && minute >= 0 && minute <= 29) return 8;
        else return 0;
    }

    private Boolean IsTollFreeDate(DateTime date)
    {
        int year = date.Year;
        int month = date.Month;
        int day = date.Day;

        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

        if (year == 2013)
        {
            if ((month == 1 && day == 1) ||
                (month == 3 && (day == 28 || day == 29)) ||
                (month == 4 && (day == 1 || day == 30)) ||
                (month == 5 && (day == 1 || day == 8 || day == 9)) ||
                (month == 6 && (day == 5 || day == 6 || day == 21)) ||
                (month == 7) ||
                (month == 11 && day == 1) ||
                (month == 12 && (day == 24 || day == 25 || day == 26 || day == 31)))
            {
                return true;
            }
        }
        return false;
    }

    private enum TollFreeVehicles
    {
        Motorcycle = 0,
        Tractor = 1,
        Emergency = 2,
        Diplomat = 3,
        Foreign = 4,
        Military = 5
    }
}