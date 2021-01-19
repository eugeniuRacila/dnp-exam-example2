using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DNP_EXAM_EXAMPLE_2.Entities
{
    public class Employee
    {
        [JsonPropertyName("id")]

        public int Id { get; set; }
        
        [Required]
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [Required]
        [JsonPropertyName("hourlyWage")]

        public double HourlyWage { get; set; }
        
        [Required]
        [JsonPropertyName("hoursPerMonth")]
        public double HoursPerMonth { get; set; }

        public Employee(string name, double hourlyWage, double hoursPerMonth)
        {
            Name = name;
            HourlyWage = hourlyWage;
            HoursPerMonth = hoursPerMonth;
        }

        public double GetMonthlyPay()
        {
            double monthlyPay = HourlyWage * HoursPerMonth;

            if (HoursPerMonth > 150)
                monthlyPay += (HoursPerMonth - 150) * (HourlyWage / 2); 

            return monthlyPay;
        }
    }
}