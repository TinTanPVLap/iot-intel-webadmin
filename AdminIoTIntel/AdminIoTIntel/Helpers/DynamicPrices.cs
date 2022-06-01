using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminiotintel.Helpers
{
    public class DynamicPrices
    {
    }
    public class Spec1Spec2
    {
        public decimal SpecID1 { get; set; }
        public decimal SpecID2 { get; set; }
    }

    public class Spec1Spec2Spec3
    {
        public decimal SpecID1 { get; set; }
        public decimal SpecID2 { get; set; }
        public decimal SpecID3 { get; set; }
    }
    public class Spec1Spec2Spec3Production
    {
        public decimal SpecID1 { get; set; }
        public decimal SpecID2 { get; set; }
        public decimal SpecID3 { get; set; }
        public decimal ProductionID { get; set; }
    }
    public class Spec1Spec2Spec3ProductionStore
    {
        public decimal SpecID1 { get; set; }
        public decimal SpecID2 { get; set; }
        public decimal SpecID3 { get; set; }
        public decimal ProductionID { get; set; }
        public decimal StoreID { get; set; }
    }

}