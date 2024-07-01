using DAL.Models;
using System;
using System.Diagnostics.CodeAnalysis;

namespace reservation_app_server.Models
{
    public class BasicReservationItem : BasicItem
    {
        public string ReservationNum { get; set; }
        public string ModifiedOn { get; set; }
        public IdValuePair CreatedBy { get; set; }
        public string GroupName { get; set; }
        public IdValuePair ReservationStatus { get; set; }
        public IdValuePair Coordinator { get; set; }
        public int NumberOfGroups { get; set; }
        public int NumberOfVisitors { get; set; }
        public bool IsOrderConfirmed { get; set; }
        public IdValuePair Language { get; set; }
        public IdValuePair PaymentStatus { get; set; }
        public string InternalComments { get; set; }
        public IdValuePair ResponsibleDivision { get; set; }
        public string DivisionRouter { get; set; }
        public IdValuePair Institution { get; set; }
        public string FullNameContact { get; set; }
        public bool IsApprovedByCoordinator { get; set; }
        public IdValuePair BudgetFramework { get; set; }
        public bool IsInstitutionHaredi { get; set; }
        [AllowNull]
        public IdValuePair AdditionalCharacterization { get; set; }
        public bool IsReservationLocked { get; set; }
        public string CreatedOn { get; set; }
        public string[] InvoiceList { get; set; }
        public IdValuePair Location { get; set; }
        public string ActivityDayFirstDate { get; set; }
        public int TotalPrice { get; set; }
        public DiscountType DiscountType { get; set; }
        public int Discount { get; set; }
        public int TotalPriceAfterDiscount { get; set; }
    }
   public class FullReservationItem : BasicReservationItem { 
    
    public List<Invoice> FullInvoiceList { get; set; }
    
    }

}
