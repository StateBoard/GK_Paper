using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GK_Paper.Models
{
    public class Final_Data
    {
        public int id { get; set; }
        public List<string> Chk { get; set; }
        public string Index_No { get; set; }
        public Nullable<System.DateTime> Date_Time { get; set; }
        public string IP_Address { get; set; }
        public string Type { get; set; }
        public string Question_Id { get; set; }
        public string Paper_ID { get; set; }
        public string Question { get; set; }
        public string Option_A { get; set; }
        public string Option_B { get; set; }
        public string Option_C { get; set; }
        public string Option_D { get; set; }
        public string Option_1 { get; set; }
        public string Option_2 { get; set; }
        public string Option_3 { get; set; }
        public string Option_4 { get; set; }
        public string Correct_Option { get; set; }
        public string Verify { get; set; }
    }
}