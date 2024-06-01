using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Undersoft.SDK.Benchmarks.Updating.Models
{
    [Description("Tabela przechowująca zdarzenia związane ze datasetami użytkownika")]
    [Table("UserDataSets", Schema = "Events")]
    public class UserProfileEvent
    {
        [Description("Identyfikator zdarzenia")]
        [Required]
        public Guid EventId { get; set; }

        [Description("Identyfikator DateSet'u")]
        public int DataSetId { get; set; }

        [Description("Identyfikator użytkownika")]
        public Guid UserId { get; set; }

        [Description("Nazwa typu zdarzenia")]
        [Required]
        public string TypeName { get; set; }

        [Description("Dane zdarzenia w formacie json")]
        [Required]
        public string Data { get; set; }

        [Description("Object wystąpienia zdarzenia")]
        [Column(TypeName = "datetime2")]
        public DateTime OccuredOn { get; set; }

        [Description("Object przeliczenia metryczki")]
        [Column(TypeName = "datetime2")]
        [ConcurrencyCheck]
        public DateTime? CalculatedOn { get; set; }

        [Description("Ilość prób ponownego przeliczenia metryczki, w przypadku błędu")]
        public int CalculationRetryAttempts { get; set; }
    }
}
