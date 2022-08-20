using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Models
{
    public interface IUrlEntity
    {
        public int Id { get; set; }
        public string Url { get; set; }
    }
}