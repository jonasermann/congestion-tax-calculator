using congestion.calculator;
using Microsoft.AspNetCore.Mvc;

namespace CongestionTaxCalculatorApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CongestionTaxCalculatorController : ControllerBase
{
    private Dictionary<string, Vehicle> _vehicles = new Dictionary<string, Vehicle>()
    {
        {"Car", new Car()},
        {"Motorcycle", new Motorcycle()},
        {"Tractor", new Tractor()},
        {"Emergency", new Emergency()},
        {"Diplomat", new Diplomat()},
        {"Foreign", new Foreign()},
        {"Military", new Military()},
    };

    [HttpPost]
    public int CongestionTax(TaxCalculationDTO info)
    {
        var congestionTaxCalculator = new CongestionTaxCalculator();
        var vehicle = info.vehicle;

        if (_vehicles.ContainsKey(vehicle))
        {
            return congestionTaxCalculator.GetTax(_vehicles[vehicle], info.dates);
        }

        return congestionTaxCalculator.GetTax(null, info.dates);
    }
}
