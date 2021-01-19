using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeeWeb.Entities
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
    }
}