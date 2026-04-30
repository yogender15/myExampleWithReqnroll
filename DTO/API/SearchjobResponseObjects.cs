using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.APIObjects
{
    public class SearchjobResponseObjects
    {
        public List<Value> Value { get; set; }
        public object Querycontext { get; set; }
    }
    public class Document
    {
        public string SearchObjectid { get; set; }
        public string SearchEntityname { get; set; }
        public string Ownerid { get; set; }
        public string Owneridname { get; set; }
        public string SearchOwneridLogicalname { get; set; }
        public int SearchObjecttypecode { get; set; }
        public object Entityimage_url { get; set; }
        public string Createdon { get; set; }
        public string Modifiedon { get; set; }
        public string SearchSecondaryfieldLogicalname { get; set; }
        public string Ticketnumber { get; set; }
        public string Title { get; set; }
        public string SearchPrimaryfieldLogicalname { get; set; }
        public string SearchEntitycollectionDisplayname { get; set; }
        public string SearchSecondaryfieldDisplayname { get; set; }
    }

    public class Value
    {
        public string Text { get; set; }
        public Document Document { get; set; }
    }
}
