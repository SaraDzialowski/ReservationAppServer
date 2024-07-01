using BL.Services.Interfaces;
using DAL.Models;
using DAL.Repositories.Interfaces;
using reservation_app_server.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class HelperService<T>: IHelperService<T>
    {
        public List<T> CasesHelper(List<T> data, FilterProperties filterProperties, string EntityName)
        {
            if (filterProperties.FilterList.Count > 0)
            {
                foreach (var f in filterProperties?.FilterList)
                {
                    switch (f.Type)
                    {
                        case "lookup":
                            data = FilterLookUpField(data, f, EntityName); break;
                        case "date":
                            data = FilterDateField(data, f, EntityName); break;
                        case "boolean":
                            data = FilterBooleanField(data, f, EntityName); break;
                        case "lookupText":
                            data = FilterLookUpTextField(data, f, EntityName); break;
                        case "text":
                            data = FilterTextField(data, f, EntityName); break;
                        case "number":
                            data = FilterNumberField(data, f, EntityName); break;
                    }
                }

            }

            if (filterProperties.SortList?.Count > 0)
            {
                for (int i = filterProperties.SortList.Count - 1; i >= 0; i--)
                {
                    var s = filterProperties.SortList[i];
                    switch (s.Type)
                    {
                        case "lookup":
                            data = SortLookUpField(data, s, EntityName); break;
                        case "date":
                            data = SortDateField(data, s, EntityName); break;
                        case "boolean":
                            data = SortBooleanField(data, s, EntityName); break;
                        case "text":
                            data = SortTextField(data, s, EntityName); break;
                        case "number":
                            data = SortNumberField(data, s, EntityName); break;
                    }
                }

            }
            return data;
        }
        public List<T> FilterLookUpField(List<T> data, Filter filterItem, string EntityName)
        {
            switch (filterItem.Operator)
            {
                case "eq":
                    data =
                                data.Where(a => filterItem.ParentAlias == EntityName ?
                                filterItem.Values.Contains(((IdValuePair)a.GetType().GetProperty(filterItem.FieldName).GetValue(a)).Id)
                               : filterItem.Values.Contains(
                                   ((IdValuePair)a.GetType().GetProperty(filterItem.ParentAlias).GetValue(a).GetType().GetProperty(filterItem.FieldName).GetValue(a.GetType().GetProperty(filterItem.ParentAlias).GetValue(a))).Id))
                                    .ToList(); break;
                case "not-eq":
                    data =
                                data.Where(a => filterItem.ParentAlias == EntityName ?
                                !filterItem.Values.Contains(((IdValuePair)a.GetType().GetProperty(filterItem.FieldName).GetValue(a)).Id)
                               : !filterItem.Values.Contains(((IdValuePair)a.GetType().GetProperty(filterItem.ParentAlias).GetValue(a).GetType().GetProperty(filterItem.FieldName).GetValue(a.GetType().GetProperty(filterItem.ParentAlias).GetValue(a))).Id))
                                    .ToList(); break;
                case "not-null":
                    data =
                            data.Where(a => filterItem.ParentAlias == EntityName ?
                                ((IdValuePair)a.GetType().GetProperty(filterItem.FieldName).GetValue(a))?.Id != ""
                               : ((IdValuePair)a.GetType().GetProperty(filterItem.ParentAlias).GetValue(a).GetType().GetProperty(filterItem.FieldName).GetValue(a.GetType().GetProperty(filterItem.ParentAlias).GetValue(a)))?.Id != "")
                                    .ToList(); break;

            }
            return data;
        }
        public List<T> FilterDateField(List<T> data, Filter filterItem, string EntityName)
        {
            DateTime parsedDateTime;
            if (DateTime.TryParseExact(filterItem.Values[0], "ddd MMM dd yyyy HH:mm:ss 'GMT'zzz '(שעון ישראל (קיץ))'", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDateTime))
            {
                switch (filterItem.Operator)
                {
                    case "eq":
                        data = data.Where(a => DateTime.Parse(filterItem.Values[0]) == DateTime.Parse((string)a.GetType().GetProperty(filterItem.FieldName).GetValue(a))).ToList(); break;
                    case "between-dates":
                        data = data.Where(a => parsedDateTime <= DateTime.ParseExact((string)a.GetType().GetProperty(filterItem.FieldName).GetValue(a), "ddd MMM dd yyyy HH:mm:ss 'GMT'zzz '(שעון ישראל (קיץ))'", CultureInfo.InvariantCulture) && DateTime.ParseExact(filterItem.Values[1], "ddd MMM dd yyyy HH:mm:ss 'GMT'zzz '(שעון ישראל (קיץ))'", CultureInfo.InvariantCulture) >= DateTime.ParseExact((string)a.GetType().GetProperty(filterItem.FieldName).GetValue(a), "ddd MMM dd yyyy HH:mm:ss 'GMT'zzz '(שעון ישראל (קיץ))'", CultureInfo.InvariantCulture)).ToList(); break;
                    case "gt":
                        data = data.Where(a => DateTime.Parse(filterItem.Values[0]) < DateTime.Parse((string)a.GetType().GetProperty(filterItem.FieldName).GetValue(a))).ToList(); break;
                    case "lt":
                        data = data.Where(a => DateTime.Parse(filterItem.Values[0]) > DateTime.Parse((string)a.GetType().GetProperty(filterItem.FieldName).GetValue(a))).ToList(); break;
                }
            }

            return data;
        }
        public List<T> FilterBooleanField(List<T> data, Filter filterItem, string EntityName)
        {
            switch (filterItem.Operator)
            {
                case "eq":
                    data =
                        data.Where(a => filterItem.ParentAlias == EntityName ?
                        filterItem.Values.Contains(a.GetType().GetProperty(filterItem.FieldName).GetValue(a).ToString().ToLowerInvariant())
                       : filterItem.Values.Contains(a.GetType().GetProperty(filterItem.ParentAlias).GetValue(a).GetType().GetProperty(filterItem.FieldName).GetValue(a.GetType().GetProperty(filterItem.ParentAlias).GetValue(a).ToString())))
                            .ToList(); break;
                case "not-eq":
                    data =
                        data =
                        data.Where(a => filterItem.ParentAlias == EntityName ?
                        !filterItem.Values.Contains((string)a.GetType().GetProperty(filterItem.FieldName).GetValue(a))
                       :!filterItem.Values.Contains((string)a.GetType().GetProperty(filterItem.ParentAlias).GetValue(a).GetType().GetProperty(filterItem.FieldName).GetValue(a.GetType().GetProperty(filterItem.ParentAlias).GetValue(a))))
                            .ToList(); break;
            }

            return data;
        }
        public List<T> FilterLookUpTextField(List<T> data, Filter filterItem, string EntityName)
        {

            switch (filterItem.Operator)
            {
                case "contains":
                    data =
                        data.Where(a => filterItem.ParentAlias == EntityName ?
                        ((IdValuePair)a.GetType().GetProperty(filterItem.FieldName).GetValue(a)).Value.Contains(filterItem.Values[0])
                       : a.GetType().GetProperty(filterItem.ParentAlias).GetValue(a).GetType().GetProperty(filterItem.FieldName).GetValue(a.GetType().GetProperty(filterItem.ParentAlias).GetValue(a)).ToString().Contains(filterItem.Values[0]))
                            .ToList(); break;
                case "notContains":
                    data =
                       data.Where(a => filterItem.ParentAlias == EntityName ?
                        !((IdValuePair)a.GetType().GetProperty(filterItem.FieldName).GetValue(a)).Value.Contains(filterItem.Values[0])
                       : !a.GetType().GetProperty(filterItem.ParentAlias).GetValue(a).GetType().GetProperty(filterItem.FieldName).GetValue(a.GetType().GetProperty(filterItem.ParentAlias).GetValue(a)).ToString().Contains(filterItem.Values[0]))
                           .ToList(); break;

            }
            return data;
        }
        public List<T> FilterTextField(List<T> data, Filter filterItem, string EntityName)
        {
            switch (filterItem.Operator)
            {
                case "contains":
                    data =
                        data.Where(a => filterItem.ParentAlias == EntityName ?
                        a.GetType().GetProperty(filterItem.FieldName).GetValue(a).ToString().Contains(filterItem.Values[0])
                       : a.GetType().GetProperty(filterItem.ParentAlias).GetValue(a).GetType().GetProperty(filterItem.FieldName).GetValue(a.GetType().GetProperty(filterItem.ParentAlias).GetValue(a)).ToString().Contains(filterItem.Values[0]))
                            .ToList(); break;
                case "notContains":
                    data =
                       data.Where(a => filterItem.ParentAlias == EntityName  ?
                        !a.GetType().GetProperty(filterItem.FieldName).GetValue(a).ToString().Contains(filterItem.Values[0])
                       : !a.GetType().GetProperty(filterItem.ParentAlias).GetValue(a).GetType().GetProperty(filterItem.FieldName).GetValue(a.GetType().GetProperty(filterItem.ParentAlias).GetValue(a)).ToString().Contains(filterItem.Values[0]))
                           .ToList(); break;
            }

            return data;
        }
        public List<T> FilterNumberField(List<T> data, Filter filterItem, string EntityName)
        {
            switch (filterItem.Operator)
            {
                case "eq":
                    data = data.Where(a => Convert.ToDouble(filterItem.Values[0]) == (double)a.GetType().GetProperty(filterItem.FieldName).GetValue(a)).ToList(); break;
                case "not-eq":
                    data = data.Where(a => !(Convert.ToInt32(filterItem.Values[0]) == (int)a.GetType().GetProperty(filterItem.FieldName).GetValue(a))).ToList(); break;
                case "gt":
                    data = data.Where(a => Convert.ToInt32(filterItem.Values[0]) < (int)a.GetType().GetProperty(filterItem.FieldName).GetValue(a)).ToList(); break;
                case "lt":
                    data = data.Where(a => Convert.ToInt32(filterItem.Values[0]) > (int)a.GetType().GetProperty(filterItem.FieldName).GetValue(a)).ToList(); break;
            }

            return data;
        }
        public List<ReservationListItem> FilterAnyFieldReservation(List<ReservationListItem> data, Filter filterItem)
        {
            string onSearch = filterItem.Values[0].ToLower();
            data = data.Where(item =>
                 (item != null) &&
                 (item.ReservationNum.ToLower().Contains(onSearch.ToLower()) ||
                 item.Institution.Value.ToLower().Contains(onSearch.ToLower()) ||
                 item.GroupName.ToLower().Contains(onSearch.ToLower()) ||
                 item.CreatedOn.ToLower().Contains(onSearch.ToLower()) ||
                 item.CreatedBy.Value.ToLower().Contains(onSearch.ToLower()) ||
                 item.NumberOfGroups.ToString().ToLower().Contains(onSearch.ToLower()) ||
                 item.Coordinator.Value.ToString().ToLower().Contains(onSearch.ToLower()) ||
                 item.Location.Value.ToString().ToLower().Contains(onSearch.ToLower()) ||
                 item.ActivityDayFirstDate.ToLower().Contains(onSearch.ToLower()) ||
                 item.BudgetFramework.Value.ToLower().Contains(onSearch.ToLower()) ||
                 item.InvoiceList[0].ToString().ToLower().Contains(onSearch.ToLower()) ||
                 (item.IsOrderConfirmed != null && item.IsOrderConfirmed.ToString().ToLower().Contains(onSearch.ToLower())) ||
                 item.PaymentStatus.Value.ToLower().Contains(onSearch.ToLower()) ||
                 item.ReservationStatus.Value.ToLower().Contains(onSearch.ToLower()))).ToList();           
            return data;
        }
        public List<ActivityDayListItem> FilterAnyFieldActivityDay(List<ActivityDayListItem> data, Filter filterItem)
        {
            string onSearch = filterItem.Values[0].ToLower();
            data = data.Where(item =>
                 (item != null && item.Reservation != null) && 
                 (item.Reservation.ReservationNum.ToLower().Contains(onSearch.ToLower()) ||
                 //item.Reservation.CreatedOn.ToLower().Contains(onSearch.ToLower()) ||
                 item.Reservation.GroupName.ToLower().Contains(onSearch.ToLower()) ||
                 item.Reservation.FullNameContact.ToLower().Contains(onSearch.ToLower()) ||
                 item.StartDate.ToLower().Contains(onSearch.ToLower()) ||
                 item.StartTime.ToString().ToLower().Contains(onSearch.ToLower()) ||
                 item.EndTime.ToString().ToLower().Contains(onSearch.ToLower()) ||
                 item.Reservation.NumberOfGroups.ToString().ToLower().Contains(onSearch.ToLower()) ||
                 item.Reservation.NumberOfVisitors.ToString().ToLower().Contains(onSearch.ToLower()) ||
                 item.Location.Value.ToLower().Contains(onSearch.ToLower()) ||
                 item.Reservation.Language.Value.ToString().ToLower().Contains(onSearch.ToLower()) ||
                 item.Reservation.BudgetFramework.Value.ToString().ToLower().Contains(onSearch.ToLower()) ||
                 //item.Reservation.ResponsibleDivision.Value.ToString().ToLower().Contains(onSearch.ToLower()) ||
                 item.Reservation.Coordinator.Value.ToString().ToLower().Contains(onSearch.ToLower()) ||
                 item.Reservation.IsOrderConfirmed.ToString().ToLower().Contains(onSearch.ToLower()) ||
                 item.Reservation.IsApprovedByCoordinator.ToString().ToLower().Contains(onSearch.ToLower()) ||
                 item.Reservation.PaymentStatus.Value.ToString().ToLower().Contains(onSearch.ToLower()) ||
                 item.ActivityDayStatus.Value.ToString().ToLower().Contains(onSearch.ToLower()) ||
                 //item.Reservation.InvoiceList[0].ToString().ToLower().Contains(onSearch.ToLower())||
                 item.Reservation.CreatedBy.Value.ToString().ToLower().Contains(onSearch.ToLower()) ||
                 item.Reservation.Institution.Value.ToString().ToLower().Contains(onSearch.ToLower()))).ToList();
            return data;
        }

        public List<T> SortLookUpField(List<T> data, Sort s, string EntityName)
        {
            switch (s.Direction)
            {
                case "asc":
                    data = data.OrderBy(a => s.Alias == EntityName ?
                    ((IdValuePair)a.GetType().GetProperty(s.Active).GetValue(a)).Value
                   : ((IdValuePair)a.GetType().GetProperty(s.Alias).GetValue(a).GetType().GetProperty(s.Active).GetValue(a.GetType().GetProperty(s.Alias).GetValue(a))).Value)
                        .ToList(); break;
                case "desc":
                    data = data.OrderByDescending(a => s.Alias == EntityName ?
                    ((IdValuePair)a.GetType().GetProperty(s.Active).GetValue(a)).Value
                   : ((IdValuePair)a.GetType().GetProperty(s.Alias).GetValue(a).GetType().GetProperty(s.Active).GetValue(a.GetType().GetProperty(s.Alias).GetValue(a))).Value)
                        .ToList(); break;
            }

            return data;
        }

        public List<T> SortDateField(List<T> data, Sort s, string EntityName)
        {
            switch (s.Direction)
            {
                case "asc":
                    data = data.OrderBy(a => s.Alias == EntityName ?
                    DateTime.Parse((string)a.GetType().GetProperty(s.Active).GetValue(a))
                   : DateTime.Parse((string)a.GetType().GetProperty(s.Alias).GetValue(a).GetType().GetProperty(s.Active).GetValue(a.GetType().GetProperty(s.Alias).GetValue(a))))
                        .ToList(); break;
                case "desc":
                    data = data.OrderByDescending(a => s.Alias == EntityName ?
                    DateTime.Parse((string)a.GetType().GetProperty(s.Active).GetValue(a))
                   : DateTime.Parse((string)a.GetType().GetProperty(s.Alias).GetValue(a).GetType().GetProperty(s.Active).GetValue(a.GetType().GetProperty(s.Alias).GetValue(a))))
                        .ToList(); break;
            }

            return data;
        }

        public List<T> SortBooleanField(List<T> data, Sort s, string EntityName)
        {
            switch (s.Direction)
            {
                case "asc":
                    data = data.OrderBy(a => s.Alias == EntityName ?
                    ((bool)a.GetType().GetProperty(s.Active).GetValue(a))
                   :((bool)a.GetType().GetProperty(s.Alias).GetValue(a).GetType().GetProperty(s.Active).GetValue(a.GetType().GetProperty(s.Alias).GetValue(a))))
                        .ToList(); break;
                case "desc":
                    data = data.OrderByDescending(a => s.Alias == EntityName ?
                   ((bool)a.GetType().GetProperty(s.Active).GetValue(a))
                   :((bool)a.GetType().GetProperty(s.Alias).GetValue(a).GetType().GetProperty(s.Active).GetValue(a.GetType().GetProperty(s.Alias).GetValue(a))))
                        .ToList(); break;
            }

            return data;
        }
        public List<T> SortTextField(List<T> data, Sort s, string EntityName)
        {
            switch (s.Direction)
            {
                case "asc":
                    data = data.OrderBy(a => s.Alias == EntityName ?
                    (string)a.GetType().GetProperty(s.Active).GetValue(a)
                   : (string)a.GetType().GetProperty(s.Alias).GetValue(a).GetType().GetProperty(s.Active).GetValue(a.GetType().GetProperty(s.Alias).GetValue(a)))
                        .ToList(); break;
                case "desc":
                    data = data.OrderByDescending(a => s.Alias == EntityName ?
                    (string)a.GetType().GetProperty(s.Active).GetValue(a)
                   : (string)a.GetType().GetProperty(s.Alias).GetValue(a).GetType().GetProperty(s.Active).GetValue(a.GetType().GetProperty(s.Alias).GetValue(a)))
                        .ToList(); break;
            }

            return data;
        }

        public List<T>? SortNumberField(List<T> data, Sort s, string EntityName)
        {
            switch (s.Direction)
            {
                case "asc":
                    data = data.OrderBy(a => s.Alias == EntityName ?
                    (int)a.GetType().GetProperty(s.Active).GetValue(a)
                    : (int)a.GetType().GetProperty(s.Alias).GetValue(a).GetType().GetProperty(s.Active).GetValue(a.GetType().GetProperty(s.Alias).GetValue(a)))
                        .ToList(); break;
                case "desc":
                    data = data.OrderByDescending(a => s.Alias == EntityName ?
                    (int)a.GetType().GetProperty(s.Active).GetValue(a)
                   : (int)a.GetType().GetProperty(s.Alias).GetValue(a).GetType().GetProperty(s.Active).GetValue(a.GetType().GetProperty(s.Alias).GetValue(a)))
                        .ToList(); break;
            }

            return data;
        }


    }
}



