using congestion.calculator;
using System.ComponentModel.DataAnnotations;

namespace CongestionTaxCalculatorApi
{
    public class TaxCalculationDTO
    {
        [Required]
        public string vehicle { get; set; }

        [Required]
        public DateTime[] dates { get; set; }
    }
}
