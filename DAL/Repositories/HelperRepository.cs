using DAL.Models;
using DAL.Repositories.Interfaces;
using reservation_app_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class HelperRepository<T> : IHelperRepository<T>
    {
        public List<T> CasesHelper(List<T> data,FilterProperties filterProperties)
        {
            if (filterProperties.FilterList.Count > 0)
            {
                foreach (var f in filterProperties.FilterList)
                {
                    switch (f.Type)
                    {
                        case "lookup":
                            data = FilterLookUpField(data, f); break;
                        case "date":
                            data = FilterDateField(data, f); break;
                        case "boolean":
                            data = FilterBooleanField(data, f); break;
                        case "text":
                            data = FilterTextField(data, f); break;
                        case "number":
                            data = FilterNumberField(data, f); break;
                    }
                }

            }

            if (filterProperties.SortList.Count > 0)
            {
                for (int i = filterProperties.SortList.Count - 1; i >= 0; i--)
                {
                    var s = filterProperties.SortList[i];
                    switch (s.Type)
                    {
                        case "lookup":
                            data = SortLookUpField(data, s); break;
                        case "date":
                            data = SortDateField(data, s); break;
                        case "text":
                            data = SortTextField(data, s); break;
                        case "number":
                            data = SortNumberField(data, s); break;
                    }
                }

            }
            return data;
        }
            
        public List<T> FilterLookUpField(List<T> data, Filter filterItem)
        {
                   switch (filterItem.Operator)
                {
                    case "eq":
                    data =
                                data.Where(a => filterItem.ParentAlias == "ActivityDay" ?
                                filterItem.Values.Contains(((IdValuePair)a.GetType().GetProperty(filterItem.FieldName).GetValue(a)).Id)
                               :filterItem.Values.Contains(((IdValuePair)a.GetType().GetProperty(filterItem.ParentAlias).GetValue(a).GetType().GetProperty(filterItem.FieldName).GetValue(a)).Id))
                                    .ToList(); break;
                    case "not-eq":
                    data =
                                data.Where(a => filterItem.ParentAlias == "ActivityDay" ?
                                !filterItem.Values.Contains(((IdValuePair) a.GetType().GetProperty(filterItem.FieldName).GetValue(a)).Id)
                               :!filterItem.Values.Contains(((IdValuePair) a.GetType().GetProperty(filterItem.ParentAlias).GetValue(a).GetType().GetProperty(filterItem.FieldName).GetValue(a)).Id))
                                    .ToList(); break;
                    case "not-null":
                    data =
                            data.Where(a => filterItem.ParentAlias == "ActivityDay" ?
                                ((IdValuePair) a.GetType().GetProperty(filterItem.FieldName).GetValue(a))?.Id != ""
                               :((IdValuePair) a.GetType().GetProperty(filterItem.ParentAlias).GetValue(a).GetType().GetProperty(filterItem.FieldName).GetValue(a))?.Id != "")
                                    .ToList(); break;
                
            }
            return data;
        }
        public List<T> FilterDateField(List<T> data, Filter filterItem)
        {
                switch (filterItem.Operator)
                {
                    case "eq":
                    data = data.Where(a => DateTime.Parse(filterItem.Values[0]) == DateTime.Parse((string)a.GetType().GetProperty(filterItem.FieldName).GetValue(a))).ToList(); break;
                    case "between-dates":
                    data = data.Where(a => DateTime.Parse(filterItem.Values[0]) <= DateTime.Parse((string)a.GetType().GetProperty(filterItem.FieldName).GetValue(a)) && DateTime.Parse(filterItem.Values[1]) >= DateTime.Parse((string)a.GetType().GetProperty(filterItem.FieldName).GetValue(a))).ToList(); break;
                    case "gt":
                    data = data.Where(a => DateTime.Parse(filterItem.Values[0]) < DateTime.Parse((string)a.GetType().GetProperty(filterItem.FieldName).GetValue(a))).ToList(); break;
                    case "lt":
                    data = data.Where(a => DateTime.Parse(filterItem.Values[0]) > DateTime.Parse((string)a.GetType().GetProperty(filterItem.FieldName).GetValue(a))).ToList(); break;
                }

            return data;
        }
        public List<T> FilterBooleanField(List<T> data, Filter filterItem)
        {
            switch (filterItem.Operator)
            {
                case "true":
                    data =
                        data.Where(a => filterItem.ParentAlias == "ActivityDay" ?
                        true == (bool)a.GetType().GetProperty(filterItem.FieldName).GetValue(a)
                       :true == (bool)a.GetType().GetProperty(filterItem.FieldName).GetValue(a))
                            .ToList(); break;
                case "false":
                    data =
                        data.Where(a => filterItem.ParentAlias == "ActivityDay" ?
                        false == (bool)a.GetType().GetProperty(filterItem.FieldName).GetValue(a)
                       :false == (bool)a.GetType().GetProperty(filterItem.ParentAlias).GetValue(a).GetType().GetProperty(filterItem.FieldName).GetValue(a))
                            .ToList(); break;
            }

            return data;
        }
        public List<T> FilterTextField(List<T> data, Filter filterItem)
        {
            switch (filterItem.Operator)
            {
                case "contains":
                    data =
                        data.Where(a => filterItem.ParentAlias == "ActivityDay" ?
                        a.GetType().GetProperty(filterItem.FieldName).GetValue(a).ToString().Contains(filterItem.Values[0])
                       :a.GetType().GetProperty(filterItem.ParentAlias).GetValue(a).GetType().GetProperty(filterItem.FieldName).GetValue(a).ToString().Contains(filterItem.Values[0]))
                            .ToList(); break;
                case "notContains":
                    data =
                       data.Where(a => filterItem.ParentAlias == "ActivityDay" ?
                        ! a.GetType().GetProperty(filterItem.FieldName).GetValue(a).ToString().Contains(filterItem.Values[0])
                       :! a.GetType().GetProperty(filterItem.ParentAlias).GetValue(a).GetType().GetProperty(filterItem.FieldName).GetValue(a).ToString().Contains(filterItem.Values[0]))
                           .ToList(); break;
            }

            return data;
        }
        public List<T> FilterNumberField(List<T> data, Filter filterItem)
        {
            switch (filterItem.Operator)
            {
                case "eq":
                    data = data.Where(a =>  Convert.ToInt32(filterItem.Values[0]) == (int)a.GetType().GetProperty(filterItem.FieldName).GetValue(a)).ToList(); break;
                case "not-eq":
                    data = data.Where(a =>!(Convert.ToInt32(filterItem.Values[0]) == (int)a.GetType().GetProperty(filterItem.FieldName).GetValue(a))).ToList(); break;
                case "gt":
                    data = data.Where(a =>  Convert.ToInt32(filterItem.Values[0]) < (int)a.GetType().GetProperty(filterItem.FieldName).GetValue(a)).ToList(); break;
                case "lt":
                    data = data.Where(a =>  Convert.ToInt32(filterItem.Values[0]) > (int)a.GetType().GetProperty(filterItem.FieldName).GetValue(a)).ToList(); break;
            }

            return data;
        }

        public List<T> SortLookUpField(List<T> data, Sort s)
        {
            switch (s.Direction)
            {
                case "asc":
                    data = data.OrderBy(a => s.Alias == "ActivityDay" ?
                    ((IdValuePair)a.GetType().GetProperty(s.Active).GetValue(a)).Value
                   :((IdValuePair)a.GetType().GetProperty(s.Alias).GetValue(a).GetType().GetProperty(s.Active).GetValue(a)).Value)
                        .ToList(); break;
                case "desc":
                    data = data.OrderByDescending(a => s.Alias == "ActivityDay" ?
                    ((IdValuePair)a.GetType().GetProperty(s.Active).GetValue(a)).Value
                   :((IdValuePair)a.GetType().GetProperty(s.Alias).GetValue(a).GetType().GetProperty(s.Active).GetValue(a)).Value)
                        .ToList(); break;
            }

            return data;
        }

        public List<T> SortDateField(List<T> data, Sort s)
        {
            switch (s.Direction)
            {
                case "asc":
                    data = data.OrderBy(a => s.Alias == "ActivityDay" ?
                    DateTime.Parse((string)a.GetType().GetProperty(s.Active).GetValue(a))
                   :DateTime.Parse((string)a.GetType().GetProperty(s.Alias).GetValue(a).GetType().GetProperty(s.Active).GetValue(a)))
                        .ToList(); break;
                case "desc":
                    data = data.OrderByDescending(a => s.Alias == "ActivityDay" ?
                    DateTime.Parse((string)a.GetType().GetProperty(s.Active).GetValue(a))
                   :DateTime.Parse((string)a.GetType().GetProperty(s.Alias).GetValue(a).GetType().GetProperty(s.Active).GetValue(a)))
                        .ToList(); break;
            }

            return data;
        }


        public List<T> SortTextField(List<T> data, Sort s)
        {
            switch (s.Direction)
            {
                case "asc":
                    data = data.OrderBy(a => s.Alias == "ActivityDay" ?
                    (string)a.GetType().GetProperty(s.Active).GetValue(a)
                   :(string)a.GetType().GetProperty(s.Alias).GetValue(a).GetType().GetProperty(s.Active).GetValue(a))
                        .ToList(); break;
                case "desc":
                    data = data.OrderByDescending(a => s.Alias == "ActivityDay" ?
                    (string)a.GetType().GetProperty(s.Active).GetValue(a)
                   :(string)a.GetType().GetProperty(s.Alias).GetValue(a).GetType().GetProperty(s.Active).GetValue(a))
                        .ToList(); break;
            }

            return data;
        }

        public List<T>? SortNumberField(List<T> data, Sort s)
        {
            switch (s.Direction)
            {
                case "asc":
                    data = data.OrderBy(a => s.Alias == "ActivityDay" ?
                    (int)a.GetType().GetProperty(s.Active).GetValue(a)
                    : (int)a.GetType().GetProperty(s.Alias).GetValue(a).GetType().GetProperty(s.Active).GetValue(a))
                        .ToList(); break;
                case "desc":
                    data = data.OrderByDescending(a => s.Alias == "ActivityDay" ?
                    (int)a.GetType().GetProperty(s.Active).GetValue(a)
                   :(int)a.GetType().GetProperty(s.Alias).GetValue(a).GetType().GetProperty(s.Active).GetValue(a))
                        .ToList(); break;
            }

            return data;
        }
    }
}



