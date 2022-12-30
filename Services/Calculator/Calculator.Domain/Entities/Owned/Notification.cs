using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Domain.Entities.Owned
{
    [Owned]
    public class Notification
    {
        public DateTime PublishedDate { get; set; } = DateTime.Now;
    }
}
