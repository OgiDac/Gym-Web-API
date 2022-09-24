using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeretanaWebApi.Models
{
    public class FitnessCenter
    {

        private string id;
        private string centerName;
        private string address;
        private int openingYear;
        private bool deleted;
        private string owner;
        private int monthlyMembership;
        private int annualMembership;
        private int priceOfOneTraining;
        private int priceOfOneGroupTraining;
        private int priceOfOnePersonalTraining;
        

        public FitnessCenter(string id, bool deleted, string centerName, string address, int openingYear, string owner, int monthlyMembership, int annualMembership, int priceOfOneTraining, int priceOfOneGroupTraining, int priceOfOnePersonalTraining)
        {
            if (id != null && id.Length > 0)
            {
                this.id = id;
            }
            else {
                GenerateId(); 
            }
            this.CenterName = centerName;
            this.Deleted = deleted;
            this.Address = address;
            this.OpeningYear = openingYear;
            this.Owner = owner;
            this.MonthlyMembership = monthlyMembership;
            this.AnnualMembership = annualMembership;
            this.PriceOfOneTraining = priceOfOneTraining;
            this.PriceOfOneGroupTraining = priceOfOneGroupTraining;
            this.PriceOfOnePersonalTraining = priceOfOnePersonalTraining;
        }

        public string Id { get => id; set => id = value; }
        public bool Deleted { get => deleted; set => deleted = value; }
        public string CenterName { get => centerName; set => centerName = value; }
        public string Address { get => address; set => address = value; }
        public int OpeningYear { get => openingYear; set => openingYear = value; }
        public string Owner { get => owner; set => owner = value; }
        public int MonthlyMembership { get => monthlyMembership; set => monthlyMembership = value; }
        public int AnnualMembership { get => annualMembership; set => annualMembership = value; }
        public int PriceOfOneTraining { get => priceOfOneTraining; set => priceOfOneTraining = value; }
        public int PriceOfOneGroupTraining { get => priceOfOneGroupTraining; set => priceOfOneGroupTraining = value; }
        public int PriceOfOnePersonalTraining { get => priceOfOnePersonalTraining; set => priceOfOnePersonalTraining = value; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        private void GenerateId()
        {
           this.id =  DateTimeOffset.Now.ToUnixTimeSeconds().ToString();

        }
    }
}