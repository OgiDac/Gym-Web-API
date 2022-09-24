using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeretanaWebApi.Models
{
    public class EditFitnessCenter
    {
        private string id;
        private string centerName;
        private string address;
        private int? openingYear;
        private bool deleted;
        private int? monthlyMembership;
        private int? annualMembership;
        private int? priceOfOneTraining;
        private int? priceOfOneGroupTraining;
        private int? priceOfOnePersonalTraining;

        public EditFitnessCenter(string id, string centerName, string address, int openingYear, bool deleted, int monthlyMembership, int annualMembership, int priceOfOneTraining, int priceOfOneGroupTraining, int priceOfOnePersonalTraining)
        {
            this.Id = id;
            this.CenterName = centerName;
            this.Address = address;
            this.OpeningYear = openingYear;
            this.Deleted = deleted;
            this.MonthlyMembership = monthlyMembership;
            this.AnnualMembership = annualMembership;
            this.PriceOfOneTraining = priceOfOneTraining;
            this.PriceOfOneGroupTraining = priceOfOneGroupTraining;
            this.PriceOfOnePersonalTraining = priceOfOnePersonalTraining;
        }

        public string Id { get => id; set => id = value; }
        public string CenterName { get => centerName; set => centerName = value; }
        public string Address { get => address; set => address = value; }
        public int? OpeningYear { get => openingYear; set => openingYear = value; }
        public bool Deleted { get => deleted; set => deleted = value; }
        public int? MonthlyMembership { get => monthlyMembership; set => monthlyMembership = value; }
        public int? AnnualMembership { get => annualMembership; set => annualMembership = value; }
        public int? PriceOfOneTraining { get => priceOfOneTraining; set => priceOfOneTraining = value; }
        public int? PriceOfOneGroupTraining { get => priceOfOneGroupTraining; set => priceOfOneGroupTraining = value; }
        public int? PriceOfOnePersonalTraining { get => priceOfOnePersonalTraining; set => priceOfOnePersonalTraining = value; }
    }
}