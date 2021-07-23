using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADONETDataSet.Models
{
    [Table(Name ="Waiter")]
    public class Waiter
    {
        [Column(IsPrimaryKey =true,IsDbGenerated =true)]
        public int Id { get; set; }
        [Column(Name ="Name")]
        public string Name { get; set; }
        [Column]
        public int? CountryId { get; set; }
    }
}
